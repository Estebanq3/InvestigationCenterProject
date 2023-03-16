using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.PublicationContext;
using System;
using ResearchRepository.Domain.Core.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ResearchRepository.Domain.ResearchAreas.Entities;

namespace ResearchRepository.Domain.PublicationContext
{
    public partial class Publication:AggregateRoot
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public DateTime Year { get; set; }
        public string TypePublication { get; set; }
        public string JournalConference { get; set; }
        public int ResearchGroupId { get; set; }
        public string? Image { get; set; }
        public bool Deleted { get; set; }
        public string? DocumentPDF { get; set; }
        public byte[]? DocumentPDFAttached { get; set; }

        //public IList<CollaboratorIsAuthorOfPublication> CollaboratorIsAuthorOfPublication { set; get; }

        private readonly List<Publication> _publication=new List<Publication>();
        public IList<PublicationPartOfTesis> PublicationPartOfTesis { set; get; }
        public IList<ProjectAsociatedToPublication> ProjectAsociatedToPublication { set; get; }
        public IList<ResearchAreaPublication> ResearchAreaPublication { set; get; }

        private readonly List<ResearchArea> _researchAreas = new List<ResearchArea>();
        public IReadOnlyCollection<ResearchArea> ResearchAreas => _researchAreas.AsReadOnly();


        /// <summary>
        /// This method checks if a summary is empty or not.
        /// </summary>
        /// Author: Christian Rojas
        /// StoryID: ST-PH-1.1
        public void checkPublicationSummary()
        {

            if (string.IsNullOrWhiteSpace(this.Summary)){

                this.Summary = "Esta publicacion no contiene un resumen";
            }

        }


        /// <summary>
        /// This method order a list in descending form by year.
        /// </summary>
        /// Author: Christian Rojas
        /// StoryID: ST-PH-1.4
        public IList<Publication> orderByYear(IList<Publication>publicationsList) 
        {

            if (publicationsList != null) {

                publicationsList = publicationsList.OrderByDescending(publications => publications.Year).ToList();
     
            }

            return publicationsList;
        
        }

        public Publication(string name,string summary,DateTime year, string typePublication,string journalConference, string id, int researchGroupId, string image, string documentPDF, byte[] documentPDFAttached)
        {
            Name = name;
            Year = year;
            TypePublication = typePublication;
            JournalConference = journalConference;
            Id = id;
            ResearchGroupId = researchGroupId;
            Summary = summary;
            Image = image;
            _researchAreas = new List<ResearchArea>();
            DocumentPDF = documentPDF;
            DocumentPDFAttached = documentPDFAttached;
        }
        public Publication()
        {
            Name = null!;
            Year = DateTime.Now;
            TypePublication = null!;
            JournalConference = null!;
            Id = null!;
            ResearchGroupId = -1;
            Summary = null!;
            Image = "img/picture-default.png";
            _researchAreas = null!;
            DocumentPDF = null;
            DocumentPDFAttached = null;
        }

        public void RemoveAreaFromPublication(ResearchArea area)
        {
            _researchAreas.Remove(area);
        }
    }

}
