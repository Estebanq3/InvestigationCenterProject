using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.PublicationContext;
using ResearchRepository.Domain.Theses.Entities;
namespace ResearchRepository.Domain.PublicationContext
{
    public class PublicationPartOfTesis
    {
        public string PublicationId { get; set; }

        [ForeignKey("Thesis")]
        public int ThesisId { get; set; }

        public Publication publication { get; set; }

        public PublicationPartOfTesis(string publicationId, int thesisId)
        {
            PublicationId = publicationId;
            ThesisId = thesisId;
        }
        public PublicationPartOfTesis()
        {
            PublicationId = null;
            ThesisId = 0;
        }
    }
}
