using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;

namespace ResearchRepository.Domain.InvestigationProjects.Entities
{
    public class InvestigationProject : AggregateRoot
    {

        public String Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int InvestigationGroupID { get; set; }
        public String Description { get; set; }

        public String Summary { get; set; }
        public String Image { get; set; }
        public bool Active { get; set; }

        public InvestigationProject(String name, DateTime startdate, DateTime endate, int investigationGroupId, string description, string summary, String image)
        {
            Name = name;
            StartDate = startdate;
            EndDate = endate;
            InvestigationGroupID = investigationGroupId;
            Description = description;
            Summary = summary;
            Image = image;
        }       
        public InvestigationProject(int id, String name, DateTime startdate, DateTime endate, int investigationGroupId, string description, string summary, String image)
        {
            Id = id;
            Name = name;
            StartDate = startdate;
            EndDate = endate;
            InvestigationGroupID = investigationGroupId;
            Description = description;
            Summary = summary;
            Image = image;
        }

        // for EFCore empty private constructor
        public InvestigationProject()
        {
            Name = null!;
            InvestigationGroupID = 0;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            Description = null;
            Summary = null;
        }

        /// <summary>
        /// Changes the current state of a project.
        /// </summary>
        /// Author: Gabriel Revillat Zeledón
        /// StoryID: ST-HC-1.32
        /// <param name="state">True if active, false otherwise.</param>
        public void ChangeProjectState(bool state)
        {
            Active = state;
        }
    }
}