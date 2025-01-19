using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PATCHUB.SharedLibrary.Dtos;

namespace PATCHUB.AuthServer.Application.Dtos
{
    public class AppClient
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public List<String> Audiences { get; set; }
    }
}
