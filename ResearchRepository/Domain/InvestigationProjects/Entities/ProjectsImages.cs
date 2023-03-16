using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;

namespace ResearchRepository.Domain.InvestigationProjects.Entities
{
    public class ProjectsImages : AggregateRoot
    {
        public String Image { get; set; }
        public int ProjectId { get; set; }


        public ProjectsImages(String image, int projectId) {
            Image = image;
            ProjectId = projectId;
        }

        public ProjectsImages(int id, String image, int projectId) {
            Id = id;
            Image = image;
            ProjectId = projectId;
        }

        public ProjectsImages() {
            Image = "img/DefaultImage.png";
            ProjectId = 0;
        }
    }

}
