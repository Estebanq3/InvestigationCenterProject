using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.InvestigationProjects.Entities;
using ResearchRepository.Domain.ResearchAreas.Entities;

namespace ResearchRepository.Domain.ResearchAreas.Entities
{
    public class ResearchAreaProject
    {

        [Key]
        public int ResearchAreasId { get; set; }

        [ForeignKey("InvestigationProject")]
        public int ProjectsId { get; set; }

        public ResearchArea researchArea { get; set; }
    }
}