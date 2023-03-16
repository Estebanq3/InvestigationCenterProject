using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.ValueObjects;

namespace ResearchRepository.Domain.Theses.DTOs

{
    public class ThesisDTO
    {
        public long Id { get; }
        public String Name { get; }

        public DateTime PublicationDate { get; }

        public String Summary { get; }

        public long InvestigationGroupId { get; }

        public String DOI { get; set; }

        public String Image { get; }

        public String Type { get; set; }

        public String Reference { get; }

        public ThesisDTO(long id, String name, DateTime publicationDate, String summary, long GroupId, String doi, String image, String type, String reference)
        {
            Id = id;
            Name = name;
            PublicationDate = publicationDate;
            Summary = summary;
            InvestigationGroupId = GroupId;
            Image = image;
            DOI = doi;
            Type = type;
            Reference = reference;
        }
    }
}