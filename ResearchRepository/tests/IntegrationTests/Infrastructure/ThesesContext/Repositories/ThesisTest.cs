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
using ResearchRepository.Domain.Theses;
using ResearchRepository.Domain.Theses.Repositories;
using FluentAssertions;
using WebApplication_ResearchRepository;
using IntegrationTests.Infrastructure.ThesesContext;
using ResearchRepository.Domain.Theses.Entities;
using ResearchRepository.Domain.Theses.DTOs;

namespace IntegrationTests.Infrastructure.ThesesContext.Repositories
{
    public class ThesisRepositoryIntegrationTestClass :
    IClassFixture<ThesisFactory<Startup>>
    {
        private readonly ThesisFactory<Startup> _factory;
        public
        ThesisRepositoryIntegrationTestClass(ThesisFactory<Startup>
        factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetThesisByIdAsync()
        {

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IThesesRepository>();

            int id = 4;

            //act
            var thesis = await repository.GetByIdAsync(id);

            //assert
            thesis.Name.Should().Equals("Evaluación empírica de un protocolo de verificación de exactitud de la medición de tamaño funcional del software ");
        }

        [Fact]
        public async Task GetThesesCountAsync()
        {

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IThesesRepository>();

            int idGroup = 1;

            //act
            int thesisCount = await repository.GetThesesCount(idGroup);

            //assert
            thesisCount.Should().Be(4);
        }

        /// <summary>
        /// This method tests the addition of a thesis.
        /// </summary>
        /// Author: Steven Nuñez
        /// Collaborator: Gabriel Revillat
        [Fact]
        public async Task AddThesisAsync()
        {

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IThesesRepository>();

            Thesis thesis = new Thesis(5, "AddTest1", DateTime.Now, "Añadiendo Tesis de prueba", 2, "12345", "Default", "Posgrado", "Null");

            //act
            await repository.SaveAsync(thesis);
            var subtractedVariable = await repository.GetByIdAsync(5);
            Task.WaitAll();
            //assert
            subtractedVariable.Name.Should().Equals("AddTest1");

            // cleanup
            _factory.SeedDatabaseForTests((DbContext)repository.UnitOfWork);
        }


        [Fact]
        public async Task DeleteThesis()
        {

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IThesesRepository>();

            //act
            await repository.DeleteThesis(2);
            Task.WaitAll();
            var subtractedVariable = await repository.GetByIdAsync(2);
            Task.WaitAll();
            //assert
            subtractedVariable.Should().Be(null);

            // cleanup
            _factory.SeedDatabaseForTests((DbContext)repository.UnitOfWork);
        }

        [Fact]
        public async Task GetAllAsync()
        {

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IThesesRepository>();

            //act
            IEnumerable<ThesisDTO> lista  = await repository.GetAllAsync();

            //assert
            lista.Count().Should().Be(4);
        }

        /// <summary>
        /// This method tests getting a thesis by its name.
        /// </summary>
        /// Author: Gabriel Revillat Zeledón.
        /// Collaborators: Esteban Quesada, Sofía Castillo.
        /// <returns>The completed tasks.</returns>
        [Fact]
        public async Task GetThesisByNameAsync()
        {
            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IThesesRepository>();

            string thesisName = "Evaluación empírica de un protocolo de verificación de exactitud de la medición de tamaño funcional del software ";

            // act
            var thesis = await repository.GetByNameAsync(thesisName, new DateTime(2016, 01, 01));

            // assert
            thesis.Id.Should().Equals(4);
        }

        [Fact]
        public async Task GetProjectFromThesisId()
        {
            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IThesesRepository>();

            //act
            IList<int> projectList = await repository.GetProjectFromThesisId(3);

            //assert
            projectList.Count().Should().Be(0);
        }

        /// <summary>
        /// This method tests the paged list of active theses
        /// returned by GetActiveThesesPaged.
        /// Author: Gabriel Revillat Zeledón
        /// StoryID: ST-HC-1.33
        /// </summary>
        /// <returns>The completed task.</returns>
        [Fact]
        public async Task GetActiveThesesPagedReturnActiveTheses()
        {
            const int groupId = 1;
            // arrange.
            var repository = _factory.Services.GetRequiredService<IThesesRepository>();

            // act.
            var theses = await repository.GetActiveThesesPaged(1, 3, groupId);
            // assert.
            theses.Should().HaveCount(3);
        }

        /// <summary>
        /// This method lists the number of active theses returned
        /// by GetActiveThesesCount.
        /// </summary>
        /// Author: Gabriel Revillat Zeledón
        /// StoryID: ST-HC-1.33
        /// <returns>The completed task.</returns>
        [Fact]
        public async Task GetActiveThesesCountReturnCount()
        {
            const int groupId = 1;
            // arrange.
            var repository = _factory.Services
                                     .GetRequiredService<IThesesRepository>();
            // act.
            var thesisCount = await repository.GetActiveThesesCount(groupId);
            // assert.
            thesisCount.Should().Be(4);
        }
    }
}