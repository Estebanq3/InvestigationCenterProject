using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ResearchRepository.Domain.ResearchAreas.Repositories;
using ResearchRepository.Domain.ResearchAreas.Entities;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_ResearchRepository;
using Xunit;
using System.Transactions;

namespace IntegrationTests.Infrastructure.ResearchAreas.Repositories
{
    public class ResearchAreasIntegrationTests : IClassFixture<ResearchAreasFactory<Startup>>
    {
        private readonly ResearchAreasFactory<Startup> _factory;

        private readonly IResearchAreaRepository _repository;

        public ResearchAreasIntegrationTests(ResearchAreasFactory<Startup> factory)
        {
            _factory = factory;
            _repository = _factory.Server.Services.GetRequiredService<IResearchAreaRepository>();
        }

        [Fact]
        public async Task GetAllAsyncShouldReturnAllAreas()
        {
            // Arrange
            const int areasCount = 23;

            // Act
            var areas = await _repository.GetAllAsync();

            // Assert
            areas.Should().HaveCount(areasCount);
        }

        [Fact]
        public async Task GetByIdAsyncShouldReturnAreaWithSameId()
        {
            // Arrange
            const int id = 1;

            // Act
            var area = await _repository.GetByIdAsync(id);

            area.Id.Should().Equals(id);
        }

        [Fact]
        public async Task GetByIdAsyncShouldReturnNullWhenIdDoesntExist()
        {
            // Arrange
            const int id = 1000; // Doesn't exist

            // Act
            var area = await _repository.GetByIdAsync(id);

            // Assert
            area.Should().BeNull();
        }

        [Fact]
        public async Task DeleteResearchAreaShouldDeleteAreaFromDbIfIdExists()
        {
            // Start new transaction to roll back after test
            await _repository.UnitOfWork.BeginTransactionAsync();

            // Arrange
            var areaId = 23;
            var area = await _repository.GetByIdAsync(areaId);

            // Act
            await _repository.DeleteResearchArea(area);
            var result = await _repository.GetByIdAsync(areaId);

            // Assert
            result.Should().BeNull();

            // Undo changes
            _repository.UnitOfWork.RollbackTransaction();
        }

        [Fact]
        public async Task SaveAsyncAreaShouldPersistinDb()
        {

            // Start new transaction to roll back after test
            await _repository.UnitOfWork.BeginTransactionAsync();

            // Arrange
            const int areasCount = 24;
            var area = new ResearchArea(RequiredString.TryCreate("Area_name", 100).Success(), "Description");

            // Act
            await _repository.SaveAsync(area);
            var result = await _repository.GetAllAsync();

            // Assert
            result.Should().HaveCount(areasCount);

            // Undo changes
            _repository.UnitOfWork.RollbackTransaction();
        }

        [Fact]
        public async Task GetAssociatedAreasReturnsList()
        {
            
            // arrange
            // Start new transaction to roll back after test
            await _repository.UnitOfWork.BeginTransactionAsync();

            // Act
            var result = await _repository.GetAssociatedAreas(1);

            // Assert
            result.Should().HaveCount(0);

            // Undo changes
            _repository.UnitOfWork.RollbackTransaction();
        }
    }
}
