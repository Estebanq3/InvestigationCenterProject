using ResearchRepository.Infrastructure.People.EntityMappings;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Infrastructure.Core;
using ResearchRepository.Domain.ResearchGroups.Entities;

using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ResearchRepository.Infrastructure.People
{
    public class AccountInfoDbContext : ApplicationDbContext
    {
        public AccountInfoDbContext(DbContextOptions<AccountInfoDbContext> options, ILogger<AccountInfoDbContext> logger) : base(options, logger)
        {
        }


        public DbSet<Person> Person { get; set; }
        public DbSet<AcademicProfile> AcademicProfile { get; set; }
        public DbSet<Domain.People.Entities.Subscriptions> Subscriptions { get; set; }
     

    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<ResearchGroup>(); // Important
            modelBuilder.ApplyConfiguration(new PersonMap());
            modelBuilder.ApplyConfiguration(new AcademicProfileMap());
            modelBuilder.ApplyConfiguration(new SubscriptionsMap());
        }
    }
}