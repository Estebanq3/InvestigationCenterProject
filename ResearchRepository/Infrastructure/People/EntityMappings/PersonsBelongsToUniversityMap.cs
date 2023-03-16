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
    public class PersonsBelongsToUniversityMap : IEntityTypeConfiguration<PersonsBelongsToUniversity>
    {

        public void Configure(EntityTypeBuilder<PersonsBelongsToUniversity> builder)
        {
            builder.ToTable("PersonsBelongsToUniversity");
            builder.HasKey(c => new { c.PersonEmail, c.UniversityName });
            builder.HasOne(e => e.university)
            .WithMany(s => s.personsBelongsToUniversityList)
            .HasForeignKey(e => e.UniversityName);
        }
    }
}
