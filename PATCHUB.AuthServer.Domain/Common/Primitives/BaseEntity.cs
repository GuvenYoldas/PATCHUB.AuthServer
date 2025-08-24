using PATCHUB.AuthServer.Domain.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Domain.Common.Primitives
{
    public abstract class BaseEntity
    {
        // Ortak ama audit/ID olmayan şeyler için durabilir (domain events vb.)
    }

    // Sadece ID isteyenler buradan türesin
    public abstract class BaseEntity<TKey> : BaseEntity, IBaseEntity<TKey>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TKey ID { get; set; }
    }

}
