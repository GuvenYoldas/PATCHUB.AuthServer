using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PATCHUB.AuthServer.Domain.Entities;

namespace PATCHUB.AuthServer.Persistence.Configurations
{
    public class ClientAllowedIpConfiguration : IEntityTypeConfiguration<ClientAllowedIpEntity>
    {
        public void Configure(EntityTypeBuilder<ClientAllowedIpEntity> builder)
        {
            builder.ToTable("CLIENT_ALLOWED_IP");

            builder.Property(x => x.IpAddress)
                   .IsRequired()
                   .HasMaxLength(45); // IPv6 destekleniyorsa 45 karakter yeterlidir

            builder.HasOne(x => x.Policy)
                   .WithMany(p => p.AllowedIps)
                   .HasForeignKey(x => x.IDPolicy)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
