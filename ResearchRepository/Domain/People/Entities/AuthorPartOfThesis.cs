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
    public class AuthorPartOfThesis
    {

        public string CollaboratorEmail { get; set; }

        [ForeignKey("Thesis")]
        public int ThesisId { get; set; }

        public string Role { get; set; }

        public Collaborator Collaborator { get; set; }

        public AuthorPartOfThesis(string collaboratorEmail, int thesisID, string role)
        {
            CollaboratorEmail = collaboratorEmail;
            ThesisId = thesisID;
            Role = role;
        }

        public AuthorPartOfThesis()
        {
            CollaboratorEmail = null;
            Role = null;
        }
    }
}