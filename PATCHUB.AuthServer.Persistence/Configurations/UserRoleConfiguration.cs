using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PATCHUB.AuthServer.Domain.Entities;

namespace PATCHUB.AuthServer.Persistence.Configurations
{

    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
        {
            builder.ToTable("USER_ROLE");

            builder.Property(x => x.IDUser).IsRequired();
            builder.Property(x => x.IDRole).IsRequired();
            builder.HasOne(x => x.Role).WithMany(r => r.UserRoles).HasForeignKey(x => x.IDRole).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
