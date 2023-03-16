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
    public class InvestigatorMap : IEntityTypeConfiguration<Investigator>
    {

        public void Configure(EntityTypeBuilder<Investigator> builder)
        {
            builder.ToTable("Investigator");
        }
    }
}
