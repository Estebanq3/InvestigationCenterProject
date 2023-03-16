using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResearchRepository.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Infrastructure.Core.EntityMappings
{
    public class ScalarIntMap : IEntityTypeConfiguration<ScalarInt>
    {
        /// <summary>
        ///  Maps ScalarInt entity 
        ///  Author: Tyron Fonseca
        ///  StoryID: None
        /// </summary>
        /// <param name="builder">ScalarInt EntityTypeBuilder</param>
        public void Configure(EntityTypeBuilder<ScalarInt> builder)
        {
            builder.HasNoKey();
        }
    }
}
