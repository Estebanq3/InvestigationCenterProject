using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ResearchRepository.Application.Theses;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.Theses;
using ResearchRepository.Domain.Theses.Repositories;
using ResearchRepository.Domain.Theses.Entities;
using ResearchRepository.Application.Theses.Implementations;
using Moq;
using Xunit;
using System;
//Proyectos
using ResearchRepository.Domain.InvestigationProjects.Repositories;
using ResearchRepository.Domain.InvestigationProjects.Entities;
using ResearchRepository.Application.InvestigationProjects.Implementations;
using ResearchRepository.Domain.InvestigationProjects.DTOs;

namespace UnitTests.Application.ResearchThesisContext
{
    public class ThesisServiceTests
    {
        /// <summary>
        /// This method tests the returned theses given a term and an id of a group
        /// </summary>
        /// Author: Sebastian Gonzalez-Oscar Navarro
        [Fact]
        public async Task GetThesesByTermPagedAsyncTest()
        {
            List<Thesis> ThesesList = new List<Thesis>
            {

                new Thesis {Name="Tesis1",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary1", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia1"},
                new Thesis {Name="Tesis2",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary2", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia2"},
                new Thesis {Name="Tesis3",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary3", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia3"},
                new Thesis {Name="Tesis4",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary4", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia4"},
                new Thesis {Name="Tesis5",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary5", InvestigationGroupId = 2, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia5"}
            };

            var mockThesisRepository = new Mock<IThesesRepository>();
            var thesisService = new ThesisService(mockThesisRepository.Object);
            mockThesisRepository.Setup(repo => repo.GetThesesByTermPaged(1, 1, "Tesis", 1)).ReturnsAsync(ThesesList);


            var _theses = await thesisService.GetThesesByTermPagedAsync(1, 1, "Tesis", 1);


            mockThesisRepository.Verify(repo => repo.GetThesesByTermPaged(1, 1, "Tesis", 1), Times.Once);
            _theses.Count().Should().Equals(4);
        }

        /// <summary>
        /// This method tests getting a Thesis given their id
        /// </summary>
        /// Author: Oscar Navarro- Sebastian Gonzalez
        [Fact]
        public async Task GetThesisById()
        {
            var thesis = new Thesis(7, "Tesis1", new DateTime(2021, 11, 18), "Summary1", 1, "default", "12332432", "Maestria", "referencia1");

            var mockThesisRepository = new Mock<IThesesRepository>();
            var thesisService = new ThesisService(mockThesisRepository.Object);
            mockThesisRepository.Setup(repo => repo.GetByIdAsync(7)).ReturnsAsync(thesis);

            await thesisService.AddThesisAsync(thesis);

            var thesisTest = await thesisService.GetByIdAsync(7);
            thesisTest.Id.Should().Be(7);

            mockThesisRepository.Verify(repo => repo.GetByIdAsync(7), Times.Once);
        }

        /// <summary>
        /// This method tests the obtaining of the amount of theses associated with a term.
        /// </summary>
        /// Author: Steven Nuñez
        /// Collaborator: Gabriel Revillat
        [Fact]
        public async Task GetPublicationByTermCountAsync()
        {
            //arrange
            var thesis = new Thesis(6, "Tesis de pruebas de machine learning", DateTime.Now, "Resumen de pruebas", 1, "12345", "Default", "Posgrado", " ");
            var thesis2 = new Thesis(7, "Tesis de pruebas de pruebas automatizadas", DateTime.Now, "Resumen de pruebas2", 1, "123456", "Default", "Posgrado", " ");
            var thesis3 = new Thesis(8, "Tesis de nanotecnología", DateTime.Now, "Resumen de pruebas3", 1, "123457", "Default", "Posgrado", " ");
            var thesis4 = new Thesis(9, "Tesis de seguridad cibernetica", DateTime.Now, "Resumen de pruebas4", 1, "123458", "Default", "Posgrado", " ");

            var mockThesesRepository = new Mock<IThesesRepository>();
            var thesisService = new ThesisService(mockThesesRepository.Object);

            await thesisService.AddThesisAsync(thesis);
            await thesisService.AddThesisAsync(thesis2);
            await thesisService.AddThesisAsync(thesis3);
            await thesisService.AddThesisAsync(thesis4);

            //act
            var count = await thesisService.GetThesesByTermCountAsync("pruebas", 1);

            //assert
            mockThesesRepository.Verify(thesisRepo => thesisRepo.GetThesesByTermCount("pruebas", 1), Times.Once);
            count.Should().Equals(2);

        }

        /// <summary>
        /// This method tests a thesis update by name.
        /// </summary>
        /// Author: Steven Nuñez
        /// Collaborator: Gabriel Revillat
        [Fact]
        public async Task UpdateThesis()
        {
            //arrange
            var thesis = new Thesis("Tesis de pruebas de machine learning", DateTime.Now, "Resumen de pruebas", 1, "12345", "Default", "Posgrado", "s", null, " ");

            var mockThesesRepository = new Mock<IThesesRepository>();
            var thesisService = new ThesisService(mockThesesRepository.Object);

            //act
            await thesisService.AddThesisAsync(thesis);

            thesis.Name = "Tesis de pruebas modificado";

            await thesisService.UpdateThesisAsync(thesis.Id, thesis.Name, thesis.PublicationDate, thesis.Summary, thesis.InvestigationGroupId, thesis.DOI, thesis.Image, thesis.Type, thesis.Reference, thesis.Attachment, thesis.AttachmentName);

            //assert
            thesis.Should().Be(thesis);
            mockThesesRepository.Verify(thesisRepo => thesisRepo.UpdateAsync(thesis.Id, thesis.Name, thesis.PublicationDate, thesis.Summary, thesis.InvestigationGroupId, thesis.DOI, thesis.Image, thesis.Type, thesis.Reference, thesis.Attachment, thesis.AttachmentName), Times.Once);
        }

        private static IEnumerable<Thesis> GetTheses()
        {
            const int publicationsCount = 1000;
            for (int i = 0; i < publicationsCount; ++i)
            {
                yield return new Thesis(i + 11, "Pruebas", DateTime.Now, "Pruebas", 1, "12345", "Default", "Posgrado", " ");
            }
        }

        /// <summary>
        /// This method tests the theses returned per page given a term and a group ID
        /// </summary>
        /// Author: Steven Nuñez
        /// Collaborator: Gabriel Revillat
        [Fact]
        public async Task GetThesisByTermPagedFindCorrectTerm()
        {
            //arrange
            int groupId = 1;
            int currentPage = 1;
            int size = 10;
            string term = "Pruebas";

            var theses = GetTheses().ToList();
            var mockTeamRepository = new Mock<IThesesRepository>();
            mockTeamRepository.Setup(thesisRepo => thesisRepo.GetThesesByTermPaged(currentPage, size, term, groupId)).ReturnsAsync(theses);
            var thesisService = new ThesisService(mockTeamRepository.Object);

            //act
            var result = await thesisService.GetThesesByTermPagedAsync(currentPage, size, term, groupId);

            //assert
            result.Should().BeEquivalentTo(theses);
        }

        /// <summary>
        /// This method tests get the ids of projects asociated to a thesis.
        /// </summary>
        /// Author: SebastianGonzalez
        /// Collaborator: Oscar Navarro and Steven Nuñez
        [Fact]
        public async Task GetProjectFromThesisId()
        {
            int groupId = 1;
            IList<int> projects = new List<int>();
            var mockTeamRepository = new Mock<IThesesRepository>();
            mockTeamRepository.Setup(thesisRepo => thesisRepo.GetProjectFromThesisId(groupId)).ReturnsAsync(projects);
            var thesisService = new ThesisService(mockTeamRepository.Object);

            var result = await thesisService.GetProjectFromThesisId(groupId);

            result.Should().BeEquivalentTo(projects);
        }


        /// <summary>
        /// This method tests delete a project part of thesis.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 100%
        [Fact]
        public async Task DeleteProjectPartOfThesis()
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

            var mockTeamRepository = new Mock<IThesesRepository>();
            var thesisService = new ThesisService(mockTeamRepository.Object);
            await thesisService.DeleteProjectPartOfThesis(project.Id);

        }

        /// <summary>
        /// This method tests delete a thesis.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 100%
        [Fact]
        public async Task DeleteThesis()
        {
            var thesis = new Thesis(6, "Tesis de pruebas de machine learning", DateTime.Now, "Resumen de pruebas", 1, "12345", "Default", "Posgrado", " ");

            var mockTeamRepository = new Mock<IThesesRepository>();
            var thesisService = new ThesisService(mockTeamRepository.Object);
            await thesisService.DeleteThesis(thesis.Id);

        }

        /// <summary>
        /// This method tests delete a thesis part of project.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 100%
        [Fact]
        public async Task DeleteThesisPartOfProject()
        {
            var thesis = new Thesis(6, "Tesis de pruebas de machine learning", DateTime.Now, "Resumen de pruebas", 1, "12345", "Default", "Posgrado", " ");
            var mockTeamRepository = new Mock<IThesesRepository>();
            var thesisService = new ThesisService(mockTeamRepository.Object);
            await thesisService.DeleteThesisPartOfProject(thesis.Id);
        }

        /// <summary>
        /// This method test get all thesis.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 100%
        [Fact]
        public async Task GetAllAsync()
        {
            List<Thesis> ThesesList = new List<Thesis>
            {

                new Thesis {Name="Tesis1",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary1", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia1"},
                new Thesis {Name="Tesis2",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary2", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia2"},
                new Thesis {Name="Tesis3",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary3", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia3"},
                new Thesis {Name="Tesis4",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary4", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia4"},
                new Thesis {Name="Tesis5",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary5", InvestigationGroupId = 2, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia5"}
            };

            var mockThesisRepository = new Mock<IThesesRepository>();
            var thesisService = new ThesisService(mockThesisRepository.Object);
            mockThesisRepository.Setup(repo => repo.GetAllAsync());

            var _theses = await thesisService.GetAllAsync();

            mockThesisRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            _theses.Count().Should().Equals(5);
        }


        /// <summary>
        /// This method test get projects part of thesis by id of thesis.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 100%
        [Fact]
        public async Task GetAsyncProjectsPartOfThesisFromId()
        {
            var thesis = new Thesis(6, "Tesis de pruebas de machine learning", DateTime.Now, "Resumen de pruebas", 1, "12345", "Default", "Posgrado", " ");
            var mockTeamRepository = new Mock<IThesesRepository>();
            var thesisService = new ThesisService(mockTeamRepository.Object);
            await thesisService.GetAsyncProjectsPartOfThesisFromId(thesis.Id);

        }

        /// <summary>
        /// This method test get thesis part of project by id of project.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 100%
        [Fact]
        public async Task GetAsyncThesisPartOfProjectFromId()
        {
            var project = new InvestigationProject("Proyecto prueba",
                                                    new DateTime(2019, 11, 20),
                                                    new DateTime(2020, 12, 15),
                                                    1,
                                                    "Descripcion",
                                                    "Resumen",
                                                    "default");
            var thesis = new Thesis(6, "Tesis de pruebas de machine learning", DateTime.Now, "Resumen de pruebas", 1, "12345", "Default", "Posgrado", " ");
            var mockTeamRepository = new Mock<IThesesRepository>();
            var thesisService = new ThesisService(mockTeamRepository.Object);
            var count = await thesisService.GetAsyncThesisPartOfProjectFromId(project.Id);

            count.Should().Equals(0);

        }

        /// <summary>
        /// This method test get thesis by name.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 100%
        [Fact]
        public async Task GetByNameAsync()
        {
            var thesis = new Thesis(6, "Tesis de pruebas de machine learning", DateTime.Now, "Resumen de pruebas", 1, "12345", "Default", "Posgrado", " ");
            var mockTeamRepository = new Mock<IThesesRepository>();
            var thesisService = new ThesisService(mockTeamRepository.Object);
            var obtainThesis = await thesisService.GetByNameAsync(thesis.Name, thesis.PublicationDate);

            thesis.Should().Equals(obtainThesis);

        }

        /// <summary>
        /// This method test get thesis by id group.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 100%
        [Fact]
        public async Task GetThesesByGroupIDAsync()
        {
            List<Thesis> ThesesList = new List<Thesis>
            {

                new Thesis {Name="Tesis1",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary1", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia1"},
                new Thesis {Name="Tesis2",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary2", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia2"},
                new Thesis {Name="Tesis3",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary3", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia3"},
                new Thesis {Name="Tesis4",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary4", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia4"},
                new Thesis {Name="Tesis5",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary5", InvestigationGroupId = 2, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia5"}
            };
            var mockTeamRepository = new Mock<IThesesRepository>();
            var thesisService = new ThesisService(mockTeamRepository.Object);
            var count = await thesisService.GetThesesByGroupIDAsync(1);

            count.Should().Equals(4);

        }

        /// <summary>
        /// This method test get thesis by id group.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 100%
        [Fact]
        public async Task GetThesesCountAsync()
        {
            List<Thesis> ThesesList = new List<Thesis>
            {

                new Thesis {Name="Tesis1",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary1", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia1"},
                new Thesis {Name="Tesis2",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary2", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia2"},
                new Thesis {Name="Tesis3",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary3", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia3"},
                new Thesis {Name="Tesis4",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary4", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia4"},
                new Thesis {Name="Tesis5",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary5", InvestigationGroupId = 2, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia5"}
            };
            var mockTeamRepository = new Mock<IThesesRepository>();
            var thesisService = new ThesisService(mockTeamRepository.Object);
            var count = await thesisService.GetThesesCountAsync(1);

            count.Should().Equals(4);

        }

        /// <summary>
        /// This method test get thesis by multiple ids of groups.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 100%
        [Fact]
        public async Task GetThesesFromIds()
        {
            List<int> ids = new List<int>(3) { 1, 2, 3 };

            List<Thesis> ThesesList = new List<Thesis>
            {
                new Thesis {Name="Tesis1",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary1", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia1"},
                new Thesis {Name="Tesis2",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary2", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia2"},
                new Thesis {Name="Tesis3",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary3", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia3"},
                new Thesis {Name="Tesis4",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary4", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia4"},
                new Thesis {Name="Tesis5",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary5", InvestigationGroupId = 2, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia5"}
            };
            var mockTeamRepository = new Mock<IThesesRepository>();
            var thesisService = new ThesisService(mockTeamRepository.Object);
            var count = await thesisService.GetThesesFromIds(ids);

            count.Should().Equals(5);

        }

        /// <summary>
        /// This method test get thesis paged.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 100%
        [Fact]
        public async Task GetThesesPagedAsync()
        {

            List<Thesis> ThesesList = new List<Thesis>
            {
                new Thesis {Name="Tesis1",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary1", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia1"},
                new Thesis {Name="Tesis2",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary2", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia2"},
                new Thesis {Name="Tesis3",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary3", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia3"},
                new Thesis {Name="Tesis4",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary4", InvestigationGroupId = 1, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia4"},
                new Thesis {Name="Tesis5",PublicationDate = new DateTime(2021, 11, 18), Summary = "Summary5", InvestigationGroupId = 2, Image="default", DOI = "12332432", Type = "Maestria", Reference = "referencia5"}
            };
            var mockTeamRepository = new Mock<IThesesRepository>();
            var thesisService = new ThesisService(mockTeamRepository.Object);
            var thesisListResult = await thesisService.GetThesesPagedAsync(1, 4, 1);

            thesisListResult.Should().Equals(4);

        }

        /// <summary>
        /// This method test get thesis by thesisPartOfProject.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 100%
        [Fact]
        public async Task GetThesesOfProjectById()
        {
            var project = new InvestigationProject("Proyecto prueba",
                                                    new DateTime(2019, 11, 20),
                                                    new DateTime(2020, 12, 15),
                                                    1,
                                                    "Descripcion",
                                                    "Resumen",
                                                    "default");

            var thesis1 = new Thesis(6, "Tesis de pruebas de machine learning", DateTime.Now, "Resumen de pruebas", 1, "12345", "Default", "Posgrado", " ");
            var thesis2 = new Thesis(7, "Tesis de pruebas de pruebas automatizadas", DateTime.Now, "Resumen de pruebas2", 1, "123456", "Default", "Posgrado", " ");
            var thesis3 = new Thesis(8, "Tesis de nanotecnología", DateTime.Now, "Resumen de pruebas3", 1, "123457", "Default", "Posgrado", " ");

            var thesisPartOfProject1 = new ThesisPartOfProject(project.Id, thesis1.Id);
            var thesisPartOfProject2 = new ThesisPartOfProject(project.Id, thesis2.Id);
            var thesisPartOfProject3 = new ThesisPartOfProject(project.Id, thesis3.Id);

            List<ThesisPartOfProject> ThesesList = new List<ThesisPartOfProject>();

            ThesesList.Add(thesisPartOfProject1);
            ThesesList.Add(thesisPartOfProject2);
            ThesesList.Add(thesisPartOfProject3);


            var mockTeamRepository = new Mock<IThesesRepository>();
            var thesisService = new ThesisService(mockTeamRepository.Object);
            var thesisListResult = await thesisService.GetThesisOfProjectById(ThesesList);

            ThesesList.Should().Equals(3);

        }

        /// <summary>
        /// This method test get project saved by thesisPartOfProject.
        /// </summary>
        /// Collaborator: 
        /// - Esteban Quesada, 100%
        [Fact]
        public async Task SaveProjectPartOfThesisAsync()
        {
            var project = new InvestigationProject("Proyecto prueba",
                                                    new DateTime(2019, 11, 20),
                                                    new DateTime(2020, 12, 15),
                                                    1,
                                                    "Descripcion",
                                                    "Resumen",
                                                    "default");

            var thesis1 = new Thesis(6, "Tesis de pruebas de machine learning", DateTime.Now, "Resumen de pruebas", 1, "12345", "Default", "Posgrado", " ");
            var thesisPartOfProject1 = new ThesisPartOfProject(project.Id, thesis1.Id);
            var mockTeamRepository = new Mock<IThesesRepository>();
            var thesisService = new ThesisService(mockTeamRepository.Object);
            await thesisService.SaveProjectPartOfThesisAsync(thesisPartOfProject1);
        }
    }
}

