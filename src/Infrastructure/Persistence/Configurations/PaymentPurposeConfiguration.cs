using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PaymentPurposeConfiguration : IEntityTypeConfiguration<PaymentPurpose>
    {
        public void Configure(EntityTypeBuilder<PaymentPurpose> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.PaymentTypeId).IsRequired();
            builder.Property(x => x.SystemName).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
        }
    }
}
