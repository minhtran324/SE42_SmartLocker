namespace SLMS.Application.Modules.Iot;

// UC-I01–I08, UC-A09. MQTT-facing boundary — the only place that talks to ESP32 devices.
// See docs/module-map.md and docs/architecture.md for the signed-command / offline-credential model.
public interface IIotCommandService
{
    // UC-I03 / UC-A09: publish a signed UNLOCK command with requestId + expiry to a locker's topic.
    // Remote unlock (UC-A09) additionally requires a mandatory reason + audit log write before send.
    Task SendUnlockCommandAsync(Guid lockerId, Guid requestId, string? reasonIfRemote, CancellationToken ct);

    // UC-I02 / UC-A07: handle an incoming heartbeat, update Locker.LastHeartbeatAt / RtcDrift.
    Task HandleHeartbeatAsync(string deviceId, DateTimeOffset deviceTimeUtc, string firmwareVersion, CancellationToken ct);

    // UC-I04: handle DOOR_OPENED / DOOR_CLOSED events, drive Booking check-in/check-out confirmation.
    Task HandleDoorEventAsync(string deviceId, string eventType, DateTimeOffset occurredAt, CancellationToken ct);

    // UC-I05: door opened outside a valid access session -> raise a DoorIntrusion incident.
    Task HandleIntrusionEventAsync(string deviceId, DateTimeOffset occurredAt, CancellationToken ct);

    // UC-I07: ingest buffered events replayed by a device after reconnecting.
    Task ReplayBufferedEventsAsync(string deviceId, IReadOnlyList<object> events, CancellationToken ct);
}
