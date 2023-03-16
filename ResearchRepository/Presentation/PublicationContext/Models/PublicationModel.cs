using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.PublicationContext;

namespace ResearchRepository.Presentation.PublicationContext.Models
{
    public class PublicationModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public DateTime Year { get; set; }
        public string TypePublication { get; set; }
        public string JournalConference { get; set; }
        public int ResearchGroupId { get; set; }
        public string Image { get; set; }
        public string DocumentPDF { get; set; }
        public byte[] DocumentPDFAttached { get; set; }

        public PublicationModel(string name, string summary, DateTime year, string typePublication, string journalConference, string id, int researchGroupId, string image, string documentPDF, byte[] documentPDFAttached)
        {
            Name = name;
            Year = year;
            TypePublication = typePublication;
            JournalConference = journalConference;
            Id = id;
            ResearchGroupId = researchGroupId;
            Summary = summary;
            Image = image;
            DocumentPDF = documentPDF;
            DocumentPDFAttached = documentPDFAttached;
        }

        public PublicationModel()
        {
            Name = null!;
            Year = DateTime.Now;
            TypePublication = null!;
            JournalConference = null!;
            Id = null!;
            ResearchGroupId = -1;
            Summary = null!;
            Image = "img/picture-default.png";
            DocumentPDF = null;
            DocumentPDFAttached = null;
        }
    }
}
