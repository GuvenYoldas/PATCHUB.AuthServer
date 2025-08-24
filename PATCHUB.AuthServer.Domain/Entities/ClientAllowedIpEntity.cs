using PATCHUB.AuthServer.Domain.Common.Primitives;
using PATCHUB.AuthServer.Domain.Enumeration;

namespace PATCHUB.AuthServer.Domain.Entities
{
    public class ClientAllowedIpEntity : AuditableEntity<int>
    {
        public int IDRateLimitPolicy { get; set; } // Foreign key to ClientRateLimitPolicyEntity
        public string IpAddress { get; set; }
        public ClientRateLimitPolicyEntity RateLimitPolicy { get; set; }

        private ClientAllowedIpEntity() { } // EF için

        public static ClientAllowedIpEntity Create(string ipAddress, ClientRateLimitPolicyEntity policy)
        {
            return new ClientAllowedIpEntity
            {
                IpAddress = ipAddress,
                RateLimitPolicy = policy,
                Status = EnumStatusCode.ACTIVE
            };
        }
    }
}
