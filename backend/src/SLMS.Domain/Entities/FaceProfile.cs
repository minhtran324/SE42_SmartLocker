namespace SLMS.Domain.Entities;

// UC-C11, UC-C19 / BR-D01-03, BR-D06: encrypted embedding only, never the raw image.
public class FaceProfile
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public byte[] EncryptedEmbedding { get; set; } = Array.Empty<byte>();
    public DateTimeOffset EnrolledAt { get; set; } = DateTimeOffset.UtcNow;
}
