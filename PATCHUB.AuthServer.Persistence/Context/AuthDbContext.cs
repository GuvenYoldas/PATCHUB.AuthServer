using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PATCHUB.AuthServer.Domain.Entities;
using PATCHUB.AuthServer.Persistence.Configurations;
using PATCHUB.AuthServer.Persistence.Configurations.Base;

namespace PATCHUB.AuthServer.Persistence.Context
{
    public class AuthDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }

        #region |       DbSet Entity Classes        |

        public DbSet<ClientAllowedIpEntity> ClientAllowedIp { get; set; }
        public DbSet<ClientCredentialEntity> ClientCredential { get; set; }
        public DbSet<ClientRateLimitPolicyEntity> ClientRateLimitPolicy { get; set; }

        public DbSet<UserRefreshTokenEntity> UserRefreshToken { get; set; }
        public DbSet<ContactRequestEntity> ContactRequest { get; set; }
        #endregion
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region |       ModelBuilder Configuration      | 
            // Dinamik olarak entity'leri ekliyor
            //builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            // Manuel ekleme

            builder.ApplyConfiguration(new ApiUsageLogConfiguration());
            builder.ApplyConfiguration(new ClientAllowedIpConfiguration());
            builder.ApplyConfiguration(new ClientCredentialConfiguration());
            builder.ApplyConfiguration(new ClientRateLimitPolicyConfiguration());
            builder.ApplyConfiguration(new ContactRequestConfiguration());
            builder.ApplyConfiguration(new LoginHistoryConfiguration());
            builder.ApplyConfiguration(new PermissionConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new RolePermissionConfiguration());
            builder.ApplyConfiguration(new TokenBlacklistConfiguration());
            builder.ApplyConfiguration(new UserRefreshTokenConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());

            #endregion

            // foreignkey kaybı varsa, verileri manuel silme ayarı!
            var cascadeFks = builder.Model.GetEntityTypes()
                                          .SelectMany(t => t.GetForeignKeys())
                                          .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            //burada manuel işareti veriyorum.
            foreach (var fk in cascadeFks)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.ApplyGlobalEntityConfigurations();
            base.OnModelCreating(builder);
        }
    }
}
