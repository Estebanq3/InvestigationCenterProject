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
    public class CollaboratorPartOfGroupMap : IEntityTypeConfiguration<CollaboratorPartOfGroup>
    {

        public void Configure(EntityTypeBuilder<CollaboratorPartOfGroup> builder)
        {
            builder.ToTable("CollaboratorPartOfGroup");
            builder.HasKey(c => new { c.CollaboratorEmail, c.InvestigationGroupId });
            builder.Property(e => e.StartDate).HasColumnType("date").IsRequired(false);
            builder.Property(e => e.EndDate).HasColumnType("date").IsRequired(false);
            builder.HasOne<Collaborator>(sc => sc.Collaborator)
                .WithMany(s => s.CollaboratorPartOfGroups)
                .HasForeignKey(sc => sc.CollaboratorEmail);
        }
    }
}