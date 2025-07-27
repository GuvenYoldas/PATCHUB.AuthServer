using PATCHUB.AuthServer.Domain.Entities.Base;

namespace PATCHUB.AuthServer.Domain.Entities
{
    public class ClientAllowedIpEntity : BaseEntity<int>
    {
        public int IDRateLimitPolicy { get; set; } // Foreign key to ClientRateLimitPolicyEntity
        public string IpAddress { get; set; }

        public ClientRateLimitPolicyEntity Policy { get; set; }
    }
}
