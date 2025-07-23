using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PATCHUB.AuthServer.Domain.Entities;

namespace PATCHUB.AuthServer.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("ROLE");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(250);


            // Client FK
            builder.HasOne(x => x.Client)
                .WithMany()
                .HasForeignKey(x => x.IDClient)
                .OnDelete(DeleteBehavior.Restrict); // Client silinirse rol silinmesin

            // Index (Opsiyonel ama önerilir)
            builder.HasIndex(x => new { x.IDClient, x.Name })
                   .IsUnique(); // Aynı client içinde aynı rol ismi bir kez tanımlanmalı
        }
    }
}
