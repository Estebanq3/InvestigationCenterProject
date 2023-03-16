using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.ValueObjects;

namespace ResearchRepository.Domain.InvestigationProjects.DTOs
{
    public class InvestigationProjectDTO
    {
        public long Id { get; }
        public String Name { get;}

        public DateTime StartDate { get; }

        public DateTime EndDate { get; }
        public long InvestigationGroupID { get; }
        public String Description { get; }
        public String Summary { get; }
        public String Image { get; }

        public InvestigationProjectDTO(long id, String name, DateTime startdate, DateTime endate, long investigationGroupId, string description, string summary, String image)
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
    }
}