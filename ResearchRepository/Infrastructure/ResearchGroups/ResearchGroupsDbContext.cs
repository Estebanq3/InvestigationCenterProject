using ResearchRepository.Infrastructure.ResearchGroups.EntityMappings;
using ResearchRepository.Infrastructure.ResearchAreas.EntityMappings;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.ResearchAreas.Entities;
using ResearchRepository.Infrastructure.Core;

using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ResearchRepository.Domain.ResearchNews.Entities;
using ResearchRepository.Infrastructure.ResearchNews.EntityMappings;
using ResearchRepository.Domain.Contacts.Entities;
using ResearchRepository.Infrastructure.Contacts.EntityMappings;
using ResearchRepository.Infrastructure.Images.EntityMappings;
using ResearchRepository.Infrastructure.People.EntityMappings;

namespace ResearchRepository.Infrastructure.ResearchGroups
{
    public class ResearchGroupsDbContext : ApplicationDbContext
    {
        public ResearchGroupsDbContext(DbContextOptions<ResearchGroupsDbContext> options, ILogger<ResearchGroupsDbContext> logger) : base(options, logger)
        {
        }

        public DbSet<ResearchCenter> Centers { get; set; } = null!;
        public DbSet<ResearchGroup> Groups { get; set; } = null!;
        public DbSet<ResearchArea> Areas { get; set; } = null!;
        public DbSet<News> News { get; set; } = null!;
        public DbSet<NewsImage> NewsImage {  get; set; } = null!;
        public DbSet<Contact> Contacts {get; set;} = null!;
        public DbSet<ResearchAreaProject> ResearchAreaProject { get; set; }
        /// <summary>
        /// Applies configuration of Research Centers and Groups Mappings
        /// Author: Nelson Alvarez
        /// StoryID: ST-MM-3.1
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ResearchGroupMap());
            modelBuilder.ApplyConfiguration(new ResearchCenterMap());
            modelBuilder.ApplyConfiguration(new ResearchAreaMap());
            modelBuilder.ApplyConfiguration(new NewsMap());
            modelBuilder.ApplyConfiguration(new NewsImageMap());
            modelBuilder.ApplyConfiguration(new ContactsMap());
            //Important: Person, AcademicProfile and Subscriptions need to be mapped for News to Person N to N relation to work.
            modelBuilder.ApplyConfiguration(new PersonMap());
            modelBuilder.ApplyConfiguration(new AcademicProfileMap());
            modelBuilder.ApplyConfiguration(new SubscriptionsMap());
            modelBuilder.ApplyConfiguration(new ResearchAreaProjectMap());
        }

    }
}