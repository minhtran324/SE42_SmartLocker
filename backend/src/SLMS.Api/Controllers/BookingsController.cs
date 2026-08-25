using Microsoft.AspNetCore.Mvc;
using SLMS.Application.Modules.Bookings;

namespace SLMS.Api.Controllers;

// UC-C09, C13-C17, UC-K05, K06, UC-A13
[ApiController]
[Route("api/bookings")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public record CreateBookingRequest(Guid StationId, string Size, DateTimeOffset StartAt, DateTimeOffset EndAt);

    [HttpPost] // UC-C09
    public async Task<IActionResult> Create(CreateBookingRequest request, CancellationToken ct)
    {
        // TODO: resolve travelerId from the authenticated principal once auth is wired up.
        var travelerId = Guid.Empty;
        var booking = await _bookingService.CreateBookingAsync(
            travelerId, request.StationId, request.Size, request.StartAt, request.EndAt, ct);
        return CreatedAtAction(nameof(GetById), new { bookingId = booking.Id }, booking);
    }

    [HttpGet("{bookingId:guid}")] // UC-C13
    public async Task<IActionResult> GetById(Guid bookingId, CancellationToken ct)
    {
        var booking = await _bookingService.GetByIdAsync(bookingId, ct);
        return booking is null ? NotFound() : Ok(booking);
    }

    [HttpPost("{bookingId:guid}/extend")] // UC-C15
    public async Task<IActionResult> Extend(Guid bookingId, [FromBody] TimeSpan extension, CancellationToken ct)
    {
        await _bookingService.ExtendBookingAsync(bookingId, extension, ct);
        return NoContent();
    }

    [HttpPost("{bookingId:guid}/cancel")] // UC-C16
    public async Task<IActionResult> Cancel(Guid bookingId, CancellationToken ct)
    {
        await _bookingService.CancelBookingAsync(bookingId, ct);
        return NoContent();
    }

    [HttpPost("{bookingId:guid}/check-in")] // UC-K05
    public async Task<IActionResult> CheckIn(Guid bookingId, CancellationToken ct)
    {
        var locker = await _bookingService.CheckInAsync(bookingId, ct);
        return Ok(locker);
    }

    [HttpPost("{bookingId:guid}/check-out")] // UC-K06
    public async Task<IActionResult> CheckOut(Guid bookingId, CancellationToken ct)
    {
        await _bookingService.CheckOutAsync(bookingId, ct);
        return NoContent();
    }
}
