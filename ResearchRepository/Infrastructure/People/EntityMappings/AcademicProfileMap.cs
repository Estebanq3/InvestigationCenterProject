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
    public class AcademicProfileMap : IEntityTypeConfiguration<AcademicProfile>
    {

        public void Configure(EntityTypeBuilder<AcademicProfile> builder)
        {
            builder.ToTable("AcademicProfile");
            //builder.HasOne<Person>(a => a.Person).WithOne(p => p.AcademicProfile)
            //.HasForeignKey<AcademicProfile>(a => a.Email);
            builder.HasKey(e => e.Email);
            builder.Property(e => e.ProfilePic).IsRequired(false);
            builder.Property(e => e.Biography).IsRequired(false);
            builder.Property(e => e.Degree).IsRequired(false);
            builder.Property(e => e.FacebookLink).IsRequired(false);
            builder.Property(e => e.GitHubLink).IsRequired(false);
            builder.Property(e => e.LinkedInLink).IsRequired(false);
            builder.Property(e => e.Title).IsRequired(false);
            builder.Property(e => e.Tel).IsRequired(false);


        }
    }
}
