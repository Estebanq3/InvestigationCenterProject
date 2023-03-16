using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.People.Entities;

namespace ResearchRepository.Domain.People.Entities
{
    public class Student:Collaborator
    {
        public string StudentId { get; set; }

        public Student(string email, string firstName, string firstLastName, string secondLastName, string? country)
        {
            Email = email;
            FirstName = firstName;
            FirstLastName = firstLastName;
            SecondLastName = secondLastName;
            Country = country;
            AcademicProfile = null;
            Subscriptions = null;
            StudentId = null!;
        }


    }
}
