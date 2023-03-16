using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.InvestigationProjects.Entities;

namespace ResearchRepository.Domain.People.Entities
{
    public class CollaboratorPartOfProject
    {

        public string CollaboratorEmail { get; set; }

        [ForeignKey("InvestigacionProject")]
        public int InvestigationProjectId { get; set; }

        public string Role { get; set; }

        public Collaborator Collaborator { get; set; }

        public InvestigationProject InvestigationProject { get; set; }

        public CollaboratorPartOfProject(string collaboratorEmail, int investigationProjectId, string role)
        {
            CollaboratorEmail = collaboratorEmail;
            InvestigationProjectId = investigationProjectId;
            Role = role;
        }

        public CollaboratorPartOfProject()
        {
            CollaboratorEmail = null;
            Role = null;
        }

    }
}

