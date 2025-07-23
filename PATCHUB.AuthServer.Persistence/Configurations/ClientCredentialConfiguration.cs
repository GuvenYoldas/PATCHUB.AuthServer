using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PATCHUB.AuthServer.Domain.Entities;

namespace PATCHUB.AuthServer.Persistence.Configurations
{
    public class ClientCredentialConfiguration : IEntityTypeConfiguration<ClientCredentialEntity>
    {
        public void Configure(EntityTypeBuilder<ClientCredentialEntity> builder)
        {
            builder.ToTable("CLIENT_CREDENTIAL");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.IDClient).IsRequired();
            
            builder.Property(x => x.SecretHash).IsRequired().HasMaxLength(512);
            builder.Property(x => x.RequestLimit).IsRequired();
            builder.Property(x => x.RequestCount).IsRequired();
            builder.Property(x => x.ExpirationDate).IsRequired();

            builder.HasIndex(x => new { x.IDClient, x.SecretHash }).IsUnique();
        }
    }
}
