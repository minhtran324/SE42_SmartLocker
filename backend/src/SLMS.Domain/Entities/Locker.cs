using SLMS.Domain.Enums;

namespace SLMS.Domain.Entities;

// UC-A04, UC-I03–I06. "Bookable" flag drives the slot-availability formula in UC-C08.
public class Locker
{
    public Guid Id { get; set; }
    public Guid StationId { get; set; }
    public Station? Station { get; set; }

    public string Code { get; set; } = string.Empty; // physical label, e.g. "A-12"
    public string Size { get; set; } = "M";           // S / M / L
    public LockerStatus Status { get; set; } = LockerStatus.Available;
    public bool IsBookable { get; set; } = true;

    // ESP32 device identity (UC-I01, UC-A07)
    public string DeviceId { get; set; } = string.Empty;
    public DateTimeOffset? LastHeartbeatAt { get; set; }
    public string? FirmwareVersion { get; set; }
    public TimeSpan? RtcDrift { get; set; }
}
