using PATCHUB.AuthServer.Domain.Common.Abstractions;
using PATCHUB.AuthServer.Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Domain.Common.Primitives
{
    // Sadece audit isteyenler (ID’siz) gerekirse
    public abstract class AuditableEntity : BaseEntity
    {
        [Column("STATUS_CODE")]
        public int StatusCode { get; set; }

        [NotMapped]
        public EnumStatusCode Status
        {
            get => Enum.IsDefined(typeof(EnumStatusCode), StatusCode) ? (EnumStatusCode)StatusCode : EnumStatusCode.SYSTEM;
            set => StatusCode = (int)value;
        }

        [Column("UPDATE_DATE")]
        public DateTime? UpdateDate { get; set; }

        [Column("UPDATE_USERID")]
        public int? UpdateUserId { get; set; }

        [Column("UPDATE_IP")]
        public string? UpdateIp { get; set; }

        [Column("CREATE_DATE")]
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        [Column("CREATE_USERID")]
        public int? CreateUserId { get; set; }

        [Column("CREATE_IP")]
        public string? CreateIp { get; set; }
    }

    //  Hem ID hem audit isteyenler
    public abstract class AuditableEntity<TKey> : BaseEntity<TKey>
    {
        [Column("STATUS_CODE")]
        public int StatusCode { get; set; }

        [NotMapped]
        public EnumStatusCode Status
        {
            get => Enum.IsDefined(typeof(EnumStatusCode), StatusCode) ? (EnumStatusCode)StatusCode : EnumStatusCode.SYSTEM;
            set => StatusCode = (int)value;
        }

        [Column("UPDATE_DATE")]
        public DateTime? UpdateDate { get; set; }

        [Column("UPDATE_USERID")]
        public int? UpdateUserId { get; set; }

        [Column("UPDATE_IP")]
        public string? UpdateIp { get; set; }

        [Column("CREATE_DATE")]
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        [Column("CREATE_USERID")]
        public int? CreateUserId { get; set; }

        [Column("CREATE_IP")]
        public string? CreateIp { get; set; }
    }
}
