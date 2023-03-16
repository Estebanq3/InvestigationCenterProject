using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Presentation.ResearchProjects.Models
{

    public class JsonFormat
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public string InternalCollaborator { get; set; }
        public string PrincipalInvestigator { get; set; }
        public string ExternalCollaborator { get; set; }
    }

}
