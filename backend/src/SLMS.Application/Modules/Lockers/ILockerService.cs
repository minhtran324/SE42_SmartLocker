using SLMS.Domain.Entities;

namespace SLMS.Application.Modules.Lockers;

// UC-A04, A07, A11. See docs/module-map.md.
public interface ILockerService
{
    // UC-A04: 4-state view (Available/Occupied/Reserved/Maintenance), toggle bookable flag.
    Task<Locker> UpdateStatusAsync(Guid lockerId, string status, CancellationToken ct);

    // UC-A11 / UC-I: take a locker out of service, trigger hardware self-test, log result.
    Task SetMaintenanceAsync(Guid lockerId, bool underMaintenance, string? reason, CancellationToken ct);

    // UC-A07: heartbeat freshness, MQTT connectivity, firmware version, RTC drift per device.
    Task<IReadOnlyList<Locker>> GetDeviceHealthAsync(Guid? stationId, CancellationToken ct);
}
