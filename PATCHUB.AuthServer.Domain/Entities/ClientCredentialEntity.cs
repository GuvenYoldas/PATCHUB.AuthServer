using PATCHUB.AuthServer.Domain.Common.Abstractions;
using PATCHUB.AuthServer.Domain.Common.Primitives;
using PATCHUB.AuthServer.Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PATCHUB.AuthServer.Domain.Entities
{
    public class ClientCredentialEntity : AuditableEntity<int>
    {
        public Guid IDClient { get; set; }
        public string SecretHash { get; set; }
        public int RequestLimit { get; set; }
        public int RequestCount { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public ICollection<ClientRateLimitPolicyEntity> RateLimitPolicies { get; set; } = new List<ClientRateLimitPolicyEntity>();

        private ClientCredentialEntity() { } // EF için

        public static ClientCredentialEntity Create(string secretHash, int requestLimit, DateTime expirationDate) // constructure
        {
            return new ClientCredentialEntity
            {
                IDClient = Guid.NewGuid(),
                SecretHash = secretHash,
                RequestLimit = requestLimit,
                RequestCount = 0,
                ExpirationDate = expirationDate,
                Status = EnumStatusCode.ACTIVE
            };
        }
    }
}
