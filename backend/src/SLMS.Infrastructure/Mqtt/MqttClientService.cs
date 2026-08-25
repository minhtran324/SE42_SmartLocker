namespace SLMS.Infrastructure.Mqtt;

// UC-I01–I05: backend-side MQTT client. Subscribes to device heartbeat/door/intrusion topics
// and publishes signed UNLOCK commands. Wire up MQTTnet's IMqttClient here; kept as a thin
// wrapper so SLMS.Application only depends on an interface (add one in Modules/Iot when wiring).
public class MqttClientService
{
    // TODO: connect to MQTT_HOST:MQTT_PORT (or TLS 8883) from configuration, with auto-reconnect.
    // TODO: subscribe to "slms/stations/+/lockers/+/heartbeat", ".../door", ".../intrusion".
    // TODO: publish to "slms/stations/{stationId}/lockers/{lockerId}/command" with
    //       { command: "UNLOCK", requestId, expiresAt, signature }.
}
