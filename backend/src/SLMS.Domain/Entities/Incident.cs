using SLMS.Domain.Enums;

namespace SLMS.Domain.Entities;

// UC-A08, A09, A10, A12 / BR-S04. Segregation of duties: RaisedByUserId != ApprovedByUserId.
public class Incident
{
    public Guid Id { get; set; }
    public IncidentType Type { get; set; }
    public IncidentStatus Status { get; set; } = IncidentStatus.Open;

    public Guid? StationId { get; set; }
    public Guid? LockerId { get; set; }
    public Guid? BookingId { get; set; }

    public Guid RaisedByUserId { get; set; }
    public Guid? ApprovedByUserId { get; set; }
    public string? Reason { get; set; } // mandatory for remote unlock (UC-A09)

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ResolvedAt { get; set; }
}
