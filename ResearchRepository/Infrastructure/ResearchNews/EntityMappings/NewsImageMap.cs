using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResearchRepository.Domain.ResearchNews.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Infrastructure.Images.EntityMappings
{
    public class NewsImageMap : IEntityTypeConfiguration<NewsImage>
    {
        public void Configure(EntityTypeBuilder<NewsImage> builder)
        {
            builder.ToTable("NewsImage")
                .HasKey(i => i.Id);
            builder.Property(i => i.Path)
                .IsRequired()
                .HasColumnType("nvarchar(MAX)");
        }
    }
}
