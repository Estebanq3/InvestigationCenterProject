using FluentAssertions;
using ResearchRepository.Domain.Core.Exceptions;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.ResearchAreas.Entities;
using ResearchRepository.Domain.Contacts.Entities;
using ResearchRepository.Domain.ResearchNews.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.ResearchAreas.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.Domain.ResearchGroups.Entities
{
    public class ResearchGroupsTests
    {
        private const int _size = 10;

        private static string _name = "Name";
        private static string _email = "Email";
        private static string _description = "Description";

        private static DateTime _startDate = DateTime.Today;
        private static DateTime _endDate = DateTime.Now;

        private static ResearchCenter _center = new ResearchCenter(RequiredString.TryCreate(_name, 200).Success(), _description, null, null);
        private static ResearchGroup _group = new ResearchGroup(1, RequiredString.TryCreate(_name, 200).Success(), _description, null, null, _center);

        private IEnumerable<ResearchArea> GetAreas()
        {
            for (var i = 1; i <= _size; ++i)
            {
                yield return new ResearchArea(i, RequiredString.TryCreate(_name, 100).Success(), _description);
            }
        }

        private IEnumerable<News> GetNews()
        {
            for (var i = 1; i <= _size; ++i)
            {
                yield return new News(i, RequiredString.TryCreate(_name, 200).Success(), RequiredString.TryCreate(_description, 8000).Success(), null, null, null, null, null, _group);
            }
        }

        private IEnumerable<Contact> GetContacts()
        {
            for (var i = 1; i <= _size; ++i)
            {
                yield return new Contact(i, RequiredString.TryCreate(_name, 200).Success(), "2824-2414", RequiredString.TryCreate(_email, 8000).Success(), _startDate, _endDate, _group, false);
            }
        }

        [Fact]
        public void CreateGroupWithoutIdAndGets()
        {
            var name = RequiredString.TryCreate(_name, 200).Success();
            var image = "image.png";
            var date = DateTime.Now;

            // Act
            var group = new ResearchGroup(name, _description, image, date, _center);


            // Assert
            group.Name.Should().Be(name);
            group.ImageName.Should().Be(image);
            group.Description.Should().Be(_description);
            group.CreationDate.Should().Be(date);
            group.Center.Should().Be(_center);
        }

        [Fact]
        public void AddNewsToGroupShouldAddNews()
        {
            // Arrange
            var researchNew = GetNews().First();
            var group = new ResearchGroup(2, RequiredString.TryCreate(_name, 200).Success(), _description, null, null, _center);

            // Act
            group.AddNewsToGroup(researchNew);

            // Assert
            group.News.Should().Contain(researchNew);
        }

        [Fact]
        public void AddNullNewsToGroupShouldThrow()
        {
            // Arrange
            News researchNew = null;
            var group = new ResearchGroup(2, RequiredString.TryCreate(_name, 200).Success(), _description, null, null, _center);

            // Assert
            group.Invoking(g => g.AddNewsToGroup(researchNew)).Should().Throw<DomainException>().WithMessage("Research news is null");
        }

        [Fact]
        public void AddSameNewsToGroupShouldThrow()
        {
            // Arrange
            var researchNew = GetNews().First();
            var group = new ResearchGroup(2, RequiredString.TryCreate(_name, 200).Success(), _description, null, null, _center);

            // Act
            group.AddNewsToGroup(researchNew);

            // Assert
            group.Invoking(g => g.AddNewsToGroup(researchNew)).Should().Throw<DomainException>().WithMessage("Research news is already added to the group");
        }

        [Fact]
        public void RemoveNewsFromGroupShouldWork()
        {
            // Arrange
            var researchNew = GetNews().First();
            var group = new ResearchGroup(2, RequiredString.TryCreate(_name, 200).Success(), _description, null, null, _center);
            group.AddNewsToGroup(researchNew);
            // Act
            group.RemoveNewsFromGroup(researchNew);

            // Assert
            group.News.Should().NotContain(researchNew);
        }

        [Fact]
        public void AddContactToGroupShouldAddContact()
        {
            // Arrange
            var contact = GetContacts().First();
            var group = new ResearchGroup(2, RequiredString.TryCreate(_name, 200).Success(), _description, null, null, _center);

            // Act
            group.AddContactToGroup(contact);

            // Assert
            group.Contacts.Should().Contain(contact);
        }

        [Fact]
        public void RemoveContactFromGroupShouldWork()
        {
            // Arrange
            var contact = GetContacts().First();
            var group = new ResearchGroup(2, RequiredString.TryCreate(_name, 200).Success(), _description, null, null, _center);
            group.AddContactToGroup(contact);

            // Act
            group.RemoveContactFromGroup(contact);

            // Assert
            group.Contacts.Should().NotContain(contact);
        }

        [Fact]
        public void AddNullContactToGroupShouldThrow()
        {
            // Arrange
            Contact contact = null;
            var group = new ResearchGroup(2, RequiredString.TryCreate(_name, 200).Success(), _description, null, null, _center);

            // Assert
            group.Invoking(g => g.AddContactToGroup(contact)).Should().Throw<DomainException>().WithMessage("Contact is null");
        }

        [Fact]
        public void AddSameContactToGroupShouldThrow()
        {
            // Arrange
            var contact = GetContacts().First();
            var group = new ResearchGroup(2, RequiredString.TryCreate(_name, 200).Success(), _description, null, null, _center);

            // Act
            group.AddContactToGroup(contact);

            // Assert
            group.Invoking(g => g.AddContactToGroup(contact)).Should().Throw<DomainException>().WithMessage("Contact is already added to the group");
        }


        [Fact]
        public void AddAreaToGroupShouldAddArea()
        {
            //arrange
            ResearchArea area = new ResearchArea(RequiredString.TryCreate(_name).Success(), _description);
            var group = new ResearchGroup(2, RequiredString.TryCreate(_name, 200).Success(), _description, null, null, _center);

            //act
            group.AddAreaToGroup(area);

            //assert
            group._researchAreas.First().Should().Be(area);

        }


        [Fact]
        public void AddSameAreaToGroupShouldThrow()
        {
            //arrange
            ResearchArea area = new ResearchArea(RequiredString.TryCreate(_name).Success(), _description);
            var group = new ResearchGroup(2, RequiredString.TryCreate(_name, 200).Success(), _description, null, null, _center);

            //act
            group.AddAreaToGroup(area);

            //assert
            group.Invoking(g => g.AddAreaToGroup(area)).Should().Throw<DomainException>().WithMessage("Research area is already in the group");

        }

        [Fact]
        public void RemoveAreaFromGroupShouldWork()
        {
            //arrange
            ResearchArea area = new ResearchArea(RequiredString.TryCreate(_name).Success(), _description);
            var group = new ResearchGroup(2, RequiredString.TryCreate(_name, 200).Success(), _description, null, null, _center);
            group.AddAreaToGroup(area);

            //act
            group.RemoveAreaFromGroup(area);

            //assert
            group._researchAreas.Count.Should().Be(0);

        }


        [Fact]
        public void AssingCenterGroupShouldAssignCenter()
        {
            //arrange
            var group = new ResearchGroup(2, RequiredString.TryCreate(_name, 200).Success(), _description, null, null, null);

            //act
            group.AssignCenter(_center);

            //assert
            group.Center.Should().Be(_center);
        }

        [Fact]
        public void AssingAdminGroupShouldAssignAdmin()
        {
            //arrange
            var group = new ResearchGroup(2, RequiredString.TryCreate(_name, 200).Success(), _description, null, null, _center);
            var admin = "correo@ucr.ac.cr";

            //act
            group.AssingAdmin(admin);

            //assert
            group.AdminEmail.Should().Be(admin);

        }


        [Fact]
        public void ChangeStateGroupShouldWork()
        {
            //arrange
            var group = new ResearchGroup(2, RequiredString.TryCreate(_name, 200).Success(), _description, null, null, _center);
            var newState = true;

            //act
            group.ChangeStateGroup(newState);

            //assert
            group.Active.Should().Be(newState);

        }


    }
}
