using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Contacts.Entities;

namespace ResearchRepository.Infrastructure.Contacts.EntityMappings
{
    public class ContactsMap : IEntityTypeConfiguration<Contact>
    {
        /// <summary>
        ///  Maps Contact entity to table "Contact"
        ///  Author: Dean Vargas
        ///  StoryID: ST-MM-1.4
        /// </summary>
        /// <param name="builder">Contact EntityTypeBuilder</param>
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contact");
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Name).HasConversion(r => r.Value, s => RequiredString.TryCreate(s,200).Success());
            builder.Property(n => n.Email).HasConversion(r => r.Value, s => RequiredString.TryCreate(s,8000).Success()); ;
            builder.Property(n => n.Telephone).HasMaxLength(8);
            builder.Property(n => n.StartDate)
                .HasColumnType("datetime");
            builder.Property(n => n.EndDate)
                .HasColumnType("datetime");
            builder.Property(n => n.MainContact)
                .HasColumnType("bit");

            builder.HasQueryFilter(g => !g.Deleted);
        }
    }
}
