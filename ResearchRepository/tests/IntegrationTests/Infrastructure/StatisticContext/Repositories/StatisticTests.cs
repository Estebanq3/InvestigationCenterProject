using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ResearchRepository.Domain.Repositories;
using FluentAssertions;
using WebApplication_ResearchRepository;

namespace IntegrationTests.Infrastructure.StatisticContext.Repositories
{
    public class StatisticRepositoryIntegrationTestClass :
    IClassFixture<StatisticFactory<Startup>>
    {
        private readonly StatisticFactory<Startup> _factory;
        public
        StatisticRepositoryIntegrationTestClass(StatisticFactory<Startup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task GetAsyncTest()
        {
            // arrange
            List<int> groupsIds = new List<int> { 1, 2 };
            var repository = _factory.Server.Services.GetRequiredService<IStatisticsRepository>();
            //act
            var statistics = await repository.GetAsync(groupsIds);
            //assert
            statistics.Count().Should().Be(5);
        }
        [Fact]
        public async Task GetYearAsyncTest()
        {
            // arrange
            List<int> groupsIds = new List<int> { 1, 2 };
            string firtsYear = "2020";
            string SecondYear = "2021";
            var repository = _factory.Server.Services.GetRequiredService<IStatisticsRepository>();
            //act
            var years = await repository.GetYearAsync(groupsIds);
            //assert
            years.Values.Count().Should().Be(2);
            years.Keys.Count().Should().Be(2);
            years[firtsYear].Should().Be(5);
            years[SecondYear].Should().Be(0);
        }

        [Fact]
        public async Task GetYearByIdAsyncTest()
        {
            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IStatisticsRepository>();
            int groupId = 1;
            string firtsYear = "2020";
            string SecondYear = "2021";
            //act
            var yearsByGroup = await repository.GetYearByIdAsync(groupId);
            //assert
            yearsByGroup.Values.Count().Should().Be(2);
            yearsByGroup.Keys.Count().Should().Be(2);
            yearsByGroup[firtsYear].Should().Be(5);
            yearsByGroup[SecondYear].Should().Be(0);
        }

        [Fact]
        public async Task GetByIdTest()
        {
            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IStatisticsRepository>();
            int groupId = 1;
            //act
            var statistics = await repository.GetById(groupId);
            //assert
            statistics.Count().Should().Be(5);
        }


        [Fact]
        public async Task GetGroupsTets()
        {
            // arrange
            List<int> groupsIds = new List<int> { 1, 2 };
            var repository = _factory.Server.Services.GetRequiredService<IStatisticsRepository>();
            // act
            var groups = await repository.GetPublicationsByGroups(groupsIds);
            // assert
            groups.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetTypePublicationAsyncTest()
        {
            // arrange
            List<int> groupsIds = new List<int> { 1, 2 };
            string type = "Conference";
            var repository = _factory.Server.Services.GetRequiredService<IStatisticsRepository>();
            // act
            var types = await repository.GetTypePublicationAsync(groupsIds);
            // assert
            types.Keys.Count().Should().Be(1);
            types.Values.Count().Should().Be(1);
            types[type].Should().Be(5);

        }
        [Fact]
        public async Task GetTypePublicationByIdAsyncTest()
        {
            // arrange
            int groupsId = 1;
            string type = "Conference";
            var repository =  _factory.Server.Services.GetRequiredService<IStatisticsRepository>();
            // act
            var types = await repository.GetTypePublicationByIdAsync(groupsId);
            // assert
            types.Keys.Count().Should().Be(1);
            types.Values.Count().Should().Be(1);
            types[type].Should().Be(5);
        }

        // History ID: ST-PH-2.30
        // Implementation of GetPublicationCountByResearchGroup test
        // Participants: Frank A. and Pablo O.
        [Fact]
        public async Task GetPublicationCountByResearchGroupTest()
        {
            // arrange
            int groupsId = 1;
            var repository = _factory.Server.Services.GetRequiredService<IStatisticsRepository>();
            // act
            var types = await repository.GetPublicationCountByResearchGroup(groupsId);
            // assert
            types.Should().Be(5);
        }
    }
}