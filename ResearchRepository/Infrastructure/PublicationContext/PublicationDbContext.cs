using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResearchRepository.Domain.PublicationContext;
using ResearchRepository.Infrastructure.PublicationContext.EntityMappings;
using ResearchRepository.Infrastructure.Core;
using ResearchRepository.Infrastructure.ResearchAreas.EntityMappings;
using ResearchRepository.Domain.ResearchAreas.Entities;
using ResearchRepository.Domain.PublicationContext.Entities;
using ResearchRepository.Infrastructure.People.EntityMappings;

namespace Infrastructure.PublicationContext
{
    public partial class PublicationDbContext : ApplicationDbContext
    {

        public PublicationDbContext(DbContextOptions<PublicationDbContext> options, ILogger<PublicationDbContext> logger)
            : base(options, logger)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }

        public virtual DbSet<Publication> Publication { get; set; }
        public DbSet<ResearchArea> Areas { get; set; } = null!;
        public virtual DbSet<PublicationPartOfTesis> PublicationPartOfTesis { get; set; }
        public virtual DbSet<ProjectAsociatedToPublication> ProjectAsociatedToPublication { get; set; }
        public virtual DbSet<ReferenceListPublication> ReferenceListPublication { get; set; }
        public virtual DbSet<ResearchAreaPublication> ResearchAreaPublication { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PublicationMap());
            modelBuilder.ApplyConfiguration(new PublicationPartOfTesisMap());
            modelBuilder.ApplyConfiguration(new ProjectAsociatedToPublicationMap());
            modelBuilder.ApplyConfiguration(new ResearchAreaMap());
            modelBuilder.ApplyConfiguration(new ReferenceListPublicationMap());
            modelBuilder.ApplyConfiguration(new ResearchAreaPublicationMap());
            modelBuilder.ApplyConfiguration(new PersonMap());
            modelBuilder.ApplyConfiguration(new AcademicProfileMap());
            modelBuilder.ApplyConfiguration(new SubscriptionsMap());
        }

    }
}