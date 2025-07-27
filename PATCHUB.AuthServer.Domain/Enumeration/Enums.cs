using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Domain.Enumeration
{
    public enum StatusCode
    {
        DELETED = -10,
        SYSTEM = 0,
        PASSIVE = 100,
        WAITING_APPROVE = 200,
        ACTIVE = 300
    }
}
