using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResearchRepository.Infrastructure.ResearchGroups.EntityMappings
{
    public class ResearchCenterMap : IEntityTypeConfiguration<ResearchCenter>
    {
        /// <summary>
        ///  Maps ResearchCenter entity to table "ResearchCenter"
        ///  Author: Nelson Alvarez
        ///  StoryID: ST-MM-3.1
        /// </summary>
        /// <param name="builder">ResearchCenter EntityTypeBuilder</param>
        public void Configure(EntityTypeBuilder<ResearchCenter> builder)
        {
            builder.ToTable("ResearchCenter");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(200)
                   .HasConversion(r => r.Value, s => RequiredString.TryCreate(s, 200).Success());
            builder.Property(c => c.Abbreviation)
                   .HasMaxLength(10)
                   .HasColumnType("varchar");
            builder.Property(c => c.Description)
                    .HasMaxLength(8000)
                    .HasColumnType("varchar");
            builder.Property(c => c.ImageName)
                    .HasMaxLength(20)
                    .HasColumnType("varchar");
            builder.HasMany(c => c.ResearchGroups).WithOne(g => g.Center!);
        }
    }
}