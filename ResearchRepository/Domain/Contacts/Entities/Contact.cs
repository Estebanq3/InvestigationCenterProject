using ResearchRepository.Domain.Core.Entities;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.ResearchGroups.Entities;
using System;

namespace ResearchRepository.Domain.Contacts.Entities
{
    public class Contact : AggregateRoot, ISoftDeletable
    {
        public RequiredString Name { get; }

        public RequiredString Email { get; }

        public string Telephone { get; }

        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        public ResearchGroup? Group { get; private set; }

        public bool MainContact { get; private set; }

        public bool Deleted { get; set; }


        public Contact(RequiredString name, RequiredString email, string telephone, DateTime startDate, DateTime endDate, ResearchGroup group, bool main)
        {
            Name = name;
            Email = email;
            Telephone = telephone;
            Group = group;
            StartDate = startDate;
            EndDate = endDate;
            MainContact = main;
        }


        public Contact(int id, RequiredString name, string telephone, RequiredString email, DateTime startDate, DateTime endDate, ResearchGroup group, bool main)
        {
            Id = id;
            Name = name;
            Email = email;
            Telephone = telephone;
            Group = group;
            StartDate = startDate;
            EndDate = endDate;
            MainContact = main;
        }

        //EFCore
        private Contact()
        {
            Name = null!;
            Email= null!;
            Group = null!;
        }

        public void AssignGroup(ResearchGroup? group)
        {
            Group = group;
        }

        /// <summary>
        /// Toggles the state of being main contact.
        /// </summary>
        /// Author: Dean Vargas
        /// StoryID: ST-MM-1.4
        /// <param name="state">new state of the contact</param>
        public void ChangeStateMainContact(bool state)
        {
            MainContact = state;
        }
    }
}
