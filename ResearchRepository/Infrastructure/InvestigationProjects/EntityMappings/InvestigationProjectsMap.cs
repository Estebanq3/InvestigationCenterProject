using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.InvestigationProjects.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResearchRepository.Infrastructure.InvestigationProjects.EntityMappings
{
    public class InvestigationProjectsMap : IEntityTypeConfiguration<InvestigationProject>
    {
        public void Configure(EntityTypeBuilder<InvestigationProject> builder)
        {
            builder.ToTable("InvestigationProject");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(30);
            builder.Property(p => p.StartDate)
                   .IsRequired()
                   .HasColumnType("datetime");
            builder.Property(p => p.EndDate)
                   .IsRequired()
                   .HasColumnType("datetime");
            builder.Property(p => p.InvestigationGroupID)
                   .IsRequired();
            builder.Property(p => p.Description)
                   .IsRequired()
                   .HasMaxLength(8000);
            builder.Property(p => p.Summary)
                   .IsRequired()
                   .HasMaxLength(8000);
            builder.Property(p => p.Image)
                   .IsRequired();
        }
    }
}