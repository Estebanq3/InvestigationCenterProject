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
using ResearchRepository.Domain.InvestigationProjects.Entities;

namespace ResearchRepository.Domain.PublicationContext
{
    public class ProjectAsociatedToPublication
    {
        public string PublicationId { get; set; }
        public int InvestigationProjectId { get; set; }
        public Publication publication { get; set; }
    }
}
