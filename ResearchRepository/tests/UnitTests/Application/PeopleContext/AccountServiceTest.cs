using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.People.Repositories;
using ResearchRepository.Application.People.Implementations;
using Moq;
using Xunit;
using FluentAssertions;


namespace UnitTests.Application.PeopleContext
{
    public class AccountServiceTest
    {
        private string userEmail = "SEBASTIAN.MONTEROCASTRO@ucr.ac.cr";
        [Fact]
        public async Task SearchPersonByEmailTest()
        {
          

            var mock = new Mock<IAccountRepository>();
            var accountService = new AccountService(mock.Object);

            Person personTest = new Person { Email = "SEBASTIAN.MONTEROCASTRO@ucr.ac.cr" };

            mock.Setup(r => r.SearchPersonByEmail(userEmail)).ReturnsAsync(personTest);

            var person = await accountService.SearchPersonByEmail(userEmail);

            mock.Verify(repo => repo.SearchPersonByEmail(userEmail), Times.Once);
            person.Email.Should().Be(userEmail);
        }




    }

}

