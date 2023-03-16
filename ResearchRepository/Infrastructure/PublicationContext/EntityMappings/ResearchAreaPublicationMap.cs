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
using ResearchRepository.Domain.PublicationContext;
using ResearchRepository.Domain.ResearchAreas.Entities;

namespace ResearchRepository.Infrastructure.PublicationContext.EntityMappings
{
    public class ResearchAreaPublicationMap : IEntityTypeConfiguration<ResearchAreaPublication>
    {
        public void Configure(EntityTypeBuilder<ResearchAreaPublication> builder)
        {
            builder.ToTable("ResearchAreaPublication");
            builder.HasKey(p => new { p.PublicationsId, p.ResearchAreasId });
            builder.HasOne<Publication>(ap => ap.publication)
               .WithMany(p => p.ResearchAreaPublication)
               .HasForeignKey(ap => ap.PublicationsId);
        }
    }
}
