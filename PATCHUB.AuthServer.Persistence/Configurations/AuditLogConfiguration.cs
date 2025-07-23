using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PATCHUB.AuthServer.Domain.Entities;

namespace PATCHUB.AuthServer.Persistence.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLogEntity>
    {
        public void Configure(EntityTypeBuilder<AuditLogEntity> builder)
        {
            builder.ToTable("AUDIT_LOG");

            builder.Property(x => x.Action).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Endpoint).HasMaxLength(2000);
            builder.Property(x => x.IP).HasMaxLength(45);
        }
    }
}
