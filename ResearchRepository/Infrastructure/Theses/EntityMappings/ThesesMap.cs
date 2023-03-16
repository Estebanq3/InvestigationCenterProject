using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.Theses.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResearchRepository.Infrastructure.Theses.EntityMappings
{
    public class ThesesMap : IEntityTypeConfiguration<Thesis>
    {
        public void Configure(EntityTypeBuilder<Thesis> builder)
        {
            builder.ToTable("Thesis");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(300);
            builder.Property(t => t.PublicationDate)
                   .IsRequired()
                   .HasColumnType("PublicationDate");
            builder.Property(t => t.Summary)
                   .IsRequired()
                   .HasColumnType("Summary")
                   .HasMaxLength(8000);
            builder.Property(t => t.InvestigationGroupId)
                  .IsRequired();
            builder.Property(t => t.Image)
                   .IsRequired();
            builder.Property(t => t.DOI)
                   .IsRequired()
                   .HasColumnType("DOI")
                   .HasMaxLength(300);
            builder.Property(t => t.Type)
                   .IsRequired()
                   .HasColumnType("Type")
                   .HasMaxLength(30);
            builder.Property(t => t.Reference)
                   .IsRequired()
                   .HasColumnType("Reference")
                   .HasMaxLength(500);
        }
    }
}