using PATCHUB.AuthServer.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Domain.Entities
{
    public class ClientRateLimitPolicyEntity : BaseEntity<int>
    {
        public Guid IDClient { get; set; } // FK olacak
        public int MaxRequestsPerMinute { get; set; }
        public int MaxRequestsPerHour { get; set; }
        public int MaxRequestsPerDay { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        // Navigation
        public ClientCredentialEntity Client { get; set; }
        public ICollection<ClientAllowedIpEntity> AllowedIps { get; set; } = new List<ClientAllowedIpEntity>();
    }
}
