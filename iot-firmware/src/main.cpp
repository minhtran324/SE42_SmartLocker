// SLMS Locker Controller (ESP32) — starter skeleton.
// Covers: UC-I01 Connectivity, UC-I02 Heartbeat, UC-I03 Execute Unlock Command,
//         UC-I04 Detect/Publish Door Events, UC-I05 Detect Intrusion,
//         UC-I06 Verify Offline Credential and Unlock, UC-I07 Buffer/Replay Offline Events,
//         UC-I08 Synchronize RTC.
//
// This firmware NEVER makes business decisions — it only ever acts on a backend-signed
// UNLOCK command (online) or a signed, RTC-checked offline credential (offline).
// See docs/architecture.md at the repo root.

#include <Arduino.h>
#include <WiFi.h>
#include <WiFiClientSecure.h>
#include <PubSubClient.h>
#include <ArduinoJson.h>
#include "config.h"

WiFiClientSecure wifiClient;
PubSubClient mqttClient(wifiClient);

bool doorClosedLastState = true;
bool accessSessionActive = false; // set true only while a valid UNLOCK/offline-credential session is open
unsigned long lastHeartbeatAt = 0;
unsigned long lastDoorChangeAt = 0;

String topicCommand()   { return "slms/stations/" STATION_ID "/lockers/" DEVICE_ID "/command"; }
String topicHeartbeat()  { return "slms/stations/" STATION_ID "/lockers/" DEVICE_ID "/heartbeat"; }
String topicDoorEvent()  { return "slms/stations/" STATION_ID "/lockers/" DEVICE_ID "/door"; }
String topicIntrusion()  { return "slms/stations/" STATION_ID "/lockers/" DEVICE_ID "/intrusion"; }

void connectWiFi() {
  // UC-I01: non-blocking style reconnect loop; call from loop() instead of blocking setup().
  if (WiFi.status() == WL_CONNECTED) return;
  WiFi.begin(WIFI_SSID, WIFI_PASSWORD);
}

void publishHeartbeat() {
  // UC-I02
  StaticJsonDocument<128> doc;
  doc["deviceId"] = DEVICE_ID;
  doc["uptimeMs"] = millis();
  // TODO (UC-I08): include current RTC time so the backend can compute drift.
  char payload[128];
  serializeJson(doc, payload);
  mqttClient.publish(topicHeartbeat().c_str(), payload);
}

void publishDoorEvent(const char *eventType) {
  // UC-I04
  StaticJsonDocument<128> doc;
  doc["deviceId"] = DEVICE_ID;
  doc["event"] = eventType; // "DOOR_OPENED" | "DOOR_CLOSED"
  doc["occurredAtMs"] = millis(); // TODO: replace with RTC timestamp once UC-I08 is implemented
  char payload[128];
  serializeJson(doc, payload);
  mqttClient.publish(topicDoorEvent().c_str(), payload);
}

void publishIntrusion() {
  // UC-I05: door opened while no access session is active.
  StaticJsonDocument<64> doc;
  doc["deviceId"] = DEVICE_ID;
  char payload[64];
  serializeJson(doc, payload);
  mqttClient.publish(topicIntrusion().c_str(), payload);
}

void triggerSolenoid() {
  digitalWrite(PIN_SOLENOID, HIGH);
  delay(500); // TODO: replace with a non-blocking timer if the loop needs to stay responsive
  digitalWrite(PIN_SOLENOID, LOW);
}

void handleUnlockCommand(JsonDocument &doc) {
  // UC-I03: verify requestId + expiry (and signature) before ever triggering hardware.
  const char *requestId = doc["requestId"];
  long expiresAtMs = doc["expiresAtMs"];
  // TODO: verify signature against the backend's public key / shared secret.
  // TODO: reject if millis()-based/RTC-based "now" > expiresAtMs, or requestId already used.
  (void)requestId;
  (void)expiresAtMs;

  accessSessionActive = true;
  triggerSolenoid();
}

void verifyOfflineCredentialAndUnlock(JsonDocument &doc) {
  // UC-I06: only reachable while this device has no MQTT connection. Verify signature,
  // expiry against the onboard RTC, and that the nonce hasn't been used before (replay
  // protection) prior to triggering the solenoid.
  // TODO: implement signature + RTC + nonce checks.
  accessSessionActive = true;
  triggerSolenoid();
}

void onMqttMessage(char *topic, byte *payload, unsigned int length) {
  StaticJsonDocument<256> doc;
  DeserializationError err = deserializeJson(doc, payload, length);
  if (err) return;

  String t(topic);
  if (t == topicCommand()) {
    handleUnlockCommand(doc);
  }
}

void connectMqtt() {
  // UC-I01: TLS + auto-reconnect. Configure wifiClient.setCACert(...) with the broker's CA
  // before shipping — left unset here for local dev against infra/mosquitto (plaintext 1883).
  if (mqttClient.connected()) return;
  if (mqttClient.connect(DEVICE_ID)) {
    mqttClient.subscribe(topicCommand().c_str());
  }
}

void checkDoorSensor() {
  // UC-I04: debounce, publish DOOR_OPENED/DOOR_CLOSED.
  // UC-I05: if the door opens with no active access session, that's an intrusion.
  bool doorClosedNow = digitalRead(PIN_LIMIT_SWITCH) == HIGH;
  unsigned long now = millis();

  if (doorClosedNow != doorClosedLastState && (now - lastDoorChangeAt) > DOOR_DEBOUNCE_MS) {
    lastDoorChangeAt = now;
    doorClosedLastState = doorClosedNow;

    if (!doorClosedNow) {
      publishDoorEvent("DOOR_OPENED");
      if (!accessSessionActive) {
        publishIntrusion();
      }
    } else {
      publishDoorEvent("DOOR_CLOSED");
      accessSessionActive = false;
    }
  }
}

void setup() {
  Serial.begin(115200);
  pinMode(PIN_SOLENOID, OUTPUT);
  pinMode(PIN_LIMIT_SWITCH, INPUT_PULLUP);
  pinMode(PIN_STATUS_LED, OUTPUT);

  connectWiFi();
  mqttClient.setServer(MQTT_BROKER_HOST, MQTT_BROKER_PORT);
  mqttClient.setCallback(onMqttMessage);

  // TODO (UC-I07): initialize an on-flash/RAM ring buffer for events raised while offline,
  // and replay it here once WiFi + MQTT reconnect.
  // TODO (UC-I08): sync RTC (e.g. via NTP or a backend RTC-sync message) on every reconnect.
}

void loop() {
  connectWiFi();
  connectMqtt();
  mqttClient.loop();

  checkDoorSensor();

  unsigned long now = millis();
  if (now - lastHeartbeatAt >= HEARTBEAT_INTERVAL_MS) {
    lastHeartbeatAt = now;
    publishHeartbeat();
  }

  // TODO: if WiFi/MQTT is down, fall back to verifyOfflineCredentialAndUnlock() when a
  // credential is presented locally (e.g. via a companion Kiosk-to-device BLE/serial link).
}
