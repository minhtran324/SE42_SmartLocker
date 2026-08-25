using SLMS.Domain.Enums;

namespace SLMS.Domain.Entities;

// UC-A03, UC-C06, UC-C07
public class Station
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public StationStatus Status { get; set; } = StationStatus.Active;
    public string OpeningHours { get; set; } = string.Empty;

    public ICollection<Locker> Lockers { get; set; } = new List<Locker>();
}
