using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResearchRepository.Domain.PublicationContext;
using ResearchRepository.Domain.PublicationContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Infrastructure.PublicationContext.EntityMappings
{
    public class ReferenceListPublicationMap: IEntityTypeConfiguration<ReferenceListPublication>
    {
        public void Configure(EntityTypeBuilder<ReferenceListPublication> builder)
        {
            builder.ToTable("ReferenceListPublication");
            builder.HasKey(c => new { c.IdPublication, c.Order, c.Reference });

            builder.Property(e => e.IdPublication)
                .HasMaxLength(50)
                .HasColumnName("IdPublication")
                .IsRequired();

            builder.Property(e => e.Reference)
                .HasMaxLength(500)
                .HasColumnName("Reference")
                .IsRequired();

            builder.Property(e => e.Order)
                .HasColumnName("Order")
                .IsRequired();
        }    
        
    }
}
