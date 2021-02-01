using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SendeYaz.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.DataAccess.Mappings.EF
{
    public class BlogTagMap : IEntityTypeConfiguration<BlogTag>
    {
        public void Configure(EntityTypeBuilder<BlogTag> builder)
        {
            builder.ToTable("BlogTags");

            builder.HasOne(x => x.Blog).WithMany(x => x.BlogTags).HasForeignKey(x => x.BlogId);

            builder.HasOne(x => x.Tag).WithMany(x => x.BlogTags).HasForeignKey(x => x.TagId);
        }
    }
}
