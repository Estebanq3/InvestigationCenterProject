using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
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
    public class ResearchCenterIntegrationTests : IClassFixture<ResearchGroupsFactory<Startup>>
    {
        private readonly ResearchGroupsFactory<Startup> _factory;
        public ResearchCenterIntegrationTests(ResearchGroupsFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllAsyncShouldReturnAllCenters()
        {
            const int centerCount = 1;

            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();

            //act
            var groups = await repository.GetAllAsync();

            //assert
            groups.Should().HaveCount(centerCount);

        }

        [Fact]
        public async Task GetByIdAsyncInvalidReturnsNull()
        {
            const int centerId = 2; //doesnt exist
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();

            //act
            var groups = await repository.GetByIdAsync(centerId);

            //assert
            groups.Should().BeNull();
        }

        [Fact]
        public async Task GetGroupsPagedReturnsAllGroups()
        {
            const int centerId = 1;
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();

            //act
            var groups = await repository.GetGroupsPaged(centerId, 1, 10);

            //assert
            groups.Should().HaveCount(3);
        }

        [Fact]
        public async Task InvalidTermGetGroupsByTermPagedReturnsEmpty()
        {
            const int centerId = 1;
            const string term = "qweqweqw";//no found
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();

            //act
            var groups = await repository.GetGroupsByTermPaged(centerId, 1, 10, term);

            //assert
            groups.Should().BeEmpty();
        }

        [Fact]
        public async Task ValidTermGetGroupsByTermPagedReturnsGroups()
        {
            const int centerId = 1;
            const string term = "ing";//La ingenieria....
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();

            //act
            var groups = await repository.GetGroupsByTermPaged(centerId, 1, 10, term);

            //assert
            groups.Should().HaveCount(0);
        }

        [Fact]
        public async Task InvalidCenterGetGroupsByTermPagedReturnsEmpty()
        {
            const int centerId = 2;
            const string term = "la";//La ingenieria....
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();

            //act
            var groups = await repository.GetGroupsByTermPaged(centerId, 1, 10, term);

            //assert
            groups.Should().BeEmpty();
        }

        [Fact]
        public async Task GetActiveGroupsPagedReturnActiveGroups()
        {
            const int centerId = 1;
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();

            //act
            var groups = await repository.GetActiveGroupsPaged(centerId, 1, 10);

            //assert
            groups.Should().HaveCount(1);
        }

        [Fact]
        public async Task InvalidCenterGetActiveGroupsPagedReturnEmpty()
        {
            const int centerId = 2; //Invalid
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();

            //act
            var groups = await repository.GetActiveGroupsPaged(centerId, 1, 10);

            //assert
            groups.Should().BeEmpty();
        }

        [Fact]
        public async Task GetActiveGroupsCounReturnCount()
        {
            const int centerId = 1; //Valid
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();

            //act
            var groupsCount = await repository.GetActiveGroupsCount(centerId);

            //assert
            groupsCount.Should().Be(1);
        }

        [Fact]
        public async Task InvalidCenterGetActiveGroupsCounReturnZero()
        {
            const int centerId = 2; //Invalid
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();

            //act
            var groupsCount = await repository.GetActiveGroupsCount(centerId);

            //assert
            groupsCount.Should().Be(0);
        }

        [Fact]
        public async Task RCGetUnitOfWorkReturnsContext()
        {
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();

            //act
            var result = repository.UnitOfWork;

            //assert

            result.GetType().Should().Be(typeof(ResearchGroupsDbContext));

        }

        [Fact]
        public async Task GetAllGroupsByTermCountReturnCount()
        {
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();

            //act
            var result = await repository.GetAllGroupsByTermCount(1, "");

            //assert
            result.Should().Be(3);
        }

        [Fact]
        public async Task GetAllGroupsByTermCountReturnZero()
        {
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();

            //act
            var result = await repository.GetAllGroupsByTermCount(1, "sadsadsadfaf");

            //assert
            result.Should().Be(0);
        }

        [Fact]
        public async Task GetAllGroupsByTermPagedReturnGroups()
        {
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();

            //act
            var result = await repository.GetAllGroupsByTermPaged(1, 1, 3, "");

            //assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetAllGroupsByTermPagedReturnEmpty()
        {
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();

            //act
            var result = await repository.GetAllGroupsByTermPaged(1, 1, 3, "dsadsad");

            //assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllGroupsByTermPagedReturnTwoGroups()
        {
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();

            //act
            var result = await repository.GetAllGroupsByTermPaged(1, 1, 2, "");

            //assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetAllCentersReturnsCorrectCount() {
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();
            int allCenterCount = 1;
            var centers = await repository.GetAllCenters();
            centers.Count().Should().Equals(allCenterCount);
        }

        [Fact]
        public async Task GetAllGroupsAndCountFromCenter() {
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();
            int groupsInCenterNumberCount = 1;
            int centerId = 1;
            var groupsInCenterResult = await repository.GetAllGroupsFromCenter(centerId);
            groupsInCenterResult.Count().Should().Equals(groupsInCenterNumberCount);
        }

        [Fact]
        public async Task GetAllGroupsFromCenterReturnsGroups()
        {
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();
            int groupsInCenterNumberCount = 1;
            var groupsInCenterResult = await repository.GetAllGroups();
            groupsInCenterResult.Count().Should().Equals(groupsInCenterNumberCount);
        }

        [Fact]
        public async Task GetActiveGroupsCountReturnsNumber()
        {
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();
            var result = await repository.GetActiveGroupsCount(1);
            result.Should().Be(1);
        }

        [Fact]
        public async Task GetGroupsCountReturnsNumber()
        {
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();
            var result = await repository.GetGroupsCount(1);
            result.Should().Be(3);
        }

        [Fact]
        public async Task GetGroupsByTermCountReturnsNumber()
        {
            var repository = _factory.Server.Services.GetRequiredService<IResearchCenterRepository>();
            var result = await repository.GetGroupsByTermCount(1, "");
            result.Should().Be(1);
        }
    }
}
