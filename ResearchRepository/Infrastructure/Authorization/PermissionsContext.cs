using ResearchRepository.Infrastructure.Authorization.EntityMappings;
using ResearchRepository.Domain.Authorization.Entities;
using ResearchRepository.Infrastructure.Core;

using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
namespace ResearchRepository.Infrastructure.Authorization
{

    public class PermissionsContext : ApplicationDbContext
    {
        public PermissionsContext(DbContextOptions<PermissionsContext> options, ILogger<PermissionsContext> logger) : base(options, logger)
        {
        }

        public DbSet<AccountIsAdministratorOfGroup> AccountIsAdministratorOfGroup { get; set; }
        public DbSet<AccountIsCollaboratorOfGroup> AccountIsCollaboratorOfGroup { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AccountIsAdministratorOfGroupMap());
            modelBuilder.ApplyConfiguration(new AccountIsCollaboratorOfGroupMap());

        }
    }
}
