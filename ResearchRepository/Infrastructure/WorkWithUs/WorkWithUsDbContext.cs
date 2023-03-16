using ResearchRepository.Infrastructure.People.EntityMappings;
using ResearchRepository.Domain.WorkWithUs.Entities;
using ResearchRepository.Infrastructure.Core;


using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ResearchRepository.Infrastructure.WorkWithUs
{
    public class WorkWithUsDbContext : ApplicationDbContext
    {
        public WorkWithUsDbContext(DbContextOptions<WorkWithUsDbContext> options, ILogger<WorkWithUsDbContext> logger) : base(options, logger)
        {
        }


        public DbSet<WorkInfo> WorkInfo { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.ApplyConfiguration(new WorkInfoMap());
          
        }
    }
}