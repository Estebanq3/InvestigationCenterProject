using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Authorization;
using ResearchRepository.Domain.Authorization.Repositories;
using FluentAssertions;
using WebApplication_ResearchRepository;
using IntegrationTests.Infrastructure.Authorization;

namespace IntegrationTests.Infrastructure.Authorization.Repositories
{
    public class PermissionsTests :IClassFixture<PermissionsFactory<Startup>>
    {
        private readonly PermissionsFactory<Startup> _factory;
        public
        PermissionsTests(PermissionsFactory<Startup>
        factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateAdministratorOfGroup()
        {
            string email = "email@ucr.ac.cr";
            int groupId = 1;
            bool assign = true;

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IPermissionsRepository>();

            //act
            await repository.changeAdmiGroup(email, groupId, assign);
            IList<bool> admiList = await repository.AdministratorOfGroupBoolList(1,email);

            //assert
            admiList.First().Should().Be(true);

        }

        [Fact]
        public async Task CreateCollaboratorOfGroup()
        {
            string email = "email@ucr.ac.cr";
            int groupId = 1;
            bool assign = true;

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IPermissionsRepository>();

            //act
            await repository.changeColabGroup(email, groupId, assign);
            IList<bool> colabList = await repository.CollaboratorOfGroupBoolList(1, email);

            //assert
            colabList.First().Should().Be(true);

        }



        [Fact]
        public async Task CreateAndDeleteAdministratorOfGroup()
        {
            string email = "email@ucr.ac.cr";
            int groupId = 1;
            bool assign = true;

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IPermissionsRepository>();

            //act
            await repository.changeAdmiGroup(email, groupId, assign);
            assign = false;
            await repository.changeAdmiGroup(email, groupId, assign);
            IList<bool> admiList = await repository.AdministratorOfGroupBoolList(1, email);

            //assert
            admiList.First().Should().Be(false);

        }

        [Fact]
        public async Task CreateAndDeleteCollaboratorOfGroup()
        {
            string email = "email@ucr.ac.cr";
            int groupId = 1;
            bool assign = true;

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IPermissionsRepository>();

            //act
            await repository.changeColabGroup(email, groupId, assign);
            assign = false;
            await repository.changeColabGroup(email, groupId, assign);
            IList<bool> colabList = await repository.CollaboratorOfGroupBoolList(1, email);

            //assert
            colabList.First().Should().Be(false);

        }



    }
}
