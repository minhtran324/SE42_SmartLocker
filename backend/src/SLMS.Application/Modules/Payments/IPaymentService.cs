using SLMS.Domain.Entities;

namespace SLMS.Application.Modules.Payments;

// UC-C10, C17, UC-A13. See docs/module-map.md.
public interface IPaymentService
{
    // UC-C10 / UC-C17: create a Payment (Base/Extension/Overdue) and return the gateway
    // redirect/QR payload.
    Task<Payment> CreatePaymentAsync(Guid bookingId, string kind, decimal amount, CancellationToken ct);

    // UC-C10 / BR-P02, BR-P04: verify signature, process idempotently by orderCode (a retried
    // webhook for an already-processed orderCode must be a no-op that returns the first result).
    // On success: Payment -> Paid, Booking -> Confirmed, issue + pre-cache AccessCredential.
    Task HandleWebhookAsync(string orderCode, bool success, string signature, CancellationToken ct);

    // UC-C16: issue a refund per the cancellation policy tier; raises a PAYMENT_ERROR incident
    // if the gateway rejects the refund request.
    Task RefundAsync(Guid paymentId, decimal amount, CancellationToken ct);

    // BR-P04: periodic reconciliation job for webhooks that never arrived.
    Task ReconcilePendingPaymentsAsync(CancellationToken ct);
}
