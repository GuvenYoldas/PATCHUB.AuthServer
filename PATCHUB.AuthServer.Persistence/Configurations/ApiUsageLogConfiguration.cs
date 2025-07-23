using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PATCHUB.AuthServer.Domain.Entities;

namespace PATCHUB.AuthServer.Persistence.Configurations
{
    public class ApiUsageLogConfiguration : IEntityTypeConfiguration<ApiUsageLogEntity>
    {
        public void Configure(EntityTypeBuilder<ApiUsageLogEntity> builder)
        {
            builder.ToTable("API_USAGE_LOG");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.Method).IsRequired().HasMaxLength(10);
            builder.Property(x => x.Action).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Endpoint).IsRequired().HasMaxLength(200);
            builder.Property(x => x.IP).HasMaxLength(45);
            builder.Property(x => x.ExtraDataJson).HasMaxLength(2000);
            builder.Property(x => x.Timestamp).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
