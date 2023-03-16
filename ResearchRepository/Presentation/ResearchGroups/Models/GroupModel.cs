using ResearchRepository.Domain.ResearchGroups.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Presentation.ResearchGroups.Models
{
    public class GroupModel
    {

        public int? id { get; set; }
        [Required(ErrorMessage = "El grupo debe tener un nombre")]
        [StringLength(300, ErrorMessage = "Título debe tener un máximo de 300 caracteres.")]
        public string Name { get; set; }
        [Required]
        [StringLength(8000, ErrorMessage = "Descripción ha excedido el máximo número de caracteres.")]
        public string Description { get; set; }

        public string? ImageName { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }

        public ResearchCenter Center { get; set; }

        [Required(ErrorMessage = "Se necesita una persona administradora del grupo")]
        [EmailAddress]
        public string AdminEmail { get; set; }

        public bool Active { get; set; }

        public GroupModel(string title,string description, string? imageName, DateTime creationDate, ResearchCenter center)
        {
            Name = title;
            Description = description;
            ImageName = imageName;
            CreationDate = creationDate;
            Center = center;
        }

        public GroupModel()
        {
            Name = null!;
            Description = null!;
            Center = null!;
        }

    }
}
