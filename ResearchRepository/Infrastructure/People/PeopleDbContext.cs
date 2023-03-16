using ResearchRepository.Infrastructure.People.EntityMappings;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Infrastructure.Core;
using ResearchRepository.Domain.Core.ValueObjects;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ResearchRepository.Infrastructure.People
{
    public class PeopleDbContext : ApplicationDbContext
    {
        public PeopleDbContext(DbContextOptions<PeopleDbContext> options, ILogger<PeopleDbContext> logger) : base(options, logger)
        {
        }
      
        
        public DbSet<Person> Person { get; set; }
        public DbSet<AcademicProfile> AcademicProfile { get; set; }
        public DbSet<Domain.People.Entities.Subscriptions> Subscriptions { get; set; }
        public DbSet<Collaborator> Collaborator { get; set; }
        public DbSet<Investigator> Investigator { get; set; }
        public DbSet<InvestigatorManagesGroup> InvestigatorManagesGroup { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<CollaboratorPartOfGroup> CollaboratorPartOfGroup { get; set; }
        public DbSet<CollaboratorPartOfProject> CollaboratorPartOfProject { get; set; }
        public DbSet<AcademicUnit> AcademicUnits { get; set; }
        public DbSet<PersonWorksForUnit> PersonWorksForUnit { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<PersonsBelongsToUniversity> PersonsBelongsToUniversity { get; set; }
        public DbSet<AuthorPartOfThesis> AuthorPartOfThesis { get; set; }
        public DbSet<CollaboratorIsAuthorOfPublication> AuthorOfPublications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<RequiredString>(); // Important
            modelBuilder.ApplyConfiguration(new PersonMap());
            modelBuilder.ApplyConfiguration(new AcademicProfileMap());
            modelBuilder.ApplyConfiguration(new CollaboratorMap());
            modelBuilder.ApplyConfiguration(new CollaboratorPartOfGroupMap());
            modelBuilder.ApplyConfiguration(new CollaboratorPartOfProjectMap());
            //modelBuilder.ApplyConfiguration(new CollaboratorPartOfThesisMap());
            modelBuilder.ApplyConfiguration(new InvestigatorMap());
            modelBuilder.ApplyConfiguration(new InvestigatorManagesGroupMap());
            modelBuilder.ApplyConfiguration(new StudentMap());
            modelBuilder.ApplyConfiguration(new AuthorPartOfThesisMap());
            modelBuilder.ApplyConfiguration(new CollaboratorAuthorOfPublicationMap());
            modelBuilder.ApplyConfiguration(new AcademicUnitMap());
            modelBuilder.ApplyConfiguration(new PersonWorksForUnitMap());
            modelBuilder.ApplyConfiguration(new UniversityMap());
            modelBuilder.ApplyConfiguration(new PersonsBelongsToUniversityMap());
            modelBuilder.ApplyConfiguration(new SubscriptionsMap());

        }
    }
}