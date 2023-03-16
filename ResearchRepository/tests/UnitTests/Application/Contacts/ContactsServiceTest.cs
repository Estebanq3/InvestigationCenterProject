using Moq;
using ResearchRepository.Domain.Contacts.Entities;
using ResearchRepository.Domain.Contacts.Repositories;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Application.Contacts.Implementations;
using System;
using Xunit;
using Microsoft.AspNetCore.Identity.UI.Services;
using FluentAssertions;
using System.Collections.Generic;
using ResearchRepository.Application.Contacts;
using ResearchRepository.Domain.Core.Exceptions;

namespace UnitTests.Application.Contacts
{
    public class ContactsServiceTest
    {
        //Global Arrange
        private static readonly RequiredString Name = RequiredString.TryCreate("Nombre").Success();
        private static readonly RequiredString Email = RequiredString.TryCreate("Email").Success();
        private static readonly DateTime startDate = DateTime.Now;
        private static readonly DateTime endDate = DateTime.Now.AddDays(90);
        private static readonly ResearchGroup researchGroup = new ResearchGroup(Name, "", "", startDate, null);
        Mock<IContactsRepository> _repo = new();
        Mock<IContactEmailService> _sender = new();

        private IEnumerable<Contact> _getContacts()
        {
            ResearchGroup researchGroupTest = new ResearchGroup(Name, "", "", startDate, null);
            for (var i = 1; i <= 5; ++i)
            {
                yield return new Contact(i, Name, "", Email, startDate, endDate, researchGroupTest, false);
            }
        }

        [Fact]
        public async void SaveContactAddsContactToGroup()
        {
            //arrange
            ResearchGroup researchGroupTest = new ResearchGroup(Name, "", "", startDate, null);
            Contact contact = new Contact(1, Name, "", Email, startDate, endDate, researchGroupTest, true);

            _repo.Setup(repo => repo.SaveContactsAsync(contact));
            var contactService = new ContactsService(_repo.Object, _sender.Object);

            //act
            await contactService.SaveContactsAsync(contact);

            //assert
            researchGroupTest.Contacts.Count.Should().Be(1);
        }

        [Fact]
        public async void ValidGetContactsByIdAsyncReturnContact()
        {
            //arrange
            ResearchGroup researchGroupTest = new ResearchGroup(Name, "", "", startDate, null);
            Contact contact = new Contact(1, Name, "", Email, startDate, endDate, researchGroupTest, true);

            _repo.Setup(repo => repo.GetContactsByIdAsync(1)).ReturnsAsync(contact);
            var contactService = new ContactsService(_repo.Object, _sender.Object);

            //act
            var result = await contactService.GetContactsByIdAsync(1);

            //assert
            result.Should().Be(contact);
        }

        [Fact]
        public async void GetContactsCountAsyncReturnZero()
        {
            //arrange
            _repo.Setup(repo => repo.GetContactsCountAsync()).ReturnsAsync(0);
            var contactService = new ContactsService(_repo.Object, _sender.Object);

            //act
            var result = await contactService.GetContactsCountAsync();

            //assert
            result.Should().Be(0);
        }

        [Fact]
        public async void GetContactsCountAsyncReturnNumber()
        {
            //arrange
            _repo.Setup(repo => repo.GetContactsCountAsync()).ReturnsAsync(2);
            var contactService = new ContactsService(_repo.Object, _sender.Object);

            //act
            var result = await contactService.GetContactsCountAsync();

            //assert
            result.Should().Be(2);
        }

        [Fact]
        public async void DeleteContactsAsyncRemovesContactFromGroup()
        {
            //arrange
            ResearchGroup researchGroupTest = new ResearchGroup(Name, "", "", startDate, null);
            Contact contact = new Contact(1, Name, "", Email, startDate, endDate, researchGroupTest, true);

            _repo.Setup(repo => repo.DeleteContactsAsync(contact));
            var contactService = new ContactsService(_repo.Object, _sender.Object);

            //act
            await contactService.DeleteContactsAsync(contact);

            //assert
            researchGroupTest.Contacts.Count.Should().Be(0);
        }

        [Fact]
        public async void GetContactsByTermCountReturnsNumber()
        {
            //arrange
            _repo.Setup(repo => repo.GetContactsByTermCount(researchGroup, "")).ReturnsAsync(2);
            var contactService = new ContactsService(_repo.Object, _sender.Object);

            //act
            var result = await contactService.GetContactsByTermCount(researchGroup, "");

            //assert
            result.Should().Be(2);
        }

        [Fact]
        public async void GetContactsByGroupCountReturnsNumber()
        {
            //arrange
            _repo.Setup(repo => repo.GetContactsByGroupCount(researchGroup)).ReturnsAsync(2);
            var contactService = new ContactsService(_repo.Object, _sender.Object);

            //act
            var result = await contactService.GetContactsByGroupCount(researchGroup);

            //assert
            result.Should().Be(2);
        }

        [Fact]
        public async void GetContactsByGroupPagedReturnsContacts()
        {
            //arrange
            List<Contact> contactsList = new List<Contact>
            {
                new Contact(1, Name, "", Email, startDate, endDate, researchGroup, true),
                new Contact(2, Name, "", Email, startDate, endDate, researchGroup, true)
            };
            _repo.Setup(repo => repo.GetContactsByGroupPaged(researchGroup, 1, 2)).ReturnsAsync(contactsList);
            var contactService = new ContactsService(_repo.Object, _sender.Object);

            //act
            var result = await contactService.GetContactsByGroupPaged(researchGroup, 1, 2);

            //assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async void GetContactsByTermPagedReturnsContacts()
        {
            //arrange
            List<Contact> contactsList = new List<Contact>
            {
                new Contact(1, Name, "", Email, startDate, endDate, researchGroup, true),
                new Contact(2, Name, "", Email, startDate, endDate, researchGroup, true)
            };
            _repo.Setup(repo => repo.GetContactsByTermPaged(researchGroup, 1, 2, "")).ReturnsAsync(contactsList);
            var contactService = new ContactsService(_repo.Object, _sender.Object);

            //act
            var result = await contactService.GetContactsByTermPaged(researchGroup, 1, 2, "");

            //assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async void ChangeMainContactStateReturnContactUpdated()
        {
            //arrange
            ResearchGroup researchGroupTest = new ResearchGroup(Name, "", "", startDate, null);
            Contact contact = new Contact(1, Name, "", Email, startDate, endDate, researchGroupTest, true);

            _repo.Setup(repo => repo.GetContactsByIdAsync(1)).ReturnsAsync(contact);            
            var contactService = new ContactsService(_repo.Object, _sender.Object);

            //act
            await contactService.ChangeMainContactState(1);

            //assert
            contact.MainContact.Should().BeFalse();
        }

        [Fact]
        public async void GetMainContactReturnsContact()
        {
            //arrange
            ResearchGroup researchGroupTest = new ResearchGroup(Name, "", "", startDate, null);
            Contact contact = new Contact(1, Name, "", Email, startDate, endDate, researchGroupTest, true);

            _repo.Setup(repo => repo.GetMainContact(researchGroupTest)).ReturnsAsync(contact);
            var contactService = new ContactsService(_repo.Object, _sender.Object);

            //act
            var result = await contactService.GetMainContact(researchGroupTest);

            //assert
            result.Should().Be(contact);
        }

        [Fact]
        public async void SendContactEmailAsyncInvalidParams()
        {
            //arrange
            string name = null!;
            string subject = null!;
            string mail = null!;
            string organization = null!;
            string message = null!;
            string contactManagerMail = null!;
            List<string> emails = null!;
            IEnumerable<Contact> contacts = null!;
            _sender.Setup(sender => sender.SendEmailAsync(emails,contactManagerMail, "Formulario de contacto - ", ""));

            var contactService = new ContactsService(_repo.Object, _sender.Object);

            //act
            await contactService.SendContactEmailAsync(contacts,mail, name, subject, organization, message);
        }

        [Fact]
        public async void SendContactEmailAsyncEmptyParams()
        {
            //arrange
            string name = string.Empty;
            string subject = string.Empty;
            string userMail = string.Empty;
            string organization = string.Empty;
            string message = string.Empty;
            
            List<string> emails = new List<string>();
            IEnumerable<Contact> contacts = new List<Contact>();
            _sender.Setup(sender => sender.SendEmailAsync(emails, userMail, "Formulario de contacto - ", ""));

            var contactService = new ContactsService(_repo.Object, _sender.Object);

            //act
            await contactService.SendContactEmailAsync(contacts,userMail, name, subject, organization, message);

            //assert
        }

        [Fact]
        public async void SendContactEmailAsyncWithAllParams()
        {
            //arrange
            string name = "name";
            string subject = "subject";
            string userMail = "mail@mail";
            string organization = "organization";
            string message = "message";

            IEnumerable<Contact> contacts = _getContacts();
            List<string> emails = new List<string>();
            foreach(var contact in contacts)
            {
                emails.Add(contact.Email.ToString());
            }
            _sender.Setup(sender => sender.SendEmailAsync(emails, userMail, "Formulario de contacto - ", ""));

            var contactService = new ContactsService(_repo.Object, _sender.Object);

            //act
            await contactService.SendContactEmailAsync(contacts, userMail, name, subject, organization, message);

            //assert
        }

        [Fact]
        public async void GetContactsByTermCountWithSQLAttackShouldThrow()
        {
            //arrange
            const int numContacts = 2;
            const string attack = "'DROP TABLE Contacts; --";
            _repo.Setup(repo => repo.GetContactsByTermCount(researchGroup, attack)).ReturnsAsync(numContacts);
            var contactService = new ContactsService(_repo.Object, _sender.Object);
            //assert
            await contactService.Invoking(t => t.GetContactsByTermCount(researchGroup, attack)).Should().ThrowAsync<Exception>().WithMessage("Failed to parse against SQL injection validator.");
        }

        [Fact]
        public async void GetContactsByTermPagedWithSQLAttackShouldThrow()
        {
            //arrange
            const string attack = "'DROP TABLE Contacts; --";
            List<Contact> contactsList = new List<Contact>
            {
                new Contact(1, Name, "", Email, startDate, endDate, researchGroup, true),
                new Contact(2, Name, "", Email, startDate, endDate, researchGroup, true)
            };
            _repo.Setup(repo => repo.GetContactsByTermPaged(researchGroup, 1, 2, attack)).ReturnsAsync(contactsList);
            var contactService = new ContactsService(_repo.Object, _sender.Object);

            //assert
            await contactService.Invoking(t => t.GetContactsByTermPaged(researchGroup, 1, 2, attack)).Should().ThrowAsync<Exception>().WithMessage("Failed to parse against SQL injection validator.");
        }
    }
}
