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
using ResearchRepository.Domain.Contacts.Entities;

namespace ResearchRepository.Infrastructure.ResearchGroups.EntityMappings
{
    public class ResearchGroupMap : IEntityTypeConfiguration<ResearchGroup>
    {
        /// <summary>
        ///  Maps ResearchGroup entity to table "ResearchGroup"
        ///  Author: Nelson Alvarez
        ///  StoryID: ST-MM-3.1
        /// </summary>
        /// <param name="builder">ResearchGroup EntityTypeBuilder</param>
        public void Configure(EntityTypeBuilder<ResearchGroup> builder)
        {
            builder.ToTable("ResearchGroup");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasConversion(r => r.Value, s => RequiredString.TryCreate(s,200).Success());

            builder.Property(g => g.Description)
                    .HasMaxLength(8000)
                    .HasColumnType("varchar()");

            builder.Property(g => g.ImageName)
                    .HasColumnType("nvarchar(max)");

            builder.Property(g => g.CreationDate)
                    .HasColumnType("date");

            builder.Property(g => g.Active)
                    .HasColumnType("bit");

            builder.Property(g => g.AdminEmail)
                    .HasColumnType("nvarchar(60)");

            builder.HasMany(g => g.Contacts).WithOne(c => c.Group!).OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(g => g.News).WithOne(n => n.Group!).OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(g => g.ResearchAreas).WithMany(a => a.ResearchGroups)
                .UsingEntity(j => j.ToTable("ResearchAreaResearchGroup"));

            builder.HasQueryFilter(g => !g.Deleted);
        }
    }
}
