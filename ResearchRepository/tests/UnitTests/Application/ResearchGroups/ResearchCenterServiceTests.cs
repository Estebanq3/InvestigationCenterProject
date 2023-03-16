using FluentAssertions;
using Moq;
using ResearchRepository.Application.ResearchGroups.Implementations;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.ResearchAreas.Entities;
using ResearchRepository.Domain.ResearchGroups.DTOs;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.ResearchGroups.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Application.ResearchGroups
{
    public class ResearchCenterServiceTests
    {
        private Mock<IResearchCenterRepository> mockRepository = new();

        private static readonly ResearchCenter Center = new ResearchCenter(RequiredString.TryCreate("Center Name").Success(), null, null, null);
        private static readonly ResearchGroup Group = new ResearchGroup(1, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center);

        [Fact]
        public async Task CreateGroupAsyncAddsGroupToCenter()
        {
            //arrange
            ResearchCenter center = new ResearchCenter(RequiredString.TryCreate("Center Name").Success(), null, null, null);
            var service = new ResearchCenterService(mockRepository.Object);
            ResearchGroup group = new ResearchGroup(1, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, center);

            //act
            await service.CreateGroupAsync(group);

            //assert
            center.ResearchGroups.Count.Should().Be(1);
            center.ResearchGroups.Contains(group).Should().BeTrue();
        }

        [Fact]
        public async Task DeleteGroupAsynRemovesGroup()
        {
            //arrange
            ResearchCenter center = new ResearchCenter(RequiredString.TryCreate("Center Name").Success(), null, null, null);
            var service = new ResearchCenterService(mockRepository.Object);
            ResearchGroup group = new ResearchGroup(1, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, center);
            await service.CreateGroupAsync(group);

            //act
            await service.DeleteGroupAsync(group);

            //assert
            center.ResearchGroups.Count.Should().Be(0);
            center.ResearchGroups.Contains(group).Should().BeFalse();
        }

        [Fact]
        public async Task GetAllAsyncReturnDTOS()
        {
            //arrange
            var name = RequiredString.TryCreate("Center Name").Success();
            List<ResearchCenterDTO> lista = new List<ResearchCenterDTO>()
            {
                new ResearchCenterDTO(1, name),
                new ResearchCenterDTO(2, name),
                new ResearchCenterDTO(3, name),
                new ResearchCenterDTO(4, name)
            };
            mockRepository.Setup(i => i.GetAllAsync()).ReturnsAsync(lista);
            var service = new ResearchCenterService(mockRepository.Object);

            //act
            var result = await service.GetAllAsync();

            //assert           
            result.Should().NotBeEmpty();
            result.Length().Should().Be(4);
        }

        [Fact]
        public async Task GetByIdAsyncReturnsCenter()
        {
            //arrange
            mockRepository.Setup(i => i.GetByIdAsync(1)).ReturnsAsync(Center);
            var service = new ResearchCenterService(mockRepository.Object);

            //act
            var result = await service.GetByIdAsync(1);

            //assert
            result.Should().NotBeNull();
            result.Should().Be(Center);
        }

        [Fact]
        public async Task GetGroupsByTermCountAsyncReturnsNumber()
        {
            //arrange
            mockRepository.Setup(repo => repo.GetGroupsByTermCount(1, "")).ReturnsAsync(1);
            var service = new ResearchCenterService(mockRepository.Object);

            //act
            var result = await service.GetGroupsByTermCountAsync(1,"");

            //assert
            result.Should().Be(1);
        }

        [Fact]
        public async Task GetGroupsByTermPagedAsyncReturnsGroups()
        {
            //arrange
            var name = RequiredString.TryCreate("Center Name").Success();
            List<ResearchGroup> lista = new List<ResearchGroup>()
            {
                new ResearchGroup(1, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center),
                new ResearchGroup(2, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center),
                new ResearchGroup(3, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center),
            };
            mockRepository.Setup(i => i.GetGroupsByTermPaged(1,1,1,"")).ReturnsAsync(lista);
            var service = new ResearchCenterService(mockRepository.Object);

            //act
            var result = await service.GetGroupsByTermPagedAsync(1,1,1,"");

            //assert           
            result.Should().NotBeEmpty();
            result.Length().Should().Be(3);
        }

        [Fact]
        public async Task GetGroupsCountAsyncReturnsNumber()
        {
            //arrange
            mockRepository.Setup(repo => repo.GetGroupsCount(1)).ReturnsAsync(1);
            var service = new ResearchCenterService(mockRepository.Object);

            //act
            var result = await service.GetGroupsCountAsync(1);

            //assert
            result.Should().Be(1);
        }


        [Fact]
        public async Task GetGroupsPagedAsyncReturnsGroups()
        {
            //arrange
            var name = RequiredString.TryCreate("Center Name").Success();
            List<ResearchGroup> lista = new List<ResearchGroup>()
            {
                new ResearchGroup(1, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center),
                new ResearchGroup(2, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center),
                new ResearchGroup(3, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center),
            };
            mockRepository.Setup(i => i.GetGroupsPaged(1, 1, 1)).ReturnsAsync(lista);
            var service = new ResearchCenterService(mockRepository.Object);

            //act
            var result = await service.GetGroupsPagedAsync(1, 1, 1);

            //assert           
            result.Should().NotBeEmpty();
            result.Length().Should().Be(3);
        }

        [Fact]
        public async Task GetGroupsByAreaListAndTermPagedReturnsGroups()
        {
            //arrange
            var name = RequiredString.TryCreate("Center Name").Success();
            HashSet<ResearchArea> areas = new HashSet<ResearchArea>() {
                new ResearchArea(name, ""),
                new ResearchArea(name, "")
            };
            List<ResearchGroup> lista = new List<ResearchGroup>()
            {
                new ResearchGroup(1, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center),
                new ResearchGroup(2, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center),
                new ResearchGroup(3, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center),
            };
            mockRepository.Setup(i => i.GetGroupsByAreaListAndTermPaged(1, 1, 1, areas, "")).ReturnsAsync(lista);
            var service = new ResearchCenterService(mockRepository.Object);

            //act
            var result = await service.GetGroupsByAreaListAndTermPaged(1, 1, 1, areas, "");

            //assert           
            result.Should().NotBeEmpty();
            result.Length().Should().Be(3);
        }

        [Fact]
        public async Task GetGroupsByAreaListAndTermCountReturnsNumber()
        {
            //arrange
            var name = RequiredString.TryCreate("Center Name").Success();
            HashSet<ResearchArea> areas = new HashSet<ResearchArea>() {
                new ResearchArea(name, ""),
                new ResearchArea(name, "")
            };
            mockRepository.Setup(i => i.GetGroupsByAreaListAndTermCount(1,areas, "")).ReturnsAsync(1);
            var service = new ResearchCenterService(mockRepository.Object);

            //act
            var result = await service.GetGroupsByAreaListAndTermCount(1,areas, "");

            //assert           
            result.Should().Be(1);
        }

        [Fact]
        public async Task GetGroupsByAreaListPageReturnsGroups()
        {
            //arrange
            var name = RequiredString.TryCreate("Center Name").Success();
            HashSet<ResearchArea> areas = new HashSet<ResearchArea>() {
                new ResearchArea(name, ""),
                new ResearchArea(name, "")
            };
            List<ResearchGroup> lista = new List<ResearchGroup>()
            {
                new ResearchGroup(1, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center),
                new ResearchGroup(2, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center),
                new ResearchGroup(3, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center),
            };
            mockRepository.Setup(i => i.GetGroupsByAreaListPaged(1, 1, 1, areas)).ReturnsAsync(lista);
            var service = new ResearchCenterService(mockRepository.Object);

            //act
            var result = await service.GetGroupsByAreaListPaged(1, 1, 1, areas);

            //assert           
            result.Should().NotBeEmpty();
            result.Length().Should().Be(3);
        }

        [Fact]
        public async Task GetGroupsByAreaListCountReturnsNumber()
        {
            //arrange
            var name = RequiredString.TryCreate("Center Name").Success();
            HashSet<ResearchArea> areas = new HashSet<ResearchArea>() {
                new ResearchArea(name, ""),
                new ResearchArea(name, "")
            };
            mockRepository.Setup(i => i.GetGroupsByAreaListCount(1, areas)).ReturnsAsync(3);
            var service = new ResearchCenterService(mockRepository.Object);

            //act
            var result = await service.GetGroupsByAreaListCount(1, areas);

            //assert           
            result.Should().Be(3);
        }

        [Fact]
        public async Task GetActiveGroupsPagedReturnsGroups()
        {
            //arrange            
            List<ResearchGroup> lista = new List<ResearchGroup>()
            {
                new ResearchGroup(1, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center),
                new ResearchGroup(2, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center),
                new ResearchGroup(3, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center),
            };
            mockRepository.Setup(i => i.GetActiveGroupsPaged(1, 1, 1)).ReturnsAsync(lista);
            var service = new ResearchCenterService(mockRepository.Object);

            //act
            var result = await service.GetActiveGroupsPaged(1, 1, 1);

            //assert           
            result.Should().NotBeEmpty();
            result.Length().Should().Be(3);
        }

        [Fact]
        public async Task GetActiveGroupsCountAsyncReturnsNumber()
        {
            //arrange            
            mockRepository.Setup(i => i.GetActiveGroupsCount(1)).ReturnsAsync(2);
            var service = new ResearchCenterService(mockRepository.Object);

            //act
            var result = await service.GetActiveGroupsCountAsync(1);

            //assert           
            result.Should().Be(2);
        }


        [Fact]
        public async Task GetAllGroupsByTermPagedAsyncReturnsGroups()
        {
            //arrange            
            List<ResearchGroup> lista = new List<ResearchGroup>()
            {
                new ResearchGroup(1, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center),
                new ResearchGroup(2, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center),
                new ResearchGroup(3, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center),
            };
            mockRepository.Setup(i => i.GetAllGroupsByTermPaged(1, 1, 1, "")).ReturnsAsync(lista);
            var service = new ResearchCenterService(mockRepository.Object);

            //act
            var result = await service.GetAllGroupsByTermPagedAsync(1, 1, 1, "");

            //assert           
            result.Should().NotBeEmpty();
            result.Length().Should().Be(3);
        }

        [Fact]
        public async Task GetAllGroupsByTermCountAsyncReturnsNumber()
        {
            //arrange            
            mockRepository.Setup(i => i.GetAllGroupsByTermCount(1, "")).ReturnsAsync(2);
            var service = new ResearchCenterService(mockRepository.Object);

            //act
            var result = await service.GetAllGroupsByTermCountAsync(1, "");

            //assert           
            result.Should().Be(2);
        }

    }
}
