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
    public class Investigator : Collaborator
    {
        public IList<InvestigatorManagesGroup> InvestigatorManagesGroups { get; set; }


        public Investigator(string email, string firstName, string firstLastName, string secondLastName, string? country)
        {
            Email = email;
            FirstName = firstName;
            FirstLastName = firstLastName;
            SecondLastName = secondLastName;
            Country = country;
            AcademicProfile = null;
            Subscriptions = null;
            InvestigatorManagesGroups = null!;
        }

    }
}
