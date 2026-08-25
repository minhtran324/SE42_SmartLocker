#pragma once

// Fill these in locally (or load from NVS/provisioning) — do not commit real values.
#define WIFI_SSID "CHANGE_ME"
#define WIFI_PASSWORD "CHANGE_ME"

#define MQTT_BROKER_HOST "CHANGE_ME"
#define MQTT_BROKER_PORT 8883 // TLS, per UC-I01

#define DEVICE_ID "locker-CHANGE_ME"
#define STATION_ID "station-CHANGE_ME"

// Pins — adjust to the actual wiring.
#define PIN_SOLENOID 26
#define PIN_LIMIT_SWITCH 27
#define PIN_STATUS_LED 2

#define HEARTBEAT_INTERVAL_MS 15000UL   // UC-I02
#define DOOR_DEBOUNCE_MS 50UL           // UC-I04
