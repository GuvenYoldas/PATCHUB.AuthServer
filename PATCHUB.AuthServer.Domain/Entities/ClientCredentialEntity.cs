using PATCHUB.AuthServer.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Domain.Entities
{
    public class ClientCredential : BaseEntity
    {
        public string IDClient { get; set; }
        public string SecretClient { get; set; }
        public int RequestLimit { get; set; }
        public int RequestCount { get; set; }
        public int ExpirationDate { get; set; }
    }
}
