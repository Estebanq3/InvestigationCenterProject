using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.ResearchAreas.Entities;
using ResearchRepository.Domain.Core.Helpers;

namespace ResearchRepository.Infrastructure.ResearchAreas.EntityMappings
{
    public class ResearchAreaMap : IEntityTypeConfiguration<ResearchArea>
    {
        public void Configure(EntityTypeBuilder<ResearchArea> builder)
        {
            builder.ToTable("ResearchArea")
                    .HasKey(a => a.Id);

            builder.Property(a => a.Name)
                    .HasMaxLength(100)
                    .IsRequired()
                    .HasConversion(c => c.Value, s => RequiredString.TryCreate(s,100).Success());

            builder.Property(a => a.Description)
                    .HasMaxLength(8000);

            // Recursive Many to Many relation of Areas to Subareas
            builder.HasMany(a => a.ResearchSubAreas).WithMany(s => s.ResearchAreas)
                    .UsingEntity(j => j.ToTable("ResearchAreaResearchSubArea"));
            // Many to Many relation of Subareas to Groups
            builder.HasMany(a => a.ResearchGroups).WithMany(g => g.ResearchAreas)
                    .UsingEntity(j => j.ToTable("ResearchAreaResearchGroup"));

            // Many to Many relation of Subareas to Groups
           // builder.HasMany(a => a.Publications).WithMany(g => g.ResearchAreas)
                  //  .UsingEntity(j => j.ToTable("ResearchAreaPublication"));
        }
    }
}
