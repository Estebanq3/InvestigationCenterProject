using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ResearchRepository.Domain.Core.ValueObjects;

using ResearchRepository.Domain.InvestigationProjects.Entities;


namespace ResearchRepository.Domain.Theses.Entities
{
   public class ThesisPartOfProject
    {
        public int ThesisId { get; set; }
        
        [ForeignKey("InvestigationProject")]
        public int InvestigationProjectId { get; set; }
        
        public Thesis Thesis { get; set; }

        public InvestigationProject InvestigationProject { get; set; }
        public ThesisPartOfProject (int investigationProjectId, int thesisId)
        {
            InvestigationProjectId = investigationProjectId;
            ThesisId = thesisId;
        }

    }
}
