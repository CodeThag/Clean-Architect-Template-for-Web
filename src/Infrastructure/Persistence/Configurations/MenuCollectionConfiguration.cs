using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class MenuCollectionConfiguration : IEntityTypeConfiguration<MenuCollection>
    {
        public void Configure(EntityTypeBuilder<MenuCollection> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.DisplayName).IsRequired();
            builder.Property(x => x.SystemName).IsRequired();
            builder.Property(x => x.Weight).IsRequired();
        }
    }
}
