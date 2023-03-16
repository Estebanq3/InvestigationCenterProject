using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Domain.PublicationContext.Entities
{
    public class ReferenceListPublication
    {
        [ForeignKey("Publication")]
        public string IdPublication { get; set; }

        public int Order { get; set; }

        public string Reference { get; set; }

        //FK
        public Publication publication;


        public ReferenceListPublication(string idPublication, int order, string reference)
        {
            IdPublication = idPublication;
            Order = order;
            Reference = reference;
            publication = null;

        }        
        
        public ReferenceListPublication()
        {
            IdPublication = "";
            Order = 0;
            Reference = "";
            publication = null;

        }

    }
}
