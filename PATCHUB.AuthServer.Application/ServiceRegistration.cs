using Microsoft.Extensions.DependencyInjection;
using PATCHUB.AuthServer.Application.Services.Interfaces;
using PATCHUB.AuthServer.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IClientCredentialService, ClientCredentialService>();

        }
    }
}
