using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.StateId).IsRequired();
            builder.Property(x => x.Flag).HasDefaultValue(1);
            builder.Property(x => x.Latitude).HasColumnType("decimal(10,8)");
            builder.Property(x => x.Longitude).HasColumnType("decimal(11,8)");
        }
    }
}
