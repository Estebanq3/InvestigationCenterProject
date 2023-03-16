using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.InvestigationProjects.Entities;

namespace ResearchRepository.Presentation.ResearchProjects.Models
{

        public class ProjectModel
        {
        public string? Name { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public int InvestigationGroupID { get; set; }
        public string? Description { get; set; }
        public string? Summary { get; set; }
        public String? Image { get; set; }
        //Falta la imagen
        public List<ProjectsImages>? AssociatedImages { get; set; }




        public ProjectModel(string name, DateTime stardate, DateTime enddate, int groupId, string description, string summary, String image)
        {
            Name = name;
            StartDate = stardate;
            EndDate = enddate;
            InvestigationGroupID = groupId;
            Description = description;
            Summary = summary;
            Image = image;
        }

        public ProjectModel()
        {
            Name = null;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            InvestigationGroupID = 0;
            Description = null;
            Summary = null;
            Image = null;
            AssociatedImages = new List<ProjectsImages>();
        }
    }
}
