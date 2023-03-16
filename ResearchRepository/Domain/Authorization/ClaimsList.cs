using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Domain.Authorization
{
    public class ClaimsList
    {

        public List<string> Claims { get; }

        //Agregar variable
        public string EditUsers { get; } = "Administrar Usuarios";
        public string EditRoles { get; } = "Administrar Roles";
        public string EditGroups { get; } = "Administrar Grupos";
        public string EditGroupNews { get; } = "Administrar noticias";
        public string EditGroupAreas { get; } = "Administrar áreas";
        public string EditGroupPeople { get; } = "Administrar personas de grupo";
        public string EditGroupProjects { get; } = "Administrar proyectos";
        public string EditGroupThesis { get; } = "Administrar trabajos finales";
        public string EditGroupPublications { get; } = "Administrar publicaciones";
        public string EditGroupContacts { get; } = "Administrar contactos";


        public ClaimsList() {
            Claims = new List<string>();

            //Agregar variable a la lista
            Claims.Add(EditUsers);
            Claims.Add(EditRoles);
            Claims.Add(EditGroups);
            Claims.Add(EditGroupNews);
            Claims.Add(EditGroupAreas);
            Claims.Add(EditGroupPeople);
            Claims.Add(EditGroupContacts);
            Claims.Add(EditGroupProjects);
            Claims.Add(EditGroupThesis);
            Claims.Add(EditGroupPublications);

        }

    }      
}
