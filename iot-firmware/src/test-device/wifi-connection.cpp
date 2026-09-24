#include <WiFi.h>

const char* WIFI_NAME = "Clover";
const char* WIFI_PASSWORD = "12345678zxc";

unsigned long lastReconnectTime = 0;
const unsigned long RECONNECT_INTERVAL = 5000;

void connectWiFi() {
  Serial.print("Dang ket noi WiFi: ");
  Serial.println(WIFI_NAME);

  WiFi.mode(WIFI_STA);
  WiFi.begin(WIFI_NAME, WIFI_PASSWORD);
}

void setup() {
  Serial.begin(115200);
  delay(1000);

  Serial.println();
  Serial.println("=== SMARTLOCKER WIFI TEST ===");

  WiFi.setAutoReconnect(true);
  WiFi.persistent(true);

  connectWiFi();
}

void loop() {
  if (WiFi.status() == WL_CONNECTED) {
    Serial.print("WiFi OK | IP: ");
    Serial.print(WiFi.localIP());

    Serial.print(" | RSSI: ");
    Serial.print(WiFi.RSSI());
    Serial.println(" dBm");

  } else {
    Serial.println("WiFi bi mat ket noi...");

    // Cứ mỗi 5 giây thử kết nối lại một lần
    if (millis() - lastReconnectTime >= RECONNECT_INTERVAL) {
      lastReconnectTime = millis();

      Serial.println("Dang thu ket noi lai...");
      WiFi.disconnect();
      delay(200);
      WiFi.begin(WIFI_NAME, WIFI_PASSWORD);
    }
  }

  delay(1000);
}