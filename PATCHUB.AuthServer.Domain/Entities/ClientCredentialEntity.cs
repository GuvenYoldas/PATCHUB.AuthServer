using PATCHUB.AuthServer.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Domain.Entities
{
    public class ClientCredential : BaseEntity<int>
    {
        public Guid IDClient { get; set; }
        public string SecretHash { get; set; }
        public int RequestLimit { get; set; }
        public int RequestCount { get; set; }
        public int ExpirationDate { get; set; }

        public ICollection<ClientRateLimitPolicyEntity> RateLimitPolicies { get; set; }
    }
}
