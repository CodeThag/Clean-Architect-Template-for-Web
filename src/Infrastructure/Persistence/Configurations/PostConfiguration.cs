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
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.CoverImagePath).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Views).IsRequired();
        }
    }
}
