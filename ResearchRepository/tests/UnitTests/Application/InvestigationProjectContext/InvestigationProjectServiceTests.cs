using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ResearchRepository.Application.InvestigationProjects;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.InvestigationProjects;
using ResearchRepository.Domain.InvestigationProjects.Repositories;
using ResearchRepository.Domain.InvestigationProjects.Entities;
using ResearchRepository.Application.InvestigationProjects.Implementations;
using ResearchRepository.Domain.InvestigationProjects.DTOs;
using Moq;
using Xunit;
using System;

namespace UnitTests.Application.InvestigationProjectContext
{
    
    public class InvestigationProjectServiceTests
    {
        private static long Id = 1;
        private static String Name = "Test1";
        private static DateTime StartDate = DateTime.Now;
        private static DateTime EndDate = DateTime.Now;
        private static int InvestigationGroupID = 1;
        private static String Description = "This is a test";
        private static String Summary = "This is a summary of a test";
        private static String Image = "picture-default.png";

        /// <summary>
        /// This method tests getting the projects by term page
        /// </summary>
        /// Author: Esteban Quesada - Sofia Castillo
        [Fact]
        public async Task GetProjectByTermPagedAsyncTest()
        {
            List<InvestigationProject> projectsList = new List<InvestigationProject>
            {

                new InvestigationProject {Name="Proyecto1",StartDate= new DateTime(2018, 11, 20), EndDate=new DateTime(2019, 11, 20),InvestigationGroupID = 1,Description= "Descripcion1", Summary="Resumen1", Image="default"},
                new InvestigationProject {Name="Proyecto2",StartDate= new DateTime(2019, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 1,Description= "Descripcion2", Summary="Resumen2", Image="default"},
                new InvestigationProject {Name="Proyecto3",StartDate= new DateTime(2019, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 2,Description= "Descripcion3", Summary="Resumen3", Image="default"},
                new InvestigationProject {Name="Proyecto4",StartDate= new DateTime(2020, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 3,Description= "Descripcion4", Summary="Resumen4", Image="default"},
                new InvestigationProject {Name="Proyecto5",StartDate= new DateTime(2021, 11, 20), EndDate=new DateTime(2021, 11, 20),InvestigationGroupID = 3,Description= "Descripcion5", Summary="Resumen5", Image="default"}

            };

            var mockProjectsRepository = new Mock<IInvestigationProjectsRepository>();
            var projectService = new InvestigationProjectService(mockProjectsRepository.Object);
            mockProjectsRepository.Setup(repo => repo.GetProjectsByTermPaged(1, 1, "", 1)).ReturnsAsync(projectsList);

            //act
            var _projects = await projectService.GetProjectsByTermPagedAsync(1, 1, "", 1);

            //assert
            mockProjectsRepository.Verify(repo => repo.GetProjectsByTermPaged(1, 1, "",1), Times.Once);
            _projects.Count().Should().Be(5);
        }

        /// <summary>
        /// This method tests getting the projects by Id
        /// </summary>
        /// Author: Esteban Quesada - Sofia Castillo
        [Fact]
        public async Task GetProjectById()
        {

            //arrange
            var project = new InvestigationProject(1,"Prueba", new DateTime(2018, 11, 20), new DateTime(2019, 11, 20),1, "Descripcion1", "Resumen1","default");

            var mockProjectsRepository = new Mock<IInvestigationProjectsRepository>();
            var projectsService = new InvestigationProjectService(mockProjectsRepository.Object);
            mockProjectsRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(project);

            await projectsService.AddProjectAsync(project);

            //act

            var projectsTest = await projectsService.GetByIdAsync(1);

            //assert

            projectsTest.Id.Should().Be(1);

            mockProjectsRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        }

        /// <summary>
        /// This method tests getting the projects by term count.
        /// </summary>
        /// Author: Gabriel Revillat Zeledón
        /// Collaborator: Steven Nuñez Murillo
        [Fact]
        public async Task GetProjectByTermCountAsyncTest()
        {
            var project1 = new InvestigationProject("Proyecto1",
                                                    new DateTime(2018, 11, 20),
                                                    new DateTime(2019, 11, 20),
                                                    1,
                                                    "Descripcion1",
                                                    "Resumen1",
                                                    "default");
            var project2 = new InvestigationProject("Proyecto2",
                                                    new DateTime(2018, 11, 20),
                                                    new DateTime(2019, 11, 20),
                                                    1,
                                                    "Descripcion2",
                                                    "Resumen2",
                                                    "default");
            var project3 = new InvestigationProject("Proyecto3",
                                                    new DateTime(2018, 11, 20),
                                                    new DateTime(2019, 11, 20),
                                                    1,
                                                    "Descripcion3",
                                                    "Resumen3",
                                                    "default");
            var project4 = new InvestigationProject("Proyecto4",
                                                    new DateTime(2018, 11, 20),
                                                    new DateTime(2019, 11, 20),
                                                    1,
                                                    "Descripcion4",
                                                    "Resumen4",
                                                    "default");

            var mockProjectRepo = new Mock<IInvestigationProjectsRepository>();
            var publicService = new InvestigationProjectService(mockProjectRepo.Object);

            await publicService.AddProjectAsync(project1);
            await publicService.AddProjectAsync(project2);
            await publicService.AddProjectAsync(project3);
            await publicService.AddProjectAsync(project4);

            var count = await publicService.GetProjectsByTermCountAsync("Proyecto1", 1);

            mockProjectRepo.Verify(repo
                                   => repo.GetProjectsByTermCount("Proyecto1", 1), Times.Once);
            count.Should().Equals(3);
        }

        /// <summary>
        /// This method tests adding a project.
        /// </summary>
        /// Author: Gabriel Revillat Zeledón
        /// Collaborator: Steven Nuñez Murillo
        [Fact]
        public async Task AddProjectTest()
        {
            var projectTest = new InvestigationProject("Proyecto",
                                                    new DateTime(2018, 11, 20),
                                                    new DateTime(2019, 11, 20),
                                                    1,
                                                    "Descripcion",
                                                    "Resumen",
                                                    "default");

            var mockProjectRepo = new Mock<IInvestigationProjectsRepository>();
            var publicService = new InvestigationProjectService(mockProjectRepo.Object);

            await publicService.AddProjectAsync(projectTest);

            projectTest.Should().Be(projectTest);
            mockProjectRepo.Verify(repo => repo.SaveAsync(projectTest), Times.Once);
        }

        /// <summary>
        /// This method tests get the first projects of the list.
        /// </summary>
        /// Author: Esteban Quesada Quesada
        /// Collaborator: Sofía Castillo Campos, Gabriel Revillat
        [Fact]
        public async Task GetFirstProjects()
        {
            List<InvestigationProject> ProjectsList = new List<InvestigationProject>
            {

                new InvestigationProject {Name="Proyecto1",StartDate= new DateTime(2018, 11, 20), EndDate=new DateTime(2019, 11, 20),InvestigationGroupID = 1,Description= "Descripcion1", Summary="Resumen1", Image="default"},
                new InvestigationProject {Name="Proyecto2",StartDate= new DateTime(2019, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 1,Description= "Descripcion2", Summary="Resumen2", Image="default"},
                new InvestigationProject {Name="Proyecto3",StartDate= new DateTime(2019, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 2,Description= "Descripcion3", Summary="Resumen3", Image="default"},
                new InvestigationProject {Name="Proyecto4",StartDate= new DateTime(2020, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 3,Description= "Descripcion4", Summary="Resumen4", Image="default"},
                new InvestigationProject {Name="Proyecto5",StartDate= new DateTime(2021, 11, 20), EndDate=new DateTime(2021, 11, 20),InvestigationGroupID = 3,Description= "Descripcion5", Summary="Resumen5", Image="default"}

            };

            var mockProjectsRepository = new Mock<IInvestigationProjectsRepository>();
            var projectService = new InvestigationProjectService(mockProjectsRepository.Object);
            mockProjectsRepository.Setup(repo => repo.GetFirstProjects(1));

            //act
            var _projects = await projectService.GetFirstProjects(1);

            //assert

            mockProjectsRepository.Verify(repo => repo.GetFirstProjects(1), Times.Once);
            _projects.Count().Should().Equals(2);
        }

        /// <summary>
        /// This method tests the id of a project given a name 
        /// </summary>
        /// Author: Sofia Campos
        /// Collaborator: Gabriel Revillat, Esteban Quesada. 
        [Fact]
        public async Task GetIDByNameAsync()
        {
            List<InvestigationProject> ProjectsList = new List<InvestigationProject>
            {

                new InvestigationProject {Name="Proyecto1",StartDate= new DateTime(2018, 11, 20), EndDate=new DateTime(2019, 11, 20),InvestigationGroupID = 1,Description= "Descripcion1", Summary="Resumen1", Image="default"},
                new InvestigationProject {Name="Proyecto2",StartDate= new DateTime(2019, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 1,Description= "Descripcion2", Summary="Resumen2", Image="default"},
                new InvestigationProject {Name="Proyecto3",StartDate= new DateTime(2019, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 2,Description= "Descripcion3", Summary="Resumen3", Image="default"},
                new InvestigationProject {Name="Proyecto4",StartDate= new DateTime(2020, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 3,Description= "Descripcion4", Summary="Resumen4", Image="default"},
                new InvestigationProject {Name="Proyecto5",StartDate= new DateTime(2021, 11, 20), EndDate=new DateTime(2021, 11, 20),InvestigationGroupID = 3,Description= "Descripcion5", Summary="Resumen5", Image="default"}

            };

            var mockProjectsRepository = new Mock<IInvestigationProjectsRepository>();
            var projectService = new InvestigationProjectService(mockProjectsRepository.Object);
            mockProjectsRepository.Setup(repo => repo.GetIDByNameAsync("Proyecto4"));

            //act
            var _projects = await projectService.GetIDByNameAsync("Proyecto4");

            //assert

            mockProjectsRepository.Verify(repo => repo.GetIDByNameAsync("Proyecto4"), Times.Once);
            _projects.Should().Be(ProjectsList.ElementAt(3).Id);
        }

        /// <summary>
        /// This method tests update a project.
        /// </summary>
        /// Collaborator: 
        /// - Steven Nuñez, 33,3%
        /// - Oscar Navarro, 33,3% 
        /// - Sebastian Gonzales, 33,3%
        [Fact]
        public async Task UpdateProject()
        {
            //arrange
            var project = new InvestigationProject("Proyecto prueba",
                                                    new DateTime(2019, 11, 20),
                                                    new DateTime(2020, 12, 15),
                                                    1,
                                                    "Descripcion",
                                                    "Resumen",
                                                    "default");

            var mockProjectRepository = new Mock<IInvestigationProjectsRepository>();
            var publicService = new InvestigationProjectService(mockProjectRepository.Object);

            //act
            await publicService.AddProjectAsync(project);

            project.Name = "Proyecto prueba modificado";
            await publicService.UpdateProject(project.Id, project.Name, project.StartDate, project.EndDate, project.InvestigationGroupID, project.Description, project.Summary, project.Image);

            //assert
            project.Should().Be(project);
            mockProjectRepository.Verify(repo => repo.UpdateAsync(project.Id, project.Name, project.StartDate, project.EndDate, project.InvestigationGroupID, project.Description, project.Summary, project.Image), Times.Once);
        }

        /// <summary>
        /// 
        /// Author: Oscar Navarro Céspedes
        /// StoryID: ST-HC-1.27
        /// </summary>
        /// <returns>The completed task.</returns>
        [Fact]
        public async Task AddImageAsyncTest()
        {
            var imageTest = new ProjectsImages("img/DefaultImage.png", 1);

            var mockProjectRepo = new Mock<IInvestigationProjectsRepository>();
            var publicService = new InvestigationProjectService(mockProjectRepo.Object);

            await publicService.AddImageAsync(imageTest);

            imageTest.Should().Be(imageTest);
        }


        /// <summary>
        /// This method tests removing an image from the database
        /// by GetActiveProjectsCount.
        /// Author: Oscar Navarro
        /// StoryID: ST-HC-1.27
        /// </summary>
        /// <returns>The completed task.</returns>
        [Fact]
        public async Task RemoveImageAsync()
        {
            var mockProjectRepo = new Mock<IInvestigationProjectsRepository>();
            var publicService = new InvestigationProjectService(mockProjectRepo.Object);
            await publicService.RemoveImageAsync("img/DefaultImage.png", 2);
            var image = await publicService.GetImagesAsync(2);
            image.Should().BeNull();
        }

        [Fact]
        public async Task GetImagesAsync()
        {
            var mockProjectRepo = new Mock<IInvestigationProjectsRepository>();
            var publicService = new InvestigationProjectService(mockProjectRepo.Object);
            List<ProjectsImages> imagesList = new List<ProjectsImages>
            {
                new ProjectsImages("asdfasdf", 2)
            };

            mockProjectRepo.Setup(repo => repo.GetImagesAsync(2)).ReturnsAsync(imagesList);

            //act
            var _images = await publicService.GetImagesAsync(2);

            //assert

            mockProjectRepo.Verify(repo => repo.GetImagesAsync(2), Times.Once);
            _images.Count().Should().Be(1);
        }



        /// <summary>
        /// This method tests add image to a project.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 33,3%
        /// - Sebastian Gonzalez, 33,3% 
        /// - Steven Nunez, 33,3%
        [Fact]
        public async Task AddImageAsync()
        {
            //arrange
            var project = new InvestigationProject("Proyecto prueba",
                                                    new DateTime(2019, 11, 20),
                                                    new DateTime(2020, 12, 15),
                                                    1,
                                                    "Descripcion",
                                                    "Resumen",
                                                    "default");

            var projectImage = new ProjectsImages("img/DefaultImage.png", project.Id);

            var mockProjectRepository = new Mock<IInvestigationProjectsRepository>();
            var publicService = new InvestigationProjectService(mockProjectRepository.Object);


            //act
            await publicService.AddImageAsync(projectImage);

            //assert
            projectImage.Image.Should().Be("img/DefaultImage.png");
        }

        /// <summary>
        /// This method tests change state of a project.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 33,3%
        /// - Sebastian Gonzalez, 33,3% 
        /// - Steven Nunez, 33,3%
        [Fact]
        public async Task ChangeProjectState()
        {
            //arrange
            var project = new InvestigationProject("Proyecto prueba",
                                                    new DateTime(2019, 11, 20),
                                                    new DateTime(2020, 12, 15),
                                                    1,
                                                    "Descripcion",
                                                    "Resumen",
                                                    "default");

            var mockProjectRepository = new Mock<IInvestigationProjectsRepository>();
            var publicService = new InvestigationProjectService(mockProjectRepository.Object);


            //act
            project.Active = true;
            await publicService.ChangeProjectState(project.Id, false);
            await publicService.ChangeProjectState(project.Id,true);

            //assert
            project.Active.Should().Be(true);
        }

        /// <summary>
        /// This method tests delete a project.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 33,3%
        /// - Sebastian Gonzalez, 33,3% 
        /// - Steven Nunez, 33,3%
        [Fact]
        public async Task DeleteProject()
        {
            //arrange
            var project = new InvestigationProject("Proyecto prueba",
                                                    new DateTime(2019, 11, 20),
                                                    new DateTime(2020, 12, 15),
                                                    1,
                                                    "Descripcion",
                                                    "Resumen",
                                                    "default");
            var mockProjectRepository = new Mock<IInvestigationProjectsRepository>();
            var publicService = new InvestigationProjectService(mockProjectRepository.Object);

            //act
            await publicService.DeleteProject(project.Id);
        }


        /// <summary>
        /// This method tests get active projects.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 33,3%
        /// - Sebastian Gonzalez, 33,3% 
        /// - Steven Nunez, 33,3%
        [Fact]
        public async Task GetActiveProjectsCountAsync()
        {
            List<InvestigationProject> ProjectsList = new List<InvestigationProject>
            {

                new InvestigationProject {Name="Proyecto1",StartDate= new DateTime(2018, 11, 20), EndDate=new DateTime(2019, 11, 20),InvestigationGroupID = 1,Description= "Descripcion1", Summary="Resumen1", Image="default", Active = true},
                new InvestigationProject {Name="Proyecto2",StartDate= new DateTime(2019, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 1,Description= "Descripcion2", Summary="Resumen2", Image="default", Active = true},
                new InvestigationProject {Name="Proyecto3",StartDate= new DateTime(2019, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 2,Description= "Descripcion3", Summary="Resumen3", Image="default", Active = false},
                new InvestigationProject {Name="Proyecto4",StartDate= new DateTime(2020, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 3,Description= "Descripcion4", Summary="Resumen4", Image="default", Active = false},
                new InvestigationProject {Name="Proyecto5",StartDate= new DateTime(2021, 11, 20), EndDate=new DateTime(2021, 11, 20),InvestigationGroupID = 3,Description= "Descripcion5", Summary="Resumen5", Image="default", Active = false}

            };

            var mockProjectRepository = new Mock<IInvestigationProjectsRepository>();
            var publicService = new InvestigationProjectService(mockProjectRepository.Object);
            mockProjectRepository.Setup(repo => repo.GetActiveProjectsCount(1));
            
            //act
            var count = await publicService.GetActiveProjectsCountAsync(1);

            //assert
            count.Should().Equals(2);
        }


        /// <summary>
        /// This method tests get active projects paged.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 33,3%
        /// - Sebastian Gonzalez, 33,3% 
        /// - Steven Nunez, 33,3%
        [Fact]
        public async Task GetActiveProjectsPagedAsync()
        {
            List<InvestigationProject> ProjectsList = new List<InvestigationProject>
            {

                new InvestigationProject {Name="Proyecto1",StartDate= new DateTime(2018, 11, 20), EndDate=new DateTime(2019, 11, 20),InvestigationGroupID = 1,Description= "Descripcion1", Summary="Resumen1", Image="default", Active = true},
                new InvestigationProject {Name="Proyecto2",StartDate= new DateTime(2019, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 1,Description= "Descripcion2", Summary="Resumen2", Image="default", Active = true},
                new InvestigationProject {Name="Proyecto3",StartDate= new DateTime(2019, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 2,Description= "Descripcion3", Summary="Resumen3", Image="default", Active = false},
                new InvestigationProject {Name="Proyecto4",StartDate= new DateTime(2020, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 3,Description= "Descripcion4", Summary="Resumen4", Image="default", Active = false},
                new InvestigationProject {Name="Proyecto5",StartDate= new DateTime(2021, 11, 20), EndDate=new DateTime(2021, 11, 20),InvestigationGroupID = 3,Description= "Descripcion5", Summary="Resumen5", Image="default", Active = false}

            };

            var mockProjectRepository = new Mock<IInvestigationProjectsRepository>();
            var publicService = new InvestigationProjectService(mockProjectRepository.Object);
            mockProjectRepository.Setup(repo => repo.GetActiveProjectsPaged(1,2,1));

            //act
            var count = await publicService.GetActiveProjectsPagedAsync(1, 2, 1);

            //assert
            count.Should().Equals(2);
        }


        /// <summary>
        /// This method tests get all projects.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 33,3%
        /// - Sebastian Gonzalez, 33,3% 
        /// - Steven Nunez, 33,3%
        [Fact]
        public async Task GetAllAsync()
        {
            List<InvestigationProject> ProjectsList = new List<InvestigationProject>
            {

                new InvestigationProject {Name="Proyecto1",StartDate= new DateTime(2018, 11, 20), EndDate=new DateTime(2019, 11, 20),InvestigationGroupID = 1,Description= "Descripcion1", Summary="Resumen1", Image="default", Active = true},
                new InvestigationProject {Name="Proyecto2",StartDate= new DateTime(2019, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 1,Description= "Descripcion2", Summary="Resumen2", Image="default", Active = true},
                new InvestigationProject {Name="Proyecto3",StartDate= new DateTime(2019, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 2,Description= "Descripcion3", Summary="Resumen3", Image="default", Active = false},
                new InvestigationProject {Name="Proyecto4",StartDate= new DateTime(2020, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 3,Description= "Descripcion4", Summary="Resumen4", Image="default", Active = false},
                new InvestigationProject {Name="Proyecto5",StartDate= new DateTime(2021, 11, 20), EndDate=new DateTime(2021, 11, 20),InvestigationGroupID = 3,Description= "Descripcion5", Summary="Resumen5", Image="default", Active = false}

            };

            var mockProjectRepository = new Mock<IInvestigationProjectsRepository>();
            var publicService = new InvestigationProjectService(mockProjectRepository.Object);
            mockProjectRepository.Setup(repo => repo.GetAllAsync());

            //act
            var count = await publicService.GetAllAsync();

            //assert
            count.Should().Equals(5);
        }


        /// <summary>
        /// This method tests get projects count by id group.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 33,3%
        /// - Sebastian Gonzalez, 33,3% 
        /// - Steven Nunez, 33,3%
        [Fact]
        public async Task GetProjectsCountAsync()
        {
            List<InvestigationProject> ProjectsList = new List<InvestigationProject>
            {

                new InvestigationProject {Name="Proyecto1",StartDate= new DateTime(2018, 11, 20), EndDate=new DateTime(2019, 11, 20),InvestigationGroupID = 1,Description= "Descripcion1", Summary="Resumen1", Image="default", Active = true},
                new InvestigationProject {Name="Proyecto2",StartDate= new DateTime(2019, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 1,Description= "Descripcion2", Summary="Resumen2", Image="default", Active = true},
                new InvestigationProject {Name="Proyecto3",StartDate= new DateTime(2019, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 2,Description= "Descripcion3", Summary="Resumen3", Image="default", Active = false},
                new InvestigationProject {Name="Proyecto4",StartDate= new DateTime(2020, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 3,Description= "Descripcion4", Summary="Resumen4", Image="default", Active = false},
                new InvestigationProject {Name="Proyecto5",StartDate= new DateTime(2021, 11, 20), EndDate=new DateTime(2021, 11, 20),InvestigationGroupID = 3,Description= "Descripcion5", Summary="Resumen5", Image="default", Active = false}

            };

            var mockProjectRepository = new Mock<IInvestigationProjectsRepository>();
            var publicService = new InvestigationProjectService(mockProjectRepository.Object);
            mockProjectRepository.Setup(repo => repo.GetProjectsCount(3));

            //act
            var count = await publicService.GetProjectsCountAsync(3);

            //assert
            count.Should().Equals(2);
        }


        /// <summary>
        /// This method tests get projects by Ids list.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 33,3%
        /// - Sebastian Gonzalez, 33,3% 
        /// - Steven Nunez, 33,3%
        [Fact]
        public async Task GetProjectsFromId()
        {
            List<int> ids = new List<int>(3) {1,2,3};
            List<InvestigationProject> ProjectsList = new List<InvestigationProject>
            {

                new InvestigationProject {Name="Proyecto1",StartDate= new DateTime(2018, 11, 20), EndDate=new DateTime(2019, 11, 20),InvestigationGroupID = 1,Description= "Descripcion1", Summary="Resumen1", Image="default", Active = true},
                new InvestigationProject {Name="Proyecto2",StartDate= new DateTime(2019, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 1,Description= "Descripcion2", Summary="Resumen2", Image="default", Active = true},
                new InvestigationProject {Name="Proyecto3",StartDate= new DateTime(2019, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 2,Description= "Descripcion3", Summary="Resumen3", Image="default", Active = false},
                new InvestigationProject {Name="Proyecto4",StartDate= new DateTime(2020, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 3,Description= "Descripcion4", Summary="Resumen4", Image="default", Active = false},
                new InvestigationProject {Name="Proyecto5",StartDate= new DateTime(2021, 11, 20), EndDate=new DateTime(2021, 11, 20),InvestigationGroupID = 3,Description= "Descripcion5", Summary="Resumen5", Image="default", Active = false}

            };

            var mockProjectRepository = new Mock<IInvestigationProjectsRepository>();
            var publicService = new InvestigationProjectService(mockProjectRepository.Object);
            mockProjectRepository.Setup(repo => repo.GetProjectsFromId(ids));

            //act
            var count = await publicService.GetProjectsFromId(ids);

            //assert
            count.Should().Equals(5);
        }


        /// <summary>
        /// This method tests get projects paged.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 33,3%
        /// - Sebastian Gonzalez, 33,3% 
        /// - Steven Nunez, 33,3%
        [Fact]
        public async Task GetProjectsPagedAsync()
        {
            List<int> ids = new List<int>(3) { 1, 2, 3 };
            List<InvestigationProject> ProjectsList = new List<InvestigationProject>
            {

                new InvestigationProject {Name="Proyecto1",StartDate= new DateTime(2018, 11, 20), EndDate=new DateTime(2019, 11, 20),InvestigationGroupID = 1,Description= "Descripcion1", Summary="Resumen1", Image="default", Active = true},
                new InvestigationProject {Name="Proyecto2",StartDate= new DateTime(2019, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 1,Description= "Descripcion2", Summary="Resumen2", Image="default", Active = true},
                new InvestigationProject {Name="Proyecto3",StartDate= new DateTime(2019, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 2,Description= "Descripcion3", Summary="Resumen3", Image="default", Active = false},
                new InvestigationProject {Name="Proyecto4",StartDate= new DateTime(2020, 11, 20), EndDate=new DateTime(2020, 11, 20),InvestigationGroupID = 3,Description= "Descripcion4", Summary="Resumen4", Image="default", Active = false},
                new InvestigationProject {Name="Proyecto5",StartDate= new DateTime(2021, 11, 20), EndDate=new DateTime(2021, 11, 20),InvestigationGroupID = 3,Description= "Descripcion5", Summary="Resumen5", Image="default", Active = false}

            };

            var mockProjectRepository = new Mock<IInvestigationProjectsRepository>();
            var publicService = new InvestigationProjectService(mockProjectRepository.Object);
            mockProjectRepository.Setup(repo => repo.GetProjectsPaged(1,2,1));

            //act
            var count = await publicService.GetProjectsPagedAsync(1,2,1);

            //assert
            count.Should().Equals(2);
        }

    }
}

