using Microsoft.AspNetCore.Mvc;
using SLMS.Application.Modules.Auth;

namespace SLMS.Api.Controllers;

// UC-C01–C05, UC-C11, UC-C19, UC-A01
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    public record RegisterRequest(string FullName, string Email, string? Phone, string Password);
    public record LoginRequest(string EmailOrPhone, string Password);

    [HttpPost("register")] // UC-C01
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken ct)
    {
        await _authService.RegisterAsync(request.FullName, request.Email, request.Phone, request.Password, ct);
        return Accepted();
    }

    [HttpPost("login")] // UC-C02 / UC-A01
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken ct)
    {
        var (accessToken, refreshToken) = await _authService.LoginAsync(request.EmailOrPhone, request.Password, ct);
        return Ok(new { accessToken, refreshToken });
    }

    [HttpPost("recover-password")] // UC-C04
    public async Task<IActionResult> RecoverPassword([FromBody] string emailOrPhone, CancellationToken ct)
    {
        await _authService.RecoverPasswordAsync(emailOrPhone, ct);
        return Accepted();
    }
}
