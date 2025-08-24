using Microsoft.EntityFrameworkCore;
using PATCHUB.AuthServer.Domain.Common.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Persistence.Configurations.Base
{
    public static class BaseConfiguration
    {
        public static void ApplyGlobalEntityConfigurations(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(AuditableEntity.CreateDate))
                        .HasDefaultValueSql("GETUTCDATE()");
                }
            }
        }
    }
}
