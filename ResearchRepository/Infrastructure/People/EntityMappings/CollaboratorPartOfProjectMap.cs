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
    public class CollaboratorPartOfProjectMap : IEntityTypeConfiguration<CollaboratorPartOfProject>
    {

        public void Configure(EntityTypeBuilder<CollaboratorPartOfProject> builder)
        {
            builder.ToTable("CollaboratorPartOfProject");
            builder.HasKey(c => new { c.CollaboratorEmail, c.InvestigationProjectId });
            builder.Property(c => c.Role).IsRequired();

            builder.HasOne<Collaborator>(sc => sc.Collaborator)
                .WithMany(s => s.CollaboratorPartOProject);        
        }
    }
}