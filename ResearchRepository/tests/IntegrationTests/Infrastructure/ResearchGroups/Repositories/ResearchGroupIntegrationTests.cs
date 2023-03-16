using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.ResearchGroups.Repositories;
using ResearchRepository.Infrastructure.ResearchGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_ResearchRepository;
using Xunit;

namespace IntegrationTests.Infrastructure.ResearchGroups.Repositories
{
    public class ResearchGroupIntegrationTests : IClassFixture<ResearchGroupsFactory<Startup>>
    {
        private readonly string _name = "Name";
        private readonly string _description = "Desc";

        private readonly ResearchGroupsFactory<Startup> _factory;

        public ResearchGroupIntegrationTests(ResearchGroupsFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetByIdShouldReturnGroup()
        {
            //arrange
            const int id = 1;
            var repository = _factory.Server.Services.GetRequiredService<IResearchGroupRepository>();

            //act
            var result = await repository.GetById(id);

            //assert

            result.Id.Should().Be(1);

        }

        [Fact]
        public async Task RGGetUnitOfWorkReturnsContext()
        {
            //arrange
            const int id = 1;
            var repository = _factory.Server.Services.GetRequiredService<IResearchGroupRepository>();

            //act
            var result = repository.UnitOfWork;

            //assert

            result.GetType().Should().Be(typeof(ResearchGroupsDbContext));

        }

        [Fact]
        public async Task GetCountAsyncShouldReturnInt()
        {
            //arrange
            const int id = 1;
            var repository = _factory.Server.Services.GetRequiredService<IResearchGroupRepository>();

            //act
            var result = await repository.GetCountAsync();

            //assert

            result.Should().Be(3);

        }

        //Doesnt work
        public async Task SaveAsyncShouldSaveGroup()
        {
            //arrange         
            var repository = _factory.Server.Services.GetRequiredService<IResearchGroupRepository>();
            var repositoryCenter = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();
            var center = await repositoryCenter.GetByIdAsync(1);

            center.Should().NotBeNull();
            center.Id.Should().Be(1);

            ResearchGroup _group = new ResearchGroup(4, RequiredString.TryCreate(_name, 200).Success(), _description, null, null, center);
            _group.AssingAdmin("tyron@ucr.ac.cr");

            //act
            await repository.SaveAsync(_group);
            var result = await repository.GetById(4);
            Task.WaitAll();

            //assert            
            result.Should().NotBeNull();

            // cleanup
            _factory.SeedDatabaseForTests((DbContext)repositoryCenter.UnitOfWork);
            _factory.SeedDatabaseForTests((DbContext)repository.UnitOfWork);
        }


    }
}
