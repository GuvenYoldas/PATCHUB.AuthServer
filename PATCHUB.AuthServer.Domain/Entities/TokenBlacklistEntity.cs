using PATCHUB.AuthServer.Domain.Common.Primitives;

namespace PATCHUB.AuthServer.Domain.Entities
{
    public class TokenBlacklistEntity : BaseEntity
    {
        public string Token { get; set; } = null!;
        public DateTime RevokedAt { get; set; } = DateTime.UtcNow;
        public string Reason { get; set; }
    }
}
