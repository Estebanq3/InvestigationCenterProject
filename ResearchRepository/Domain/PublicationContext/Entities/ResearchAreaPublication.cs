using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.PublicationContext;
using ResearchRepository.Domain.ResearchAreas.Entities;

namespace ResearchRepository.Domain.PublicationContext
{
    public class ResearchAreaPublication
    {
        public string PublicationsId { get; set; }

        [ForeignKey("ResearchArea")]
        public int ResearchAreasId { get; set; }

        public Publication publication { get; set; }
    }
}