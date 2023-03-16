using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ResearchRepository.Domain.Contacts.Entities;
using ResearchRepository.Domain.Contacts.Repositories;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.ResearchGroups.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_ResearchRepository;
using Xunit;

namespace IntegrationTests.Infrastructure.Contacts.Repositories
{
    public class ContactsIntegrationTests : IClassFixture<ContactsFactory<Startup>>
    {
        private readonly ContactsFactory<Startup> _factory;

        private static readonly ResearchCenter Center = new ResearchCenter(RequiredString.TryCreate("Center Name").Success(), null, null, null);
        private static readonly ResearchGroup Group = new ResearchGroup(1, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center);
        //private static readonly Contact? contact = new Contact(RequiredString.TryCreate("Testo").Success(), RequiredString.TryCreate("email@email.com").Success(), "22222222",  DateTime.Now, DateTime.Now.AddDays(1), Group, false);


        public ContactsIntegrationTests(ContactsFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetContactsByTermCountWithSQLAttackShouldThrow()
        {
            //arrange
            const string attack = "'DROP TABLE Contacts; --";
            var repository = _factory.Server.Services.GetRequiredService<IContactsRepository>();

            //assert
            await repository.Invoking(t => t.GetContactsByTermCount(Group, attack)).Should().ThrowAsync<Exception>().WithMessage("Failed to parse against SQL injection validator.");

        }

        [Fact]
        public async Task GetContactsByTermPagedWithSQLAttackShouldThrow()
        {
            //arrange
            const string attack = "'DROP TABLE Contacts; --";
            var repository = _factory.Server.Services.GetRequiredService<IContactsRepository>();
            await repository.Invoking(t => t.GetContactsByTermPaged(Group, 1, 2, attack)).Should().ThrowAsync<Exception>().WithMessage("Failed to parse against SQL injection validator.");
        }

        [Fact]
        public async Task GetContactsTest()
        {
            var repositoryGroup = _factory.Server.Services.GetRequiredService<IResearchGroupRepository>();
            var groupi = await repositoryGroup.GetById(1);
            //arrange
            const string term = "ped";
            var repository = _factory.Server.Services.GetRequiredService<IContactsRepository>();

            var list = await repository.GetContactsByTermPaged(groupi, 1, 2, term);
            await repository.Invoking(t => t.GetContactsByTermPaged(groupi, 1, 2, term)).Should().NotThrowAsync();

            var count = await repository.GetContactsByGroupCount(groupi);
            await repository.Invoking(t => t.GetContactsByGroupCount(groupi)).Should().NotThrowAsync();

            list = await repository.GetContactsByGroupPaged(groupi, 1, 2);
            await repository.Invoking(t => t.GetContactsByGroupPaged(groupi, 1, 2)).Should().NotThrowAsync();

            var conta = await repository.GetContactsByIdAsync(1);
            await repository.Invoking(t => t.GetContactsByIdAsync(1)).Should().NotThrowAsync();

            count = await  repository.GetContactsByTermCount(groupi, term);
            await repository.Invoking(t => t.GetContactsByTermCount(groupi, term)).Should().NotThrowAsync();

            count = await  repository.GetContactsCountAsync();
            await repository.Invoking(t => t.GetContactsCountAsync()).Should().NotThrowAsync();

            conta = await  repository.GetMainContact(groupi);
            await repository.Invoking(t => t.GetMainContact(groupi)).Should().NotThrowAsync();
        }

        [Fact]
        public async Task DeleteNotExistingContactShouldThrow()
        {
            //arrange

            var repository = _factory.Server.Services.GetRequiredService<IContactsRepository>();
            var contactio = await repository.Invoking(t => t.GetContactsByIdAsync(10)).Invoke(); //not existing contacto
            await repository.Invoking(t => t.DeleteContactsAsync(contactio)).Should().ThrowAsync<Exception>();

        }

        [Fact]
        public async Task CreateDelete()
        {
            //arrange
            var repositoryGroup = _factory.Server.Services.GetRequiredService<IResearchGroupRepository>();
            var groupi = await repositoryGroup.GetById(1);

            var uwu = new Contact(RequiredString.TryCreate("Testo").Success(), RequiredString.TryCreate("email@email.com").Success(), "22222222", DateTime.Now, DateTime.Now.AddDays(1), groupi, false);
            var repository = _factory.Server.Services.GetRequiredService<IContactsRepository>();

            await repository.SaveContactsAsync(uwu);
            await repository.Invoking(t => t.DeleteContactsAsync(uwu)).Should().NotThrowAsync();

        }

    }
}
