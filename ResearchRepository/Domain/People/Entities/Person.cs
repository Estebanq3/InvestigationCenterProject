using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.ResearchNews.Entities;

namespace ResearchRepository.Domain.People.Entities
{
    public class Person
    {

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string FirstLastName { get; set; }

        public string SecondLastName { get; set; }

        public string? Country { get; set; }

        public AcademicProfile? AcademicProfile { get; set; }

        public IList<Subscriptions>? Subscriptions { get; set; }

        //News that reference this person
        private readonly List<News> _associatedNews;
        public IReadOnlyCollection<News> AssociatedNews => _associatedNews.AsReadOnly();

        public string AllName() { 
            string name=FirstName+" "+FirstLastName+" "+SecondLastName;
            return name;
           
        }

        public Person(string email, string firstName, string firstLastName, string secondLastName, string? country) {
            Email = email;
            FirstName = firstName;
            FirstLastName = firstLastName;
            SecondLastName = secondLastName;
            Country = country;
            AcademicProfile = null;
            Subscriptions = null;
            _associatedNews = null!;
        }

        public Person() {
            Email = null!;
            FirstName = null!;
            FirstLastName = null!;
            SecondLastName = null!;
            Country = null!;
            AcademicProfile = null;
            Subscriptions = null;
            _associatedNews = null!;
        }



    }
}
