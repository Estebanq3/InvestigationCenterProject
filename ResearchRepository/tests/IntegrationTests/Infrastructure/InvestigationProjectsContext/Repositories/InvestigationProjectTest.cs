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
using ResearchRepository.Domain.InvestigationProjects;
using ResearchRepository.Domain.InvestigationProjects.Repositories;
using FluentAssertions;
using WebApplication_ResearchRepository;
using IntegrationTests.Infrastructure.InvestigationProjectsContext;
using ResearchRepository.Domain.InvestigationProjects.Entities;
using ResearchRepository.Domain.InvestigationProjects.DTOs;

namespace IntegrationTests.Infrastructure.InvestigationProjectsContext.Repositories
{
    public class InvestigationProjectRepositoryIntegrationTestClass :
    IClassFixture<InvestigationProjectFactory<Startup>>
    {
        private readonly InvestigationProjectFactory<Startup> _factory;
        public
        InvestigationProjectRepositoryIntegrationTestClass(InvestigationProjectFactory<Startup>
        factory)
        {
            _factory = factory;
        }
        /*
        [Fact]
        public async Task GetAllAsync()
        {

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IInvestigationProjectsRepository>();

            //act
            IEnumerable<InvestigationProjectDTO> lista = await repository.GetAllAsync();

            //assert
            lista.Count().Should().Be(5);
        }
        */

        /// <summary>
        /// This method tests a investigation project count.
        /// </summary>
        /// Author: Sofia Campos
        /// Collaborator: Esteban Quesada
        [Fact]
        public async Task GetInvestigationProjectCountAsync()
        {

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IInvestigationProjectsRepository>();

            int idGroup = 1;

            //act
            int projectCount = await repository.GetProjectsCount(idGroup);
            
            //assert
            projectCount.Should().Be(6);
        }

        /// <summary>
        /// This method tries to obtain a research project from its id
        /// </summary>
        /// Author: Esteban Quesada
        /// Collaborator: Sofia Campos
        [Fact]
        public async Task GetInvestigationProjectByIdAsync()
        {

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IInvestigationProjectsRepository>();

            int id = 4;

            //act
            var project = await repository.GetByIdAsync(id);

            //assert
            project.Name.Should().Equals("Medición automatizada del tamaño funcional de aplicaciones transaccionales");
        }

        /// <summary>
        /// This method tests the aggregate of a research project
        /// </summary>
        /// Author: Steven Nuñez
        /// Collaborator: Gabriel Revillat
        [Fact]
        public async Task AddInvestigationProjectAsync()
        {

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IInvestigationProjectsRepository>();

            InvestigationProject project = new InvestigationProject(7, "AddProject1", DateTime.Now, DateTime.Now, 1, "Añadiendo proyecto de prueba", "Añadiendo  proyecto de prueba", "Default");
            InvestigationProject subtractedVariable = null;
            //act
            Task.Run(async () => await repository.SaveAsync(project)).Wait();
            //Task.WaitAll(agregar);
            Task.Run(async () => subtractedVariable = await repository.GetByIdAsync(7)).Wait();
           // Task.WaitAll(aux);

            //assert
            subtractedVariable.Name.Should().Be("AddProject1");

            // cleanup
            _factory.SeedDatabaseForTests((DbContext)repository.UnitOfWork);
        }

        /// <summary>
        /// This method tests the deletion of a research project
        /// </summary>
        /// Author: Steven Nuñez
        /// Collaborator: Gabriel Revillat
        [Fact]
        public async Task DeleteInvestigationProject()
        {

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IInvestigationProjectsRepository>();

            //act
            Task eliminar = Task.Run(async () => await repository.DeleteProject(5));
            Task.WaitAll(eliminar);
            var subtractedVariable = await repository.GetByIdAsync(5);
            Task.WaitAll();

            //assert
            subtractedVariable.Should().Be(null);

            // cleanup
            _factory.SeedDatabaseForTests((DbContext)repository.UnitOfWork);
        }

        /// <summary>
        /// This method tests the id of a project given a name 
        /// </summary>
        /// Author: Sofia Campos
        /// Collaborator: Gabriel Revillat, Esteban Quesada. 
        [Fact]
        public async Task GetIDByNameAsync()
        {

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IInvestigationProjectsRepository>();

            //act
            var projectID = await repository.GetIDByNameAsync("Evaluación de herramientas automatizadas para pruebas de software basadas en modelos");

            //assert
            projectID.Should().Be(3);
        }

        /// <summary>
        /// This method tests the paged list of active projects
        /// returned by GetActiveProjectsPaged.
        /// Author: Gabriel Revillat Zeledón
        /// StoryID: ST-HC-1.32
        /// </summary>
        /// <returns>The completed task.</returns>
        [Fact]
        public async Task GetActiveProjectsPagedReturnActiveProjects()
        {
            const int groupId = 1;
            // arrange.
            var repository = _factory.Services
                                     .GetRequiredService<IInvestigationProjectsRepository>();

            // act.
            var projects = await repository.GetActiveProjectsPaged(1, 3, groupId);
            // assert.
            projects.Should().HaveCount(3);
        }

        /// <summary>
        /// This method lists the number of active projects returned
        /// by GetActiveProjectsCount.
        /// Author: Gabriel Revillat Zeledón
        /// StoryID: ST-HC-1.32
        /// </summary>
        /// <returns>The completed task.</returns>
        [Fact]
        public async Task GetActiveProjectsCountReturnCount()
        {
            const int groupId = 1;
            // arrange.
            var repository = _factory.Services
                                     .GetRequiredService<IInvestigationProjectsRepository>();
            // act.
            var projectCount = await repository.GetActiveProjectsCount(groupId);
            // assert.
            projectCount.Should().Be(6);
        }
    }
}