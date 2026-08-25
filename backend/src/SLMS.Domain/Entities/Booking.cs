using SLMS.Domain.Enums;

namespace SLMS.Domain.Entities;

// UC-C09, C10, C15, C16, C17 / BR-A01-06, BR-T02-06
public class Booking
{
    public Guid Id { get; set; }
    public Guid TravelerId { get; set; }
    public Guid StationId { get; set; }
    public Guid? LockerId { get; set; } // assigned at check-in (UC-K05)
    public string Size { get; set; } = "M";

    public DateTimeOffset StartAt { get; set; }
    public DateTimeOffset EndAt { get; set; }
    public DateTimeOffset? PaymentExpiresAt { get; set; } // BR: 10 min from PendingPayment

    public BookingStatus Status { get; set; } = BookingStatus.PendingPayment;
    public bool IsOverdue { get; set; }
    public decimal OverdueFeeAccrued { get; set; }

    public decimal AmountBase { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public AccessCredential? AccessCredential { get; set; }
}
