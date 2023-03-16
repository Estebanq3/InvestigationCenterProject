using ResearchRepository.Domain.ResearchGroups.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Presentation.Contacts.Models
{
    public class ContactModel
    {

        public int? id { get; set; }
        [Required]
        [StringLength(300, ErrorMessage = "El nombre debe tener un máximo de 300 caracteres.")]
        public string Name { get; set; }
        [Required]
        [StringLength(8000, ErrorMessage = "El email ha excedido el máximo número de caracteres.")]
        public string Email { get; set; }
        [StringLength(8, ErrorMessage = "El teléfono ha excedido el máximo número de caracteres.")]
        public string Telephone { get; set; }
        [Required]
        public DateTime PublicationDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public bool MainContact { get; set; }

        public ResearchGroup Group { get; set; }

        public ContactModel(string title,string description, string telephone, ResearchGroup group, bool main)
        {
            Name = title;
            Email = description;
            Telephone = telephone;
            Group = group;
            MainContact = main;
        }

        public ContactModel()
        {
            Name = null!;
            Email = null!;
            Group = null!;
            PublicationDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

    }
}
