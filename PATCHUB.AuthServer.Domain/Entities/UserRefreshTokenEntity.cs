using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PATCHUB.AuthServer.Domain.Entities.Base;

namespace PATCHUB.AuthServer.Domain.Entities
{
    public class UserRefreshTokenEntity : BaseEntity
    {
        public int IDUser { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int IDClientCredential { get; set; } // Refresh token hangi client'a ait?

        public bool IsRevoked { get; set; } = false; // Token silindi mi, geçersiz mi?

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ClientCredentialEntity Client { get; set; }
    }

}
