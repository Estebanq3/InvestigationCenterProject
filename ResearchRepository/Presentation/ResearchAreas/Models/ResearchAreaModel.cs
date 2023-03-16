using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.ResearchAreas.Entities;

namespace ResearchRepository.Presentation.ResearchAreas.Models
{
    public class ResearchAreaModel
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public HashSet<ResearchArea>? ResearchAreas { get; set; } = new HashSet<ResearchArea>();

        public bool isSubarea { get; set; }
    }
}
