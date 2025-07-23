using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PATCHUB.AuthServer.Domain.Entities;

namespace PATCHUB.AuthServer.Persistence.Configurations
{
    public class TokenBlacklistConfiguration : IEntityTypeConfiguration<TokenBlacklistEntity>
    {
        public void Configure(EntityTypeBuilder<TokenBlacklistEntity> builder)
        {
            builder.ToTable("TOKEN_BLACKLIST");

            builder.Property(x => x.Token).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.Reason).HasMaxLength(200);
            builder.Property(x => x.RevokedAt).HasDefaultValueSql("GETUTCDATE()");

        }
    }
}
