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
    public class InvestigatorManagesGroup
    {   
        public string Email { get; set; }

        [ForeignKey("ResearchGroup")]
        public int GroupId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Investigator Investigator { get; set; }

        public ResearchGroup ResearchGroup { get; set; }


        public InvestigatorManagesGroup()
        {
          

        }


        public InvestigatorManagesGroup(string email, int group) {
            Email = email;
            GroupId = group;
            StartDate = null;
            EndDate = null;
            Investigator = null!;
            ResearchGroup = null!;
             
        }


    }
}
