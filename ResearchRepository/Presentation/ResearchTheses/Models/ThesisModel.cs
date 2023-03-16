using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Presentation.ResearchTheses.Models
{
    public class ThesisModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime PublicationDate { get; set; } = DateTime.Today;

        public string Summary { get; set; }

        public long InvestigationGroupID { get; set; }

        public String Image { get; set; }
        public String DOI { get; set; }
        public String Type { get; set; }

        public String Reference { get; set; }
        public byte[]? Attachment { get; set; }
        public String? AttachmentName { get; set; }

        public ThesisModel(string name, DateTime publicationDate, long investigationGroupID,
                           string summary, String doi, String image, String type,
                           String reference, byte[]? attachment, String? attachmentName)
        {
            Name = name;
            PublicationDate = publicationDate;
            InvestigationGroupID = investigationGroupID;
            Summary = summary;
            DOI = doi;
            Image = image;
            Type = type;
            Reference = reference;
            Attachment = attachment;
            AttachmentName = attachmentName;
        }

        public ThesisModel()
        {
            Name = null;
            PublicationDate = DateTime.Now;
            InvestigationGroupID = 0;
            Summary = null;
            DOI = null;
            Image = null;
            Type = null;
            Reference = null;
            Attachment = null;
            AttachmentName = null;
        }

        //Faltan referencias. 
    }
}
