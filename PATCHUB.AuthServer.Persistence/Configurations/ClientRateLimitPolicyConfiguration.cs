using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PATCHUB.AuthServer.Domain.Entities;

namespace PATCHUB.AuthServer.Persistence.Configurations
{
    public class ClientRateLimitPolicyConfiguration : IEntityTypeConfiguration<ClientRateLimitPolicyEntity>
    {
        public void Configure(EntityTypeBuilder<ClientRateLimitPolicyEntity> builder)
        {
            builder.ToTable("CLIENT_RATE_LIMIT_POLICY");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.IDClient).IsRequired();
            builder.Property(x => x.MaxRequestsPerMinute).IsRequired();
            builder.Property(x => x.MaxRequestsPerHour).IsRequired();
            builder.Property(x => x.MaxRequestsPerDay).IsRequired();
            builder.Property(x => x.LastUpdated).IsRequired();

            builder.HasOne(x => x.Client)
                   .WithMany(c => c.RateLimitPolicies)
                   .HasForeignKey(x => x.IDClient)
                   .HasPrincipalKey(c => c.IDClient);

            builder.HasMany(x => x.AllowedIps)
                   .WithOne(x => x.Policy)
                   .HasForeignKey(x => x.IDPolicy);
        }
    }
}
