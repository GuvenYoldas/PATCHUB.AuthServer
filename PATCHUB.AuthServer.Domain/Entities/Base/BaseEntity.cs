using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PATCHUB.AuthServer.Domain.Entities.Base
{
    public abstract class BaseEntity
    {

    }

    public abstract class BaseEntity<TKey> : BaseAuditableEntity
    {
        [Key]
        [Column(name: "ID", Order = 0)]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TKey ID { get; set; }
    }
}
