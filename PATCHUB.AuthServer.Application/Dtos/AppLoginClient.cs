using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Application.Dtos
{
    public class AppLoginClient
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
