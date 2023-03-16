using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResearchRepository.Domain.PublicationContext;

namespace ResearchRepository.Infrastructure.PublicationContext.EntityMappings
{
    public class PublicationMap:IEntityTypeConfiguration<Publication>
    {
        public void Configure(EntityTypeBuilder<Publication> builder) {

            builder.HasKey(e => new { e.Id });
            
            builder.Property(e => e.Name)
                .HasMaxLength(500)
                .HasColumnName("Name");

            builder.Property(e => e.Summary)
                .HasMaxLength(1000)
                .HasColumnName("Summary");

            builder.Property(e => e.Year)
                .HasColumnType("date")
                .HasColumnName("Year");

            builder.Property(e => e.TypePublication)
                .HasMaxLength(100)
                .HasColumnName("TypePublication");

            builder.Property(e => e.JournalConference)
                .HasMaxLength(100)
                .HasColumnName("JournalConference");

            builder.Property(e => e.ResearchGroupId)
                .HasColumnName("ResearchGroupId");

            builder.Property(e => e.Image)
                .HasColumnName("Image");

            builder.Property(e => e.Deleted)
                .HasColumnName("Deleted");



            //builder.HasMany(e => e.ResearchAreas).WithMany(a => a.Publications)
              //  .UsingEntity(j => j.ToTable("ResearchAreaPublication"));

        }

    }
}
