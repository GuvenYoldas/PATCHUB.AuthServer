using PATCHUB.AuthServer.Domain.Common.Primitives;
using PATCHUB.AuthServer.Domain.Enumeration;

namespace PATCHUB.AuthServer.Domain.Entities
{
    public class ClientRateLimitPolicyEntity : AuditableEntity<int>
    {
        public int IDClientCredential { get; set; } // FK olacak
        public int MaxRequestsPerMinute { get; set; }
        public int MaxRequestsPerHour { get; set; }
        public int MaxRequestsPerDay { get; set; }
        public DateTime LastUpdated { get; set; }

        // Navigation
        public ClientCredentialEntity Client { get; set; }
        public ICollection<ClientAllowedIpEntity> AllowedIps { get; set; } = new List<ClientAllowedIpEntity>();


        private ClientRateLimitPolicyEntity() { } // EF için

        public static ClientRateLimitPolicyEntity Create(int maxRequestsPerMinute, int maxRequestsPerHour, int maxRequestsPerDay, ClientCredentialEntity clientCredential) // constructure
        {
            return new ClientRateLimitPolicyEntity
            {
                LastUpdated = DateTime.UtcNow,
                MaxRequestsPerMinute = maxRequestsPerMinute,
                MaxRequestsPerHour = maxRequestsPerHour,
                MaxRequestsPerDay = maxRequestsPerDay,
                Status = EnumStatusCode.ACTIVE,
                Client = clientCredential
            };
        }
    }
}
