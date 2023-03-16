using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.PublicationContext;
using ResearchRepository.Domain.Theses.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResearchRepository.Infrastructure.PublicationContext.EntityMappings
{
    public class PublicationPartOfTesisMap : IEntityTypeConfiguration<PublicationPartOfTesis>
    {
        public void Configure(EntityTypeBuilder<PublicationPartOfTesis> builder)
        {
            builder.ToTable("PublicationPartOfTesis");
            builder.HasKey(p => new { p.PublicationId, p.ThesisId });
            builder.HasOne<Publication>(tp => tp.publication)
               .WithMany(p => p.PublicationPartOfTesis)
               .HasForeignKey(tp => tp.PublicationId);
        }
    }
}
