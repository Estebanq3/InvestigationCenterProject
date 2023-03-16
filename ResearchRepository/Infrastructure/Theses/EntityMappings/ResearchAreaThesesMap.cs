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
using ResearchRepository.Domain.Theses;
using ResearchRepository.Domain.Theses.Entities;

using ResearchRepository.Domain.ResearchAreas.Entities;

namespace ResearchRepository.Infrastructure.Theses.EntityMappings
{
    public class ResearchAreaThesesMap : IEntityTypeConfiguration<ResearchAreaThesis>
    {
        public void Configure(EntityTypeBuilder<ResearchAreaThesis> builder)
        {
            builder.ToTable("ResearchAreaThesis");
            builder.HasKey(p => new { p.ThesisId, p.ResearchAreasId
});
            builder.HasOne<Thesis>(ap => ap.thesis)
               .WithMany(p => p.ResearchAreaThesis)
               .HasForeignKey(ap => ap.ThesisId);
        }
    }
}