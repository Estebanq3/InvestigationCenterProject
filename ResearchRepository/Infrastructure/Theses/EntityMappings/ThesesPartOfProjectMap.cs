using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.InvestigationProjects;
using ResearchRepository.Domain.Theses.Entities;
using ResearchRepository.Domain.InvestigationProjects.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResearchRepository.Infrastructure.Theses.EntityMappings
{
    public class ThesisPartOfProjectMap: IEntityTypeConfiguration<ThesisPartOfProject>
    {
        public void Configure(EntityTypeBuilder<ThesisPartOfProject> builder)
        {

            builder.ToTable("ThesisPartOfProject");
            builder.HasKey(t => new { t.InvestigationProjectId, t.ThesisId});
            builder.HasOne<Thesis>(tp => tp.Thesis)
               .WithMany(t => t.ThesisPartOfProject)
               .HasForeignKey(tp => tp.ThesisId);
        }


    }
}
