using SLMS.Domain.Entities;

namespace SLMS.Application.Modules.Stations;

// UC-C06–C08, UC-A03. See docs/module-map.md.
public interface IStationService
{
    // UC-C06: stations within a default radius of (lat, lng), with distance/status/base price.
    Task<IReadOnlyList<Station>> FindNearbyAsync(double lat, double lng, double radiusKm, CancellationToken ct);

    // UC-C07: full detail for one station.
    Task<Station?> GetDetailsAsync(Guid stationId, CancellationToken ct);

    // UC-C08 / BR-A01, BR-A02: bookable lockers minus overlapping active bookings, per size.
    Task<IReadOnlyDictionary<string, int>> GetAvailabilityAsync(
        Guid stationId, DateTimeOffset startAt, DateTimeOffset endAt, CancellationToken ct);

    // UC-A03: create/update/suspend a station and its device configuration.
    Task<Station> UpsertStationAsync(Station station, CancellationToken ct);
}
