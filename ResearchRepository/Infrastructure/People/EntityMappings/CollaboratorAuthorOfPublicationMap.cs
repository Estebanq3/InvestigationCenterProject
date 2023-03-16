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
    public class CollaboratorAuthorOfPublicationMap : IEntityTypeConfiguration<CollaboratorIsAuthorOfPublication>
    {
        public void Configure(EntityTypeBuilder<CollaboratorIsAuthorOfPublication> builder)
        {
            builder.ToTable("CollaboratorIsAuthorOfPublication");
            builder.HasKey(c => new { c.EmailCollaborator, c.IdPublication });

            ///builder.HasOne(p => p.publication)
                ///.WithMany(c => c.CollaboratorIsAuthorOfPublication)
                ///.HasForeignKey(sc => sc.IdPublication);

           /* builder.HasOne<Collaborator>(p => p.Collaborator)
                .WithMany(c => c.CollaboratorIsAuthorOfPublication)
                .HasForeignKey(sc => sc.EmailCollaborator);*/
            

        }
    }
}