using PATCHUB.AuthServer.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Domain.Entities
{
    public class ApiUsageLogEntity: BaseEntity<int>
    {
        public int IDClientCredential { get; set; }
        public int? IDUser { get; set; }
        // public string Token { get; set; } = null!; TOKEN loglamak riski!
        public string Method { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string Endpoint { get; set; } = null!;
        public string IP { get; set; } = null!;
        public int? StatusCode { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? ExtraDataJson { get; set; }
    }
}
