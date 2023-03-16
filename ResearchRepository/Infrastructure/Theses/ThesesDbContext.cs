using ResearchRepository.Infrastructure.Theses.EntityMappings;
using ResearchRepository.Infrastructure.PublicationContext.EntityMappings;
using ResearchRepository.Domain.Theses.Entities;
using ResearchRepository.Infrastructure.Core;

using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Infrastructure.ResearchAreas.EntityMappings;
using ResearchRepository.Domain.ResearchAreas.Entities;

namespace ResearchRepository.Infrastructure.Theses

{
    public class ThesesDbContext : ApplicationDbContext
    {
        public ThesesDbContext(DbContextOptions<ThesesDbContext> options, ILogger<ThesesDbContext> logger) : base(options, logger)
        {
        }

        public DbSet<Thesis> Theses { get; set; } = null!;
        public DbSet<ThesisPartOfProject> ThesisPartOfProject { get; set; }

        public DbSet<ResearchAreaThesis> ResearchAreaThesis { get; set; }
        //Debugger
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ThesesMap());
            modelBuilder.ApplyConfiguration(new ThesisPartOfProjectMap());
            modelBuilder.ApplyConfiguration(new ResearchAreaThesesMap());
        }

    }
}