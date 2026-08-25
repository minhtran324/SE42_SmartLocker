using Microsoft.AspNetCore.Mvc;
using SLMS.Application.Modules.Admin;

namespace SLMS.Api.Controllers;

// UC-A02, A05, A06, A08, A10, A12, A14, A15
[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet("dashboard")] // UC-A02
    public async Task<IActionResult> GetDashboard(CancellationToken ct)
        => Ok(await _adminService.GetDashboardSnapshotAsync(ct));

    [HttpGet("audit-log")] // UC-A14
    public async Task<IActionResult> GetAuditLog(
        [FromQuery] Guid? actorUserId, [FromQuery] string? targetType,
        [FromQuery] DateTimeOffset? from, [FromQuery] DateTimeOffset? to, CancellationToken ct)
        => Ok(await _adminService.QueryAuditLogAsync(actorUserId, targetType, from, to, ct));

    [HttpGet("reports")] // UC-A15
    public async Task<IActionResult> GetReport(
        [FromQuery] DateTimeOffset from, [FromQuery] DateTimeOffset to, CancellationToken ct)
        => Ok(await _adminService.GenerateOperationalReportAsync(from, to, ct));

    public record UpdateIncidentRequest(string Status, string? Notes);

    [HttpPut("incidents/{incidentId:guid}")] // UC-A08
    public async Task<IActionResult> UpdateIncident(Guid incidentId, UpdateIncidentRequest request, CancellationToken ct)
        => Ok(await _adminService.UpdateIncidentAsync(incidentId, request.Status, request.Notes, ct));
}
