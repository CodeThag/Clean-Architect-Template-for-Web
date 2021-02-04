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
   public class PaymentPurposeFixedLineItemConfiguration : IEntityTypeConfiguration<PaymentPurposeFixedLineItem>
    {
        public void Configure(EntityTypeBuilder<PaymentPurposeFixedLineItem> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Amount).IsRequired().HasPrecision(18, 2);
            builder.Property(x => x.PaymentPurposeId).IsRequired();
            builder.Property(x => x.Weight).IsRequired();
            builder.Property(x => x.IsTaxable).IsRequired();
            builder.Property(x => x.IsSelectable).IsRequired();
            builder.Property(x => x.Tax).HasPrecision(4, 2);
        }
    }
}
