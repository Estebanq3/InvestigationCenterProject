using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ResearchRepository.Domain.Authorization.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResearchRepository.Infrastructure.Authorization.EntityMappings
{
    public class AccountIsCollaboratorOfGroupMap : IEntityTypeConfiguration<AccountIsCollaboratorOfGroup>
    {

        public void Configure(EntityTypeBuilder<AccountIsCollaboratorOfGroup> builder)
        {
            builder.ToTable("AccountIsCollaboratorOfGroup");
            builder.HasKey(c => new { c.Email, c.GroupId });
        }

    }
}
    