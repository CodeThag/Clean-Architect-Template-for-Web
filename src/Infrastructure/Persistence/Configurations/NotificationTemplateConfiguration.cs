
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class NotificationTemplateConfiguration : IEntityTypeConfiguration<NotificationTemplate>
    {
        public void Configure(EntityTypeBuilder<NotificationTemplate> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.SystemName).IsRequired();
            builder.HasIndex(x => x.SystemName).IsUnique();

            builder.Property(x => x.SendSystemNotification).IsRequired();
            builder.Property(x => x.SendEmailTemplate).IsRequired();
            builder.Property(x => x.SendSMSTemplate).IsRequired();

            builder.Property(x => x.Subject).IsRequired();
            builder.Property(x => x.SystemTemplate).IsRequired();
        }
    }
}
