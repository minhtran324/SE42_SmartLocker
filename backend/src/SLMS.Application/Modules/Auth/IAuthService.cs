namespace SLMS.Application.Modules.Auth;

// UC-C01–C05, UC-C11, UC-C19, UC-A01, UC-A06. See docs/module-map.md.
public interface IAuthService
{
    // UC-C01: create account PENDING_VERIFICATION, send OTP, verify OTP -> ACTIVE.
    Task RegisterAsync(string fullName, string email, string? phone, string password, CancellationToken ct);

    // UC-C02 / UC-A01: authenticate, issue access + refresh token.
    Task<(string accessToken, string refreshToken)> LoginAsync(string emailOrPhone, string password, CancellationToken ct);

    // UC-C03: revoke refresh token.
    Task LogoutAsync(Guid userId, string refreshToken, CancellationToken ct);

    // UC-C04: OTP-based password recovery, 10-minute reset token, revokes all sessions on success.
    Task RecoverPasswordAsync(string emailOrPhone, CancellationToken ct);

    // UC-C11: enroll face -> call Face Recognition Engine, store encrypted embedding only (BR-D01-03).
    Task EnrollFaceAsync(Guid userId, byte[] imageBytes, CancellationToken ct);

    // UC-C19: delete stored FaceProfile.
    Task DeleteFaceProfileAsync(Guid userId, CancellationToken ct);
}
