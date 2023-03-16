using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.PublicationContext;

namespace ResearchRepository.Domain.People.Entities
{
    public class CollaboratorIsAuthorOfPublication
    {
        [ForeignKey("Collaborator")]
        public string EmailCollaborator { get; set; }

        [ForeignKey("Publication")]
        public string IdPublication { get; set; }

        public Collaborator Collaborator { get; set; }


    }
}