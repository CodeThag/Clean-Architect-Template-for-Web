using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class SystemNotificationConfiguration : IEntityTypeConfiguration<SystemNotification>
    {
        public void Configure(EntityTypeBuilder<SystemNotification> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Recipient).IsRequired();
            builder.Property(x => x.Sender).IsRequired();
            builder.Property(x => x.Subject).IsRequired();
            builder.Property(x => x.Message).IsRequired();
            builder.Property(x => x.Read).IsRequired();
        }
    }
}
