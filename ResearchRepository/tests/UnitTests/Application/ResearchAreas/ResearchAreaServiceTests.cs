using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ResearchRepository.Application.ResearchAreas;
using ResearchRepository.Application.ResearchAreas.Implementations;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.Exceptions;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.ResearchAreas.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.ResearchAreas.Repositories;
using Moq;
using Xunit;
using System;
using ResearchRepository.Application.PublicationContext.Implementation;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Application.ResearchAreas
{
    public class ResearchAreaServiceTests
    {
        private readonly ResearchAreaService _sut;

        private readonly Mock<IResearchAreaRepository> _areaRepositoryMock = new Mock<IResearchAreaRepository>();

        private static readonly RequiredString Name = RequiredString.TryCreate("Name", 100).Success();
        private static readonly string description = "Description";

        private static IEnumerable<ResearchArea> GetAreas()
        {
            const int areaCount = 10;
            for (int i = 1; i <= areaCount; ++i)
                yield return new ResearchArea(i, Name, description);
        }

        public ResearchAreaServiceTests()
        {
            _sut = new ResearchAreaService(_areaRepositoryMock.Object);
        }

        [Fact]
        public async Task GetResearchAreaAsyncShouldReturnListWhenAreasExists()
        {
            // Arrange
            var areas = GetAreas().ToList();
            _areaRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(areas);

            // Act
            var results = await _sut.GetResearchAreaAsync();

            // Assert
            results.Should().BeEquivalentTo(areas);
        }

        [Fact]
        public async Task GetResearchAreaAsyncShouldReturnEmptyListWhenAreasDontExists()
        {
            // Arrange
            var areas = new List<ResearchArea>();
            _areaRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(areas);

            // Act
            var results = await _sut.GetResearchAreaAsync();

            // Assert
            results.Should().BeEmpty();
        }

        [Fact]
        public async Task GetResearchAreaByIdAsyncShouldReturnAreaWithSameId()
        {
            // Arrange
            var area = GetAreas().First();
            _areaRepositoryMock.Setup(x => x.GetByIdAsync(area.Id)).ReturnsAsync(area);

            // Act
            var result = await _sut.GetResearchAreaByIdAsync(area.Id);

            // Assert
            result.Should().BeSameAs(area);
        }

        [Fact]
        public async Task GetResearchAreaByIdAsyncShouldReturnNullWhenNoId()
        {
            // Arrange
            const int id = 1;
            _areaRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetResearchAreaByIdAsync(id);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddGroupToSubAreaAsyncShouldAddWhenGroupNotAdded()
        {
            // Arrange

            var subarea = GetAreas().First();
            ResearchGroup group = new ResearchGroup(
                Name, 
                description, 
                null, 
                null, 
                null
            );

            // Act
            await _sut.AddGroupToSubAreaAsync(subarea, group);

            // Assert
            subarea.ResearchGroups.Should().Contain(group);
            group.ResearchAreas.Should().Contain(subarea);

            _areaRepositoryMock.Verify(x => x.SaveAsync(subarea), Times.Once);
        }

        [Fact]
        public async Task AddGroupToSubAreaAsyncShouldThrowWhenGroupAdded()
        {
            // Arrange
            var subarea = GetAreas().First();
            ResearchGroup group = new ResearchGroup(
                Name,
                description,
                null,
                null,
                null
            );

            group.AddAreaToGroup(subarea);

            // Act
            Func<Task> act = async () => { await _sut.AddGroupToSubAreaAsync(subarea, group); };
            await act.Should().ThrowAsync<DomainException>().WithMessage("Research area is already in the group");

            // Assert
            subarea.ResearchGroups.Should().Contain(group);
            group.ResearchAreas.Should().Contain(subarea);

            await act.Should().ThrowAsync<DomainException>();
        }


        [Fact]
        public async Task RemoveGroupFromSubAreaAsyncShouldRemoveGroupWhenContained()
        {
            // Arrange
            var subarea = GetAreas().First();
            ResearchGroup group = new ResearchGroup(
                Name,
                description,
                null,
                null,
                null
            );

            group.AddAreaToGroup(subarea);

            // Act
            await _sut.RemoveGroupFromSubAreaAsync(subarea, group); 

            // Assert
            subarea.ResearchGroups.Should().NotContain(group);
            group.ResearchAreas.Should().NotContain(subarea);

            _areaRepositoryMock.Verify(x => x.SaveAsync(subarea), Times.Once);
        }

        [Fact]
        public async Task RemoveGroupFromSubAreaAsyncShouldThrowWhenGroupNotContained()
        {
            // Arrange
            var subarea = GetAreas().First();
            ResearchGroup group = new ResearchGroup(
                Name,
                description,
                null,
                null,
                null
            );

            // Act
            Func<Task> act = async () => { await _sut.RemoveGroupFromSubAreaAsync(subarea, group); };
            await act.Should().ThrowAsync<DomainException>().WithMessage("Research area is not in the group");

            // Assert
            subarea.ResearchGroups.Should().NotContain(group);
            group.ResearchAreas.Should().NotContain(subarea);

            await act.Should().ThrowAsync<DomainException>();
        }

        [Fact]
        public async Task AddResearchAreaShouldAdd()
        {
            // Arrange
            var subarea = GetAreas().First();

            _areaRepositoryMock.Setup(m => m.SaveAsync(It.IsAny<ResearchArea>()))
                .Returns(Task.CompletedTask);

            // Act
            await _sut.AddResearchArea(subarea); 

            // Assert
            _areaRepositoryMock.Verify(m => m.SaveAsync(It.IsAny<ResearchArea>()), Times.Once());
        }

        [Fact]
        public async Task GetSubAreaAsyncShouldGetWithParentAreaId()
        {
            // Arrange
            var data = new List<ResearchArea>
            {
                new ResearchArea(1,RequiredString.TryCreate("Area1",100).Success(), "Description"),
                new ResearchArea(2,RequiredString.TryCreate("Area2",100).Success(), "Description"),
                new ResearchArea(3,RequiredString.TryCreate("Subarea1",100).Success(), "Description"),
            }.AsQueryable();

            data.LastOrDefault().AddParentArea(data.FirstOrDefault());

            var mockSet = new Mock<DbSet<ResearchArea>>();

            mockSet.As<IQueryable<ResearchArea>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ResearchArea>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ResearchArea>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ResearchArea>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _areaRepositoryMock.Setup(m => m.GetSubAreaAsync(1))
                .Returns(Task.FromResult(mockSet.Object
                                            .Where(o => o.ResearchAreas.Count() > 0 && o.ResearchAreas.FirstOrDefault().Id == 1)
                                            .AsEnumerable()));
            // Act
            var result = await _sut.GetSubAreaAsync(1);

            // Assert
            Assert.Equal(1, result.Count());
            Assert.Equal("Subarea1", result.FirstOrDefault().Name.ToString());
        }

        [Fact]
        public async Task GetSubAreaAsyncShouldNotGetWithSubareaId()
        {
            // Arrange
            var data = new List<ResearchArea>
            {
                new ResearchArea(1,RequiredString.TryCreate("Area1",100).Success(), "Description"),
                new ResearchArea(2,RequiredString.TryCreate("Area2",100).Success(), "Description"),
                new ResearchArea(3,RequiredString.TryCreate("Subarea1",100).Success(), "Description"),
            }.AsQueryable();

            data.LastOrDefault().AddParentArea(data.FirstOrDefault());

            var mockSet = new Mock<DbSet<ResearchArea>>();

            mockSet.As<IQueryable<ResearchArea>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ResearchArea>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ResearchArea>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ResearchArea>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            

            _areaRepositoryMock.Setup(m => m.GetSubAreaAsync(3))
                .Returns(Task.FromResult(mockSet.Object
                                            .Where(o => o.ResearchAreas.Count() > 0 && o.ResearchAreas.FirstOrDefault().Id == 3)
                                            .AsEnumerable()));
            // Act
            var result = await _sut.GetSubAreaAsync(1);

            // Assert
            Assert.Equal(0, result.Count());
        }

        [Fact]
        public async Task DeleteResearchAreaShouldRemoveValidArea()
        {
            // Arrange
            var area = GetAreas().First();

            _areaRepositoryMock.Setup(m => m.DeleteResearchArea(It.IsAny<ResearchArea>()));

            // Act

            _sut.DeleteResearchArea(area);

            // Assert
            _areaRepositoryMock.Verify(r => r.DeleteResearchArea(area));
        }

        [Fact]
        public async Task DeleteResearchSubareaShouldRemoveValidSubarea()
        {
            // Arrange
            var data = new List<ResearchArea>
            {
                new ResearchArea(1,RequiredString.TryCreate("Area1",100).Success(), "Description"),
                new ResearchArea(2,RequiredString.TryCreate("Area2",100).Success(), "Description"),
                new ResearchArea(3,RequiredString.TryCreate("Subarea1",100).Success(), "Description"),
            };

            data.LastOrDefault().AddParentArea(data.FirstOrDefault());

            _areaRepositoryMock.Setup(m => m.DeleteResearchArea(It.IsAny<ResearchArea>()));

            // Act

            _sut.DeleteResearchArea(data.LastOrDefault());

            // Assert
            _areaRepositoryMock.Verify(r => r.DeleteResearchArea(data.LastOrDefault()));
        }

        [Fact]
        public async Task DeleteAssociatedAreaTest()
        {
            // Arrange
            var data = new List<ResearchArea>();
            // Act

            _sut.DeleteAssociatedArea(1, 1);

            // Assert
        }
    }
}
