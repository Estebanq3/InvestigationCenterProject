using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.Core.ValueObjects;
namespace ResearchRepository.Domain.People.Entities
{
    public class Collaborator:Person
    {
        public IList<CollaboratorPartOfGroup> CollaboratorPartOfGroups { set; get; }

        public IList<AuthorPartOfThesis> AuthorPartOfThesis { set; get; }

        public IList<CollaboratorIsAuthorOfPublication> CollaboratorIsAuthorOfPublication { set; get; }

        public IList<CollaboratorPartOfProject> CollaboratorPartOProject { set; get; }
        public string Role { set; get; }

    }


}



/* [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
 public Colaborador()
 {
     ColaboradorPerteneceGrupoes = new HashSet<ColaboradorPerteneceGrupo>();
     InvestigadorCoordinaGrupoes = new HashSet<InvestigadorCoordinaGrupo>();
 }*/


/* public virtual Persona Persona { get; set; }

 [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
 public virtual ICollection<ColaboradorPerteneceGrupo> ColaboradorPerteneceGrupoes { get; set; }

 public virtual Estudiante Estudiante { get; set; }

 public virtual Investigador Investigador { get; set; }

 [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
 public virtual ICollection<InvestigadorCoordinaGrupo> InvestigadorCoordinaGrupoes { get; set; }*/
