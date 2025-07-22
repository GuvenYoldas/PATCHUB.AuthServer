using PATCHUB.AuthServer.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Domain.Entities
{
    public class ClientRateLimitPolicyEntity
    {
        public int Id { get; set; }
        public string ClientId { get; set; } // FK olacak
        public int MaxRequestsPerMinute { get; set; }
        public int MaxRequestsPerHour { get; set; }
        public int MaxRequestsPerDay { get; set; }
        public string AllowedIPsJson { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        // Navigation
        public ClientCredential Client { get; set; }
    }
}
