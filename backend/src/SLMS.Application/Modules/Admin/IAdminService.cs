using SLMS.Domain.Entities;

namespace SLMS.Application.Modules.Admin;

// UC-A02, A05, A06, A08, A10, A12, A14, A15. See docs/module-map.md.
public interface IAdminService
{
    // UC-A02: real-time station/locker/device/booking/revenue/incident snapshot.
    Task<object> GetDashboardSnapshotAsync(CancellationToken ct);

    // UC-A05: time-effective pricing policy per station and size.
    Task SetPricingPolicyAsync(Guid stationId, string size, decimal pricePerBlock, DateTimeOffset effectiveFrom, CancellationToken ct);

    // UC-A06: create/assign internal accounts (SystemAdministrator/StationOperator/Technician).
    Task<User> CreateInternalUserAsync(string fullName, string email, Domain.Enums.InternalRole role, CancellationToken ct);

    // UC-A08: open/investigate/resolve/close an incident.
    Task<Incident> UpdateIncidentAsync(Guid incidentId, string status, string? notes, CancellationToken ct);

    // UC-A10: coordinate emergency retrieval when the station's ESP32 is offline.
    Task<Incident> HandleEmergencyRetrievalAsync(Guid stationId, Guid lockerId, Guid raisedByUserId, CancellationToken ct);

    // UC-A12: 72h abandoned-property workflow. Approval must come from a different user than the
    // one who raised the request (segregation of duties, BR-S04).
    Task<Incident> ProcessAbandonedPropertyAsync(Guid bookingId, Guid raisedByUserId, Guid approvedByUserId, CancellationToken ct);

    // UC-A14: query the audit log by actor, target, and time range.
    Task<IReadOnlyList<AuditLog>> QueryAuditLogAsync(Guid? actorUserId, string? targetType, DateTimeOffset? from, DateTimeOffset? to, CancellationToken ct);

    // UC-A15: revenue, occupancy rate, verification success rate, incident counts.
    Task<object> GenerateOperationalReportAsync(DateTimeOffset from, DateTimeOffset to, CancellationToken ct);
}
