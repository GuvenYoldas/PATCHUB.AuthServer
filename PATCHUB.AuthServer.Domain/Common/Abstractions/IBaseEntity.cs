using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Domain.Common.Abstractions
{
    public interface IBaseEntity<TKey>
    {
        TKey ID { get; set; }   // "Id" konvansiyon-dostudur
    }
}
