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
    class ContactRequestConfiguration : IEntityTypeConfiguration<ContactRequestEntity>
    {
        public void Configure(EntityTypeBuilder<ContactRequestEntity> builder)
        {
            builder.ToTable("CONTACT_REQUEST", "dbo");
            builder.HasNoKey();

            builder.Property(x => x.FullName).IsRequired(false);
            builder.Property(x => x.Mail).IsRequired();
            builder.Property(x => x.MessageText).IsRequired(false);
            builder.Property(x => x.Location).IsRequired(false);
            builder.Property(x => x.CreatedDate);
        }
    }
}
