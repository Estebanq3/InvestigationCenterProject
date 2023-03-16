using ResearchRepository.Infrastructure.InvestigationProjects.EntityMappings;
using ResearchRepository.Domain.InvestigationProjects.Entities;
using ResearchRepository.Infrastructure.Core;

using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ResearchRepository.Infrastructure.InvestigationProjects
{
    public class InvestigationProjectsDbContext : ApplicationDbContext
    {
        public InvestigationProjectsDbContext(DbContextOptions<InvestigationProjectsDbContext> options, ILogger<InvestigationProjectsDbContext> logger) : base(options, logger)
        {
        }

        public DbSet<InvestigationProject> Projects { get; set; } = null!;
        public DbSet<ProjectsImages> ProjectsImage { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new InvestigationProjectsMap());
            modelBuilder.ApplyConfiguration(new ProjectsImagesMap());
        }

    }
}