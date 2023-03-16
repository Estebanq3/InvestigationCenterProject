using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.ResearchNews.Entities;

namespace ResearchRepository.Infrastructure.ResearchNews.EntityMappings
{
    public class NewsMap : IEntityTypeConfiguration<News>
    {
        /// <summary>
        ///  Maps News entity to table "News"
        ///  Author: Rodrigo Contreras
        ///  StoryID: ST-MM-6.1
        /// </summary>
        /// <param name="builder">News EntityTypeBuilder</param>
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.ToTable("News");
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Title).HasConversion(r => r.Value, s => RequiredString.TryCreate(s,200).Success());
            builder.Property(n => n.Description).HasConversion(r => r.Value, s => RequiredString.TryCreate(s,8000).Success()); ;
            builder.Property(n => n.CreationDate)
                .HasColumnType("datetime");
            builder.Property(n => n.PublicationDate)
                .HasColumnType("datetime");
            builder.Property(n => n.EndDate)
                .HasColumnType("datetime");
            builder.Property(n => n.DocumentURN)
                .HasColumnType("nvarchar(100)");
            builder.Property(n => n.VideoURL)
               .HasColumnType("nvarchar(100)");
            builder.Property(g => g.Deleted)
               .HasColumnType("bit");
            builder.HasMany(i => i.AssociatedImages).WithOne(n => n.News);
            builder.HasOne(i => i.MainImage)
                .WithOne().HasForeignKey(typeof(News),nameof(News.MainImageId))
                .IsRequired(false);
            builder.HasMany(p => p.AssociatedPeople)
                .WithMany(n => n.AssociatedNews)
                .UsingEntity(r => r.ToTable("NewsPerson"));
        }
    }
}
