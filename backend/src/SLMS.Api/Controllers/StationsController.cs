using Microsoft.AspNetCore.Mvc;
using SLMS.Application.Modules.Stations;

namespace SLMS.Api.Controllers;

// UC-C06–C08, UC-A03
[ApiController]
[Route("api/stations")]
public class StationsController : ControllerBase
{
    private readonly IStationService _stationService;

    public StationsController(IStationService stationService)
    {
        _stationService = stationService;
    }

    [HttpGet("nearby")] // UC-C06
    public async Task<IActionResult> FindNearby(
        [FromQuery] double lat, [FromQuery] double lng, [FromQuery] double radiusKm, CancellationToken ct)
    {
        var stations = await _stationService.FindNearbyAsync(lat, lng, radiusKm, ct);
        return Ok(stations);
    }

    [HttpGet("{stationId:guid}")] // UC-C07
    public async Task<IActionResult> GetDetails(Guid stationId, CancellationToken ct)
    {
        var station = await _stationService.GetDetailsAsync(stationId, ct);
        return station is null ? NotFound() : Ok(station);
    }

    [HttpGet("{stationId:guid}/availability")] // UC-C08
    public async Task<IActionResult> GetAvailability(
        Guid stationId, [FromQuery] DateTimeOffset startAt, [FromQuery] DateTimeOffset endAt, CancellationToken ct)
    {
        var availability = await _stationService.GetAvailabilityAsync(stationId, startAt, endAt, ct);
        return Ok(availability);
    }
}
