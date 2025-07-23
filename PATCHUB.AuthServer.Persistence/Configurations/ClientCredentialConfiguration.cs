using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PATCHUB.AuthServer.Domain.Entities;

namespace PATCHUB.AuthServer.Persistence.Configurations
{
    public class ClientCredentialConfiguration : IEntityTypeConfiguration<ClientCredential>
    {
        public void Configure(EntityTypeBuilder<ClientCredential> builder)
        {
            builder.ToTable("CLIENT_CREDENTIAL");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.IDClient).IsRequired();
            
            builder.Property(x => x.SecretHash).IsRequired().HasMaxLength(200);
            builder.Property(x => x.RequestLimit).IsRequired();
            builder.Property(x => x.RequestCount).IsRequired();
            builder.Property(x => x.ExpirationDate).IsRequired();
        }
    }
}
