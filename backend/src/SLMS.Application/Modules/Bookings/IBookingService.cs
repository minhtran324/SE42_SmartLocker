using SLMS.Domain.Entities;

namespace SLMS.Application.Modules.Bookings;

// UC-C09, C13-C17, UC-K05, K06, UC-A13. See docs/module-map.md.
public interface IBookingService
{
    // UC-C09 / BR-A01-06: acquire lock:alloc:{stationId}:{size} (Redis, TTL 5s), re-check
    // availability inside the lock, create Booking as PendingPayment with a 10-minute
    // paymentExpiresAt, release lock via Lua script keyed on requestId.
    Task<Booking> CreateBookingAsync(Guid travelerId, Guid stationId, string size,
        DateTimeOffset startAt, DateTimeOffset endAt, CancellationToken ct);

    // UC-C15 / BR-A07, BR-O10, BR-T05: extend endAt, roll in accrued overdue fee, reissue
    // credential, and push the refreshed credential to both mobile app and kiosk cache.
    Task ExtendBookingAsync(Guid bookingId, TimeSpan extension, CancellationToken ct);

    // UC-C16 / BR-P05, BR-T06: refund tiers — 100% (>2h before start), 50% (<2h before start),
    // 0% (after start, not yet checked in). Not allowed once Stored.
    Task CancelBookingAsync(Guid bookingId, CancellationToken ct);

    // UC-K05: assign a locker to a Confirmed booking, transition to Stored on sensor confirmation.
    Task<Locker> CheckInAsync(Guid bookingId, CancellationToken ct);

    // UC-K06: verify overdue fee is settled, transition Stored -> Completed on sensor confirmation.
    Task CheckOutAsync(Guid bookingId, CancellationToken ct);

    Task<Booking?> GetByIdAsync(Guid bookingId, CancellationToken ct);
    Task<IReadOnlyList<Booking>> GetHistoryAsync(Guid travelerId, int page, int pageSize, CancellationToken ct);
}
