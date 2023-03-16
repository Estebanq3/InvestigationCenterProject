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
    public class AccountIsAdministratorOfGroupMap : IEntityTypeConfiguration<AccountIsAdministratorOfGroup>
    {

        public void Configure(EntityTypeBuilder<AccountIsAdministratorOfGroup> builder)
        {
            builder.ToTable("AccountIsAdministratorOfGroup");
            builder.HasKey(c => new { c.Email, c.GroupId });
        }

    }
}
