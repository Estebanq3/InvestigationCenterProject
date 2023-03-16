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
    public class AuthorPartOfThesisMap : IEntityTypeConfiguration<AuthorPartOfThesis>
    {

        public void Configure(EntityTypeBuilder<AuthorPartOfThesis> builder)
        {
            builder.ToTable("AuthorsPartOfThesis");
            builder.HasKey(c => new { c.CollaboratorEmail, c.ThesisId });
            builder.Property(c => c.Role).IsRequired();

            builder.HasOne<Collaborator>(sc => sc.Collaborator)
                .WithMany(s => s.AuthorPartOfThesis);
        }
    }
}