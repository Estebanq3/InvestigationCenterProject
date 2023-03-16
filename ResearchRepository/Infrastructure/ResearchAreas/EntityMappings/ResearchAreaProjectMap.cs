using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.InvestigationProjects.Entities;
using ResearchRepository.Domain.ResearchAreas.Entities;

namespace ResearchRepository.Infrastructure.ResearchAreas.EntityMappings
{
    public class ResearchAreaProjectMap : IEntityTypeConfiguration<ResearchAreaProject>
    {
        public void Configure(EntityTypeBuilder<ResearchAreaProject> builder)
        {
            builder.ToTable("ResearchAreaProject");
            builder.HasKey(p => new { p.ResearchAreasId, p.ProjectsId});
            builder.HasOne<ResearchArea>(ap => ap.researchArea)
               .WithMany(p => p.ResearchAreaProject)
               .HasForeignKey(ap => ap.ResearchAreasId);
        }
    }
}