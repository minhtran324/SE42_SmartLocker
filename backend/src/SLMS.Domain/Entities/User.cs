using SLMS.Domain.Enums;

namespace SLMS.Domain.Entities;

// UC-C01–C05. Guest/Traveler = no InternalRole; internal staff set InternalRole (UC-A06).
public class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public UserAccountStatus Status { get; set; } = UserAccountStatus.PendingVerification;
    public InternalRole? InternalRole { get; set; }
    public bool HasFaceProfile { get; set; }

    // BR-D04: consent record captured at registration (version, timestamp, IP)
    public string? TermsAcceptedVersion { get; set; }
    public DateTimeOffset? TermsAcceptedAt { get; set; }
    public string? TermsAcceptedFromIp { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
