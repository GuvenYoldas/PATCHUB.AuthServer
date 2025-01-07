using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PATCHUB.AuthServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Persistence.Configurations
{
     class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("USER", "dbo");
            builder.HasKey(x => x.ID);

            builder.HasIndex(x => x.IdentityNumber).IsUnique();
            builder.HasIndex(x => x.Mail).IsUnique();
            builder.HasIndex(x => x.PhoneNo).IsUnique();


            builder.Property(x => x.IdentityNumber).IsRequired();
            builder.Property(x => x.Mail).IsRequired();
            builder.Property(x => x.PhoneNo).IsRequired();

            builder.Property(x => x.Balance).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.UserType).HasDefaultValue(100);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.UserName);

            builder.Property(x => x.CountryCode);
            builder.Property(x => x.AddressBill);
            builder.Property(x => x.AddressShipping);
            builder.Property(x => x.AvatarUrl);

            builder.Property(x => x.SaltString).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.ActivatorKey);
            builder.Property(x => x.ReferenceUser);


        }


    }
}

