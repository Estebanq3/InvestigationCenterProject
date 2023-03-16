using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResearchRepository.Infrastructure.People.EntityMappings
{
    public class SubscriptionsMap : IEntityTypeConfiguration<Domain.People.Entities.Subscriptions>
    {

        public void Configure(EntityTypeBuilder<Domain.People.Entities.Subscriptions> builder)
        {
            builder.ToTable("Subscriptions");
            builder.HasKey(sb => new { sb.GroupID, sb.UserEmail });
            builder.HasOne<Person>(p => p.User).WithMany(s => s.Subscriptions).HasForeignKey(sc => sc.UserEmail);
            //builder.HasOne<ResearchGroup>(g => g.Group).WithMany(s => s.Subscriptions).HasForeignKey(sc => sc.GroupID);
        }
    }
}
