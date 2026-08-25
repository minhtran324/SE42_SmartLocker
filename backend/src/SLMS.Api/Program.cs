using Microsoft.EntityFrameworkCore;
using SLMS.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// UC-A01 / UC-C02: DB-backed identity. Connection string comes from .env / appsettings.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")
        ?? builder.Configuration["CONNECTION_STRING"]));

// TODO: register IAuthService, IStationService, IBookingService, IPaymentService,
// ILockerService, IIotCommandService, INotificationService, IAdminService implementations,
// plus RedisDistributedLock (StackExchange.Redis IConnectionMultiplexer) and MqttClientService.

// TODO: AddAuthentication/AddJwtBearer (JWT_SECRET, access/refresh token config from .env).

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins(
                "http://localhost:5173",  // web-admin (Vite dev)
                "http://localhost:5174")  // kiosk-app (Vite dev)
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Exposed for WebApplicationFactory-based integration tests.
public partial class Program { }
