using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;

namespace ResearchRepository.Domain.WorkWithUs.Entities
{
    public class WorkInfo
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string? ImageName { get; set; }

        public string PreRequisites { get; set; }

        public string Pregrado { get; set; }

        public string Posgrado { get; set; }

        public string Voluntario { get; set; }

        public string Email { get; set; }

        public WorkInfo(string name, string description, string image, string prerequisites, string pregrado, string posgrado, string voluntario, string email)
        {

            Name = name;
            Description = description;
            ImageName = image;
            PreRequisites = prerequisites;
            Pregrado = pregrado;
            Posgrado = posgrado;
            Voluntario = voluntario;
            Email = email;
        }

        public WorkInfo()
        {
            Name = null!;
            Description = null!;
            ImageName = null;
            PreRequisites = null!;
            Pregrado = null!;
            Posgrado = null!;
            Voluntario = null!;
            Email = null!;
        }

    }
}
