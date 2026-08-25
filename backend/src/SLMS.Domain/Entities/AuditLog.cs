namespace SLMS.Domain.Entities;

// UC-A14. Every sensitive action (remote unlock, incident approval, admin CRUD) writes one row.
public class AuditLog
{
    public Guid Id { get; set; }
    public Guid ActorUserId { get; set; }
    public string Action { get; set; } = string.Empty;       // e.g. "REMOTE_UNLOCK"
    public string TargetType { get; set; } = string.Empty;   // e.g. "Locker"
    public string TargetId { get; set; } = string.Empty;
    public string? Details { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
