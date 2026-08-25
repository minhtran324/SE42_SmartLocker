using SLMS.Domain.Entities;
using SLMS.Domain.Enums;
using Xunit;

namespace SLMS.Api.Tests;

// Placeholder test — replace with real coverage as UC-C09 (Create Booking) is implemented.
public class BookingDomainTests
{
    [Fact]
    public void NewBooking_DefaultsToPendingPayment()
    {
        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            StationId = Guid.NewGuid(),
            TravelerId = Guid.NewGuid(),
            Size = "M",
            StartAt = DateTimeOffset.UtcNow,
            EndAt = DateTimeOffset.UtcNow.AddHours(2)
        };

        Assert.Equal(BookingStatus.PendingPayment, booking.Status);
    }
}
