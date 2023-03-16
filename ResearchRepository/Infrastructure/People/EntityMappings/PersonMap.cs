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
    public class PersonMap : IEntityTypeConfiguration<Person>
    {
      
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");
            builder.HasKey(e => e.Email);
            builder.Property(e => e.FirstName).IsRequired();
            builder.Property(e => e.FirstLastName).IsRequired();
            builder.Property(e => e.SecondLastName).IsRequired();
            builder.HasOne<AcademicProfile>(p => p.AcademicProfile).WithOne(a => a.Person).HasForeignKey<AcademicProfile>(p => p.Email);
            

        }
    }
}
