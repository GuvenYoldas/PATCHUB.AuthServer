using PATCHUB.AuthServer.Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Domain.Common.Abstractions
{
    public interface IAuditable
    {
        public EnumStatusCode Status { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public string? UpdateIp { get; set; }

        public DateTime CreateDate { get; set; } 

        public int? CreateUserId { get; set; }

        public string? CreateIp { get; set; }
    }

}
