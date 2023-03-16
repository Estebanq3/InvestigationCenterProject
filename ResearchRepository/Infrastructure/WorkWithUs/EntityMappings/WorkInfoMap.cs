using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.WorkWithUs.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResearchRepository.Infrastructure.People.EntityMappings
{
    public class WorkInfoMap : IEntityTypeConfiguration<WorkInfo>
    {

        public void Configure(EntityTypeBuilder<WorkInfo> builder)
        {
            builder.ToTable("WorkWithUs");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.PreRequisites).IsRequired();
            builder.Property(e => e.ImageName);
            builder.Property(e => e.Pregrado).IsRequired();
            builder.Property(e => e.Posgrado).IsRequired();
            builder.Property(e => e.Voluntario).IsRequired();
            builder.Property(e => e.Email).IsRequired();



        }
    }
}
