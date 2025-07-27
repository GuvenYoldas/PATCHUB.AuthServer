using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Application.Dtos
{
    public class AppLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
