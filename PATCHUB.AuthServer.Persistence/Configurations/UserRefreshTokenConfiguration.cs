using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PATCHUB.AuthServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PATCHUB.AuthServer.Persistence.Configurations
{
    class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshTokenEntity>
    {
        public void Configure(EntityTypeBuilder<UserRefreshTokenEntity> builder)
        {
            builder.ToTable("USER_REFRESH_TOKEN", "dbo");
            builder.HasNoKey();

            builder.Property(x => x.IDUser).IsRequired();
            builder.Property(x => x.Token).IsRequired().HasMaxLength(200);
            builder.Property(x => x.ExpirationDate).IsRequired();

        }
    }
}
