using PATCHUB.AuthServer.Domain.Common.Primitives;

namespace PATCHUB.AuthServer.Domain.Entities
{
    public class ApiUsageLogEntity: AuditableEntity<int>
    {
        public int IDClientCredential { get; set; }
        public int? IDUser { get; set; }
        // public string Token { get; set; } = null!; TOKEN loglamak riski!
        public string Method { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string Endpoint { get; set; } = null!;
        public string IP { get; set; } = null!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? ExtraDataJson { get; set; }
    }
}
