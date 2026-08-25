using Microsoft.AspNetCore.Mvc;
using SLMS.Application.Modules.Lockers;

namespace SLMS.Api.Controllers;

// UC-A04, A07, A11
[ApiController]
[Route("api/lockers")]
public class LockersController : ControllerBase
{
    private readonly ILockerService _lockerService;

    public LockersController(ILockerService lockerService)
    {
        _lockerService = lockerService;
    }

    [HttpGet("device-health")] // UC-A07
    public async Task<IActionResult> GetDeviceHealth([FromQuery] Guid? stationId, CancellationToken ct)
    {
        var lockers = await _lockerService.GetDeviceHealthAsync(stationId, ct);
        return Ok(lockers);
    }

    [HttpPut("{lockerId:guid}/status")] // UC-A04
    public async Task<IActionResult> UpdateStatus(Guid lockerId, [FromBody] string status, CancellationToken ct)
    {
        var locker = await _lockerService.UpdateStatusAsync(lockerId, status, ct);
        return Ok(locker);
    }

    [HttpPost("{lockerId:guid}/maintenance")] // UC-A11
    public async Task<IActionResult> SetMaintenance(
        Guid lockerId, [FromQuery] bool underMaintenance, [FromQuery] string? reason, CancellationToken ct)
    {
        await _lockerService.SetMaintenanceAsync(lockerId, underMaintenance, reason, ct);
        return NoContent();
    }
}
