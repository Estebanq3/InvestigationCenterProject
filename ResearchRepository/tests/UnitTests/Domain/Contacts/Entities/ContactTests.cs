using FluentAssertions;
using ResearchRepository.Domain.Core.Exceptions;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Contacts.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.Domain.Contacts.Entities
{
    public class ContactsTests
    {
        private static readonly RequiredString Name = RequiredString.TryCreate("Nombre").Success();
        private static readonly RequiredString Email = RequiredString.TryCreate("Email").Success();
        private static readonly String? Telephone = "88888888";
        private static readonly DateTime Start = DateTime.Now;
        private static readonly DateTime End = DateTime.Today.AddDays(1);



        private static readonly String? Description = "Esto es una descripcion";
        private static readonly string? Image = "/img/imagen.png";



        private IEnumerable<ResearchGroup> GetGroups()
        {
            for (var i = 1; i <= 5; ++i)
            {
                var center = new ResearchCenter(Name, Description, Image, "Citic");
                yield return new ResearchGroup(Name, Description, Image, DateTime.Now, center);
            }
        }

        [Fact]
        public void NewContact()
        {
            // act
            var group = GetGroups().First();
            var contact = new Contact(Name, Email,Telephone,Start,End,group,false);

            // assert
            contact.MainContact.Should().BeFalse();
            
        }

        [Fact]
        public void ReAssignGroup()
        {
            // act
            var group = GetGroups().First();
            var contact = new Contact(Name, Email, Telephone, Start, End, group, false);
            // assert
            contact.Invoking(t => t.AssignGroup(group)).Should().NotThrow();
        }

        [Fact]
        public void changeMainState()
        {
            // act
            var group = GetGroups().First();
            var contact = new Contact(Name, Email, Telephone, Start, End, group, false);

            // assert
            contact.Invoking(t => t.ChangeStateMainContact(true)).Should().NotThrow();
            contact.MainContact.Should().BeTrue();

        }

        [Fact]
        public void gets() {
            // act
            var group = GetGroups().First();
            var contact = new Contact(1,Name, Telephone, Email, Start, End, group, false);

            // assert
            var gEmail = contact.Email;
            contact.Email.Should().BeSameAs(Email);

            var gEnd = contact.EndDate;
            contact.EndDate.Should().BeSameDateAs(End);

            var gGroup = contact.Group;
            contact.Group.Should().BeSameAs(group);

            var gName = contact.Name;
            contact.Name.Should().BeSameAs(Name);

            var gStart = contact.StartDate;
            contact.StartDate.Should().BeSameDateAs(Start);

            var gTelephone = contact.Telephone;
            contact.Telephone.Should().BeEquivalentTo(Telephone);

            var deleted = contact.Deleted;
            contact.Deleted.Should().Be(false);
        
        }


    }
}
