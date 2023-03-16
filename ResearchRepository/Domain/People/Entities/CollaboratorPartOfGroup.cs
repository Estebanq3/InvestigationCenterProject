using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;

namespace ResearchRepository.Domain.People.Entities
{
    public class CollaboratorPartOfGroup
    {

        public string CollaboratorEmail { get; set; }

        [ForeignKey("ResearchGroup")]
        public int InvestigationGroupId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Collaborator Collaborator { get; set; }

        public ResearchGroup ResearchGroup { get; set; }

    }
}
