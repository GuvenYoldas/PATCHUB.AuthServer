using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PATCHUB.AuthServer.Domain.Entities;

namespace PATCHUB.AuthServer.Persistence.Configurations
{
    public class LoginHistoryConfiguration : IEntityTypeConfiguration<LoginHistoryEntity>
    {
        public void Configure(EntityTypeBuilder<LoginHistoryEntity> builder)
        {
            builder.ToTable("LOGIN_HISTORY");

            builder.Property(x => x.IDUser).IsRequired();
            builder.Property(x => x.IP).HasMaxLength(45);
            builder.Property(x => x.UserAgent).HasMaxLength(500);

            
        }
    }
}
