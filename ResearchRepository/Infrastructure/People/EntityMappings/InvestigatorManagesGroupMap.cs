//InvestigatorManagesGroup
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.People.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResearchRepository.Infrastructure.People.EntityMappings
{
    public class InvestigatorManagesGroupMap : IEntityTypeConfiguration<InvestigatorManagesGroup>
    {

        public void Configure(EntityTypeBuilder<InvestigatorManagesGroup> builder)
        {
            builder.ToTable("InvestigatorManagesGroup");
            builder.HasKey(e => new { e.Email, e.GroupId });
            builder.Property(e => e.StartDate).HasColumnType("date").IsRequired(false);
            builder.Property(e => e.EndDate).HasColumnType("date").IsRequired(false);
            builder
                .HasOne<Investigator>(e => e.Investigator)
                .WithMany(s => s.InvestigatorManagesGroups)
                .HasForeignKey(e => e.Email);
        }
    }
}
