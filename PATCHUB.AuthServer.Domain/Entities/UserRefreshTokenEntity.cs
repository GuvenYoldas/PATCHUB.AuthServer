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

    }
}
