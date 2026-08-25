using SLMS.Domain.Enums;

namespace SLMS.Domain.Entities;

// UC-C10, C15, C16, C17 / BR-P01-06. orderCode must be unique for idempotent webhook handling.
public class Payment
{
    public Guid Id { get; set; }
    public Guid BookingId { get; set; }
    public Booking? Booking { get; set; }

    public PaymentKind Kind { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public decimal Amount { get; set; }
    public string OrderCode { get; set; } = string.Empty; // idempotency key from gateway
    public string Provider { get; set; } = "VNPAY";

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? PaidAt { get; set; }
}
