using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.Theses;
using ResearchRepository.Domain.Theses.Entities;
using ResearchRepository.Domain.ResearchAreas.Entities;

namespace ResearchRepository.Domain.Theses.Entities
{
    public class ResearchAreaThesis
    {
        public int ThesisId { get; set; }

        [ForeignKey("ResearchArea")]
        public int ResearchAreasId { get; set; }

        public Thesis thesis { get; set; }
    }
}