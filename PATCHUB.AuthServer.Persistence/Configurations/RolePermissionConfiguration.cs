using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PATCHUB.AuthServer.Domain.Entities;


namespace PATCHUB.AuthServer.Persistence.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
    {
        public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
        {
            builder.ToTable("ROLE_PERMISSION");

            builder.HasKey(x => new { x.IDRole, x.IDPermission });

            builder.HasOne(x => x.Role)
                   .WithMany()
                   .HasForeignKey(x => x.IDRole)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Permission)
                   .WithMany()
                   .HasForeignKey(x => x.IDPermission)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
