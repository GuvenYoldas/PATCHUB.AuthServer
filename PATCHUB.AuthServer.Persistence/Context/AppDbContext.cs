using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PATCHUB.AuthServer.Domain.Entities;
using PATCHUB.AuthServer.Persistence.Configurations;

namespace PATCHUB.AuthServer.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }

        #region |       DbSet Entity Classes        |

        public DbSet<UserEntity> User { get; set; }
        public DbSet<ClientCredentialEntity> ClientCredential { get; set; }
        public DbSet<ClientRateLimitPolicyEntity> ClientRateLimitPolicy { get; set; }
        public DbSet<ClientAllowedIpEntity> ClientAllowedIp { get; set; }

        #endregion
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region |       ModelBuilder Configuration      | 
            // Dinamik olarak entity'leri ekliyor
            //builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            // Manuel ekleme
            builder.ApplyConfiguration(new UserConfiguration());
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

            base.OnModelCreating(builder);
        }
    }
}
