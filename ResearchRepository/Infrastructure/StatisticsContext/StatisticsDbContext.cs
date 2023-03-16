using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResearchRepository.Domain.PublicationContext;
using ResearchRepository.Domain.StatisticsContext;
using ResearchRepository.Infrastructure.StatisticsContext.EntityMappings;
using ResearchRepository.Infrastructure.Core;
using ResearchRepository.Infrastructure.StatisticsContext.EntityMappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResearchRepository.Infrastructure.ResearchAreas.EntityMappings;
using ResearchRepository.Domain.ResearchAreas.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Infrastructure.ResearchGroups.EntityMappings;
using ResearchRepository.Domain.People.Entities;

namespace ResearchRepository.Infrastructure
{
    public partial class StatisticsDbContext : ApplicationDbContext
    {
        public StatisticsDbContext(DbContextOptions<StatisticsDbContext> options, ILogger<StatisticsDbContext> logger) : base(options, logger)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }

        public virtual DbSet<Statistic> Publication { get; set; }
        public DbSet<ResearchArea> Areas { get; set; } = null!;
        public DbSet<ResearchGroup> ResearchGroup { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<Person>();
            modelBuilder.Ignore<AcademicProfile>();
            modelBuilder.ApplyConfiguration(new StatisticMap());
            modelBuilder.ApplyConfiguration(new ResearchAreaMap());
            modelBuilder.ApplyConfiguration(new ResearchGroupMap());
        }

    }
}
