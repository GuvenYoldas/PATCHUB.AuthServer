using PATCHUB.AuthServer.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Domain.Entities
{
    public class AuditLogEntity : BaseEntity
    {
        public int? UserId { get; set; }
        public string Action { get; set; }
        public string Endpoint { get; set; }
        public string IP { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? ExtraDataJson { get; set; }
    }
}
