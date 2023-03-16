using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ResearchRepository.Application.StatisticsContext.Implementation;
using ResearchRepository.Domain.StatisticsContext;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.Repositories;
using ResearchRepository.Domain.Core.ValueObjects;
using Moq;
using Xunit;
using System;

namespace UnitTests.Application.StatisticContext
{
    public class StatisticsServiceTests
    {
        [Fact]
        public async Task GetAsyncShouldReturnStatistics()
        {
            // arrange
            DateTime dt1 = new DateTime(2015, 12, 31);
            DateTime dt2 = new DateTime(2018, 11, 27);
            DateTime dt3 = new DateTime(2010, 4, 7);
            DateTime dt4 = new DateTime(2013, 10, 7);
            DateTime dt5 = new DateTime(2013, 10, 8);
            List<int> groupsIds = new List<int> { 1, 2};
            List<Statistic> statisticsList = new List<Statistic>
            {
                new Statistic { Id="111.111", TypePublication="Conference", Year = dt1, ResearchGroupId = 1},
                new Statistic { Id="111.222", TypePublication="Journal", Year = dt2, ResearchGroupId = 2},
                new Statistic { Id="111.333", TypePublication="Conference", Year = dt3, ResearchGroupId = 2},
                new Statistic { Id="111.444", TypePublication="Journal", Year = dt4, ResearchGroupId = 2},
                new Statistic { Id="111.555", TypePublication="Conference", Year = dt5, ResearchGroupId = 1}
            };
            var mockPublicationRepository = new Mock<IStatisticsRepository>();
            mockPublicationRepository.Setup(repo => repo.GetAsync(groupsIds)).ReturnsAsync(statisticsList);
            var teamService = new StatisticsService(mockPublicationRepository.Object);
            // act
            var results = await teamService.GetAsync(groupsIds);
            // assert
            results.Should().BeEquivalentTo(statisticsList);
        }

        [Fact]
        public async Task GetByIdShouldReturnStatistics()
        {
            // arrange
            DateTime dt1 = new DateTime(2015, 12, 31);
            DateTime dt2 = new DateTime(2018, 11, 27);
            DateTime dt3 = new DateTime(2010, 4, 7);
            DateTime dt4 = new DateTime(2013, 10, 7);
            DateTime dt5 = new DateTime(2013, 10, 8);
            List<Statistic> statisticsList = new List<Statistic>
            {
                new Statistic { Id="111.111", TypePublication="Conference", Year = dt1, ResearchGroupId = 2},
                new Statistic { Id="111.222", TypePublication="Journal", Year = dt2, ResearchGroupId = 2},
                new Statistic { Id="111.333", TypePublication="Conference", Year = dt3, ResearchGroupId = 2},
                new Statistic { Id="111.444", TypePublication="Journal", Year = dt4, ResearchGroupId = 2},
                new Statistic { Id="111.555", TypePublication="Conference", Year = dt5, ResearchGroupId = 2}
            };

            var mockPublicationRepository = new Mock<IStatisticsRepository>();
            mockPublicationRepository.Setup(repo => repo.GetById(2)).ReturnsAsync(statisticsList);
            var teamService = new StatisticsService(mockPublicationRepository.Object);
            // act
            var results = await teamService.GetById(2);
            // assert
            results.Count().Should().Be(5);
        }

        [Fact]
        public async Task GetYearsAsyncShouldReturnYears()
        {
            // arrange
            List<int> groupsIds = new List<int> { 1, 2 };
            Dictionary<string, int> _statistics = new Dictionary<string, int>();
            _statistics.Add("2021", 3);
            _statistics.Add("2015", 6);
            _statistics.Add("2011", 5);
            var mockPublicationRepository = new Mock<IStatisticsRepository>();
            mockPublicationRepository.Setup(repo => repo.GetYearAsync(groupsIds)).ReturnsAsync(_statistics);
            var teamService = new StatisticsService(mockPublicationRepository.Object);
            // act
            var results = await teamService.GetYearAsync(groupsIds);
            // assert
            results.Should().BeEquivalentTo(_statistics);
        }

        [Fact]
        public async Task GetYearByIdAsyncShouldReturnYearById()
        {
            // arrange
            Dictionary<string, int> _statistics = new Dictionary<string, int>();
            _statistics.Add("2015", 3);
            _statistics.Add("2018", 6);
            _statistics.Add("2020", 5);
            var mockPublicationRepository = new Mock<IStatisticsRepository>();
            mockPublicationRepository.Setup(repo => repo.GetYearByIdAsync(1)).ReturnsAsync(_statistics);
            var teamService = new StatisticsService(mockPublicationRepository.Object);
            // act
            var results = await teamService.GetYearByIdAsync(1);
            // assert
            results.Should().BeEquivalentTo(_statistics);
        }

        [Fact]
        public async Task GetGroupsAsyncShouldReturnGroups()
        {
            // arrange
            List<int> groupsIds = new List<int> { 1, 2 };
            Dictionary<string, int> _statistics = new Dictionary<string, int>();
            _statistics.Add("1", 3);
            _statistics.Add("2", 6);
            _statistics.Add("3", 5);
            var mockPublicationRepository = new Mock<IStatisticsRepository>();
            mockPublicationRepository.Setup(repo => repo.GetPublicationsByGroups(groupsIds)).ReturnsAsync(_statistics);
            var teamService = new StatisticsService(mockPublicationRepository.Object);
            // act
            var results = await teamService.GetPublicationsByGroups(groupsIds);
            // assert
            results.Should().BeEquivalentTo(_statistics);
        }

        [Fact]
        public async Task GetTypePublicationAsyncShouldReturnTypePublication()
        {
            // arrange
            List<int> groupsIds = new List<int> { 1, 2 };
            Dictionary<string, int> _statistics = new Dictionary<string, int>();
            _statistics.Add("Journal", 3);
            _statistics.Add("Conference", 6);
            var mockPublicationRepository = new Mock<IStatisticsRepository>();
            mockPublicationRepository.Setup(repo => repo.GetTypePublicationAsync(groupsIds)).ReturnsAsync(_statistics);
            var teamService = new StatisticsService(mockPublicationRepository.Object);
            // act
            var results = await teamService.GetTypePublicationAsync(groupsIds);
            // assert
            results.Should().BeEquivalentTo(_statistics);
        }
        [Fact]
        public async Task GetTypePublicationByIdAsyncShouldReturnTypePublicationById()
        {
            // arrange
            Dictionary<string, int> _statistics = new Dictionary<string, int>();
            _statistics.Add("Journal", 3);
            _statistics.Add("Conference", 6);
            var mockPublicationRepository = new Mock<IStatisticsRepository>();
            mockPublicationRepository.Setup(repo =>
            repo.GetTypePublicationByIdAsync(1)).ReturnsAsync(_statistics);
            var teamService = new StatisticsService(mockPublicationRepository.Object);
            // act
            var results = await teamService.GetTypePublicationByIdAsync(1);
            // assert
            results.Should().BeEquivalentTo(_statistics);
        }

        // History ID: ST-PH-2.30
        // Implementation of GetPublicationCountByResearchGroup test
        // Participants: Frank A. and Pablo O.
        [Fact]
        public async Task GetPublicationCountByResearchGroupShouldReturnCount()
        {
            // arrange
            DateTime dt1 = new DateTime(2015, 12, 31);
            DateTime dt2 = new DateTime(2018, 11, 27);
            DateTime dt3 = new DateTime(2010, 4, 7);
            DateTime dt4 = new DateTime(2013, 10, 7);
            DateTime dt5 = new DateTime(2013, 10, 8);
            List<Statistic> statisticsList = new List<Statistic>
            {
                new Statistic { Id="111.111", TypePublication="Conference", Year = dt1, ResearchGroupId = 1},
                new Statistic { Id="111.222", TypePublication="Journal", Year = dt2, ResearchGroupId = 2},
                new Statistic { Id="111.333", TypePublication="Conference", Year = dt3, ResearchGroupId = 2},
                new Statistic { Id="111.444", TypePublication="Journal", Year = dt4, ResearchGroupId = 2},
                new Statistic { Id="111.555", TypePublication="Conference", Year = dt5, ResearchGroupId = 1}
            };
            int values = 0;
            var mockPublicationRepository = new Mock<IStatisticsRepository>();
            mockPublicationRepository.Setup(repo => repo.GetPublicationCountByResearchGroup(2)).ReturnsAsync(values);
            var teamService = new StatisticsService(mockPublicationRepository.Object);
            // act
            var results = teamService.GetPublicationCountByResearchGroupAsync(2);
            // assert
            results.Should().Equals(3);
        }

        [Fact]
        public async Task GetGroupsShouldReturnGroups()
        {
            // arrange
            List<int> groupsIds = new List<int> { 1, 2 };
            RequiredString nameCenter = RequiredString.CreateRequiredString("Center");
            RequiredString nameGroup = RequiredString.CreateRequiredString("Group");
            List<ResearchGroup> groups = new List<ResearchGroup>();
            ResearchCenter center = new ResearchCenter(nameCenter, null, null, null);
            for(int i = 1; i <= 3; i++)
            {
                ResearchGroup groupA = new ResearchGroup(i, nameGroup, null, null, null, center);
                groups.Add(groupA);
            }
            
            var mockPublicationRepository = new Mock<IStatisticsRepository>();
            mockPublicationRepository.Setup(repo => repo.GetGroups(groupsIds)).ReturnsAsync(groups);
            var teamService = new StatisticsService(mockPublicationRepository.Object);
            // act
            var results = await teamService.GetGroups(groupsIds);
            // assert
            results.Should().BeEquivalentTo(groups);
        }
    }
}
