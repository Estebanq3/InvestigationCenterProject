using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.StatisticsContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResearchRepository.Domain.StatisticsContext;

namespace ResearchRepository.Infrastructure.StatisticsContext.EntityMappings
{
    public class StatisticMap : IEntityTypeConfiguration<Statistic>
    {
        public void Configure(EntityTypeBuilder<Statistic> builder)
        {

            builder.HasKey(e => new { e.Id });

            builder.Property(e => e.Year)
                .HasColumnType("date")
                .HasColumnName("Year");

            builder.Property(e => e.TypePublication)
                .HasMaxLength(100)
                .HasColumnName("TypePublication");

            builder.Property(e => e.ResearchGroupId)
                .HasColumnName("ResearchGroupId");

            builder.Property(e => e.Deleted)
              .HasColumnName("Deleted");

            builder.HasMany(e => e.ResearchAreas).WithMany(a => a.Publications)
                .UsingEntity(j => j.ToTable("ResearchAreaPublication"));
        }

    }
}
