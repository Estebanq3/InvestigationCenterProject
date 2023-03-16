using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.ResearchAreas.Entities;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;

namespace ResearchRepository.Domain.Theses.Entities
{
    public class Thesis : AggregateRoot
    {
        public String Name { get; set; }

        public DateTime PublicationDate { get; set; }

        public String Summary { get; set; }

        public long InvestigationGroupId { get; set; }

        public String Image { get; set; }

        public String DOI { get; set; }

        public String Type { get; set; }

        public String Reference { get; set; }

        public byte[]? Attachment { get; set; }

        public String? AttachmentName { get; set; }

        public bool Active { get; set; }

        private readonly List<Thesis> _publication = new List<Thesis>();
        public IList<ThesisPartOfProject> ThesisPartOfProject { set; get; }

        public IList<ResearchAreaThesis> ResearchAreaThesis { set; get; }

        private readonly List<ResearchArea> _researchAreas = new List<ResearchArea>();
        //public IReadOnlyCollection<ResearchArea> ResearchAreas => _researchAreas.AsReadOnly();

        public Thesis(String name, DateTime publicationDate, String summary,
                      long groupId, String doi, String image, String type, String reference,
                      byte[]? attachment, String? attachmentName)
        {
            Name = name;
            PublicationDate = publicationDate;
            Summary = summary;
            InvestigationGroupId = groupId;
            Image = image;
            DOI = doi;
            Type = type;
            Reference = reference;
            Attachment = attachment;
            AttachmentName = attachmentName;
            _researchAreas = new List<ResearchArea>(); 
        }
                
        public Thesis(int id, String name, DateTime publicationDate, String summary, long GroupId, String doi, String image, String type, String reference)
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
            _researchAreas = new List<ResearchArea>();
        }

        // for EFCore empty private constructor
        public Thesis()
        {
            Name = null!;
            PublicationDate = DateTime.Now;
            InvestigationGroupId = 0;
            Summary = null!;
            _researchAreas = null!;
        }
        public void RemoveAreaFromThesis(ResearchArea area)
        {
            _researchAreas.Remove(area);
        }

        /// <summary>
        /// Changes the current state of a thesis.
        /// </summary>
        /// Author: Gabriel Revillat Zeledón
        /// StoryID: ST-HC-1.33
        /// <param name="state">True if active, false otherwise.</param>
        public void ChangeThesisState(bool state)
        {
            Active = state;
        }
    }
}