using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ResearchRepository.Domain.People.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResearchRepository.Infrastructure.People.EntityMappings
{
    public class AcademicUnitMap : IEntityTypeConfiguration<AcademicUnit>
    {

        public void Configure(EntityTypeBuilder<AcademicUnit> builder)
        {
            builder.ToTable("AcademicUnit");
            builder.HasKey(e => e.Name);
        }
    
    }
}
