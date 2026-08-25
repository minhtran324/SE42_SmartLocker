namespace SLMS.Application.Modules.Notifications;

// UC-C18. Channel matrix (push / email / SMS per event) is documented in the SRS §UC-C18.
public interface INotificationService
{
    // Falls back push -> email automatically on push failure (UC-C18 exception flow E1).
    Task NotifyAsync(Guid userId, string eventType, IReadOnlyDictionary<string, string> payload, CancellationToken ct);
}
