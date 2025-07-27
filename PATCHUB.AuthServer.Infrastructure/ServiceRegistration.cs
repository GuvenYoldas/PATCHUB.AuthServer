using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PATCHUB.AuthServer.Infrastructure.AuthTokenService;

namespace PATCHUB.AuthServer.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration = null)
        {
            serviceCollection.AddTransient<TokenService>();
            serviceCollection.AddTransient<AuthenticationService>();
        }
    }
}
