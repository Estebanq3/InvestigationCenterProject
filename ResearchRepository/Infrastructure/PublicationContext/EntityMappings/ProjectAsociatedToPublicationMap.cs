using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.PublicationContext;
using ResearchRepository.Domain.Theses.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResearchRepository.Infrastructure.PublicationContext.EntityMappings
{
    public class ProjectAsociatedToPublicationMap : IEntityTypeConfiguration<ProjectAsociatedToPublication>
    {
        public void Configure(EntityTypeBuilder<ProjectAsociatedToPublication> builder)
        {
            builder.ToTable("ProjectAsociatedToPublication");
            builder.HasKey(p => new { p.PublicationId, p.InvestigationProjectId });
            builder.HasOne<Publication>(tp => tp.publication)
               .WithMany(p => p.ProjectAsociatedToPublication);
        }
    }
}
