namespace SLMS.Domain.Entities;

// UC-C10, C12 / UC-K02-04, K08 / UC-I06 / BR-S06, S07, O09.
// Signed so the kiosk/ESP32 can verify authenticity offline without calling the backend.
public class AccessCredential
{
    public Guid Id { get; set; }
    public Guid BookingId { get; set; }

    public string SignedQrPayload { get; set; } = string.Empty;
    public string TotpSeed { get; set; } = string.Empty; // encrypted at rest
    public string Pin { get; set; } = string.Empty;       // hashed at rest

    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset IssuedAt { get; set; } = DateTimeOffset.UtcNow;

    // Reissued on extension (UC-C15) — old signatures must be invalidated.
    public int Version { get; set; } = 1;
}
