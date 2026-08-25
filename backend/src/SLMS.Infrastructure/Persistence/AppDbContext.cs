using Microsoft.EntityFrameworkCore;
using SLMS.Domain.Entities;

namespace SLMS.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Station> Stations => Set<Station>();
    public DbSet<Locker> Lockers => Set<Locker>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<FaceProfile> FaceProfiles => Set<FaceProfile>();
    public DbSet<AccessCredential> AccessCredentials => Set<AccessCredential>();
    public DbSet<Incident> Incidents => Set<Incident>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // UC-C10 / BR-P04: orderCode must be unique so a retried webhook can be handled idempotently.
        modelBuilder.Entity<Payment>()
            .HasIndex(p => p.OrderCode)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
