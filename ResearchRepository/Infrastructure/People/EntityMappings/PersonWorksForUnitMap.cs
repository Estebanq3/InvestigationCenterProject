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
    public class PersonWorksForUnitMap : IEntityTypeConfiguration<PersonWorksForUnit>
    {

        public void Configure(EntityTypeBuilder<PersonWorksForUnit> builder)
        {
            builder.ToTable("PersonWorksForUnit");
            builder.HasKey(c => new { c.Email, c.UnitName });
            builder.HasOne<AcademicUnit>(sc => sc._academicUnit)
            .WithMany(s => s._personWorksForUnit)
            .HasForeignKey(sc => sc.UnitName);
        }
    }
}
