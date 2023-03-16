using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Presentation.ResearchTheses.Models
{

    public class JsonFormat
    {
        public string Name { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public string DOI { get; set; }
        public string Type { get; set; }
        public string Reference { get; set; }
        public Persons[]? Persons { get; set; } = null!;
        public Projects[]? Projects { get; set; } = null!;
        public Publications[]? Publications { get; set; } = null!;

        public JsonFormat(){  }

        public JsonFormat(string name, DateTime publicationDate, string summary, String doi, String image, String type, String reference, Persons[] persons, Projects[] projects, Publications[] publications) 
        {
            Name = name;
            PublicationDate = publicationDate;
            Summary = summary;
            DOI = doi;
            Image = image;
            Type = type;
            Reference = reference;
            Persons = persons;
            Projects = projects;
            Publications = publications; 
        }
    }

    public class Persons 
    {
        public string Email { get; set; }
        public string Role { get; set; }
    }

    
    public class Projects
    {
        public string Name { get; set; }
    }
    public class Publications
    {
        public string PublicationID { get; set; }
    }
}
