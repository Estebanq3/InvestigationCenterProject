using FluentAssertions;
using Moq;
using ResearchRepository.Application.ResearchGroups.Implementations;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
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
    public class ResearchGroupServiceTests
    {
        private Mock<IResearchGroupRepository> mockRepository = new ();

        private static readonly ResearchCenter Center = new ResearchCenter(RequiredString.TryCreate("Center Name").Success(), null, null, null);
        private static readonly ResearchGroup Group = new ResearchGroup(1, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center);

        [Fact]
        public async Task GetByIdReturnsGroup()
        {
            //arrange
            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(Group);
            var service = new ResearchGroupService(mockRepository.Object);

            //act
            var result = await service.GetById(1);

            //assert
            result.Should().Be(Group);                        
        }

        [Fact]
        public async Task GetCountAsyncReturnsNumber()
        {
            //arrange
            mockRepository.Setup(repo => repo.GetCountAsync()).ReturnsAsync(1);
            var service = new ResearchGroupService(mockRepository.Object);

            //act
            var result = await service.GetCountAsync();

            //assert
            result.Should().Be(1);
        }

        [Fact]
        public async Task ChangeStateGroupReturnsGroupActive()
        {
            //arrange
            ResearchGroup group = new ResearchGroup(2, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center);
            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(group);
            mockRepository.Setup(repo => repo.SaveAsync(group));
            var service = new ResearchGroupService(mockRepository.Object);

            //act
            await service.ChangeStateGroup(1, true);

            //assert
            group.Active.Should().BeTrue();
        }

        [Fact]
        public async Task ChangeStateGroupReturnsGroupInactive()
        {
            //arrange
            ResearchGroup group = new ResearchGroup(2, RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center);
            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(group);
            mockRepository.Setup(repo => repo.SaveAsync(group));
            var service = new ResearchGroupService(mockRepository.Object);

            //act
            await service.ChangeStateGroup(1, false);

            //assert
            group.Active.Should().BeFalse();
        }
    }
}
