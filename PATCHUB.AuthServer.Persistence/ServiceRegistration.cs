using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PATCHUB.AuthServer.Persistence.Context;
using PATCHUB.AuthServer.Persistence.Repositories;
using PATCHUB.AuthServer.Persistence.UnitOfWorks;

namespace PATCHUB.AuthServer.Persistence
{
    // internal class ServiceRegistration
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection serviceCollection, IConfiguration configuration = null)
        {
            serviceCollection.AddDbContext<AuthDbContext>(options =>
            options.UseSqlServer(configuration?.GetConnectionString("AuthDbSQLConnection")));

            serviceCollection.AddScoped<UserRefreshTokenRepository>();
            serviceCollection.AddScoped<ContactRequestRepository>();
            serviceCollection.AddScoped<UnitOfWork<AuthDbContext>>();

            serviceCollection.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration?.GetConnectionString("AppDbSQLConnection")));

            serviceCollection.AddScoped<UserRepository>();
            serviceCollection.AddScoped<UnitOfWork<AppDbContext>>();
        }
    }
}
