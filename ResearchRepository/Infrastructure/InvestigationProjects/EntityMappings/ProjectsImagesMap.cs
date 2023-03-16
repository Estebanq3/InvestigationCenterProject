using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.InvestigationProjects.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ResearchRepository.Infrastructure.InvestigationProjects.EntityMappings
{
    public class ProjectsImagesMap : IEntityTypeConfiguration<ProjectsImages>
    {
        public void Configure(EntityTypeBuilder<ProjectsImages> builder)
        {
            builder.ToTable("ProjectImages");
            builder.HasKey(p => p.Id);
            builder.Property(i => i.Image)
                .IsRequired()
                .HasColumnType("varchar(MAX)");
            builder.Property(i => i.ProjectId)
                .IsRequired();

        }
    }
}
