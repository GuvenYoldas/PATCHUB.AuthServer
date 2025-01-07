using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Domain.Entities.Base
{
    public abstract class BaseAuditableEntity
    {
        /// <summary>
        /// -10: DELETED
        /// 0 : SYSTEM
        /// 100: PASSIVE
        /// 200: WAITING APPROVE
        /// 300: ACTIVE
        /// </summary>
        [Column("STATUS_CODE")]
        public int StatusCode { get; set; }

        [Column("UPDATE_DATE")]
        public DateTime? UpdateDate { get; set; }
        
        [Column("UPDATE_USERID")]
        public int? UpdateUserId { get; set; }
        
        [Column("UPDATE_IP")]
        public string? UpdateIp { get; set; }

        [Column("CREATE_DATE")]
        public DateTime? CreateDate { get; set; }
        
        [Column("CREATE_USERID")]
        public int? CreateUserId { get; set; }
        
        [Column("CREATE_IP")]
        public string? CreateIp {  get; set; }


    }
}
