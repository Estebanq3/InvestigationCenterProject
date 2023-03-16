using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ResearchRepository.Application.PublicationContext;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.PublicationContext;
using ResearchRepository.Domain.PublicationContext.Repositories;
using Moq;
using Xunit;
using System;
using ResearchRepository.Application.PublicationContext.Implementation;
using ResearchRepository.Domain.PublicationContext.Entities;

namespace UnitTests.Application.PublicationContext
{
    public class PublicationServiceTests
    {
        private static readonly string DOI = "DOI prueba";
        private static readonly string PubliName = "Publication Name";
        private static readonly string PubliSummary = "term";
        private static readonly DateTime PubliYear = DateTime.Now;
        private static readonly string PubliTypePublication = "Conference";
        private static readonly string PubliJorunalConference = "Name Conference";
        private static readonly int PubliResearchGroup = 1;


        private static IEnumerable<int> GetPublicationPartOfTesis()
        {
            const int tesisCount = 1000;
            for (int i = 0; i < tesisCount; ++i)
            {
                PublicationPartOfTesis test = new PublicationPartOfTesis();
                test.PublicationId = DOI;
                test.ThesisId = i;
                yield return test.ThesisId;
            }
        }

        private static IEnumerable<int> GetProjectsAsociate()
        {
            const int tesisCount = 1000;
            for (int i = 0; i < tesisCount; ++i)
            {
                ProjectAsociatedToPublication test = new ProjectAsociatedToPublication();
                test.PublicationId = DOI;
                test.InvestigationProjectId = i;
                yield return test.InvestigationProjectId;
            }
        }

        private static IEnumerable<Publication> GetPublications()
        {
            const int publicationsCount = 1000;
            for (int i = 0; i < publicationsCount; ++i)
            {
                yield return new Publication(PubliName, PubliSummary, PubliYear, PubliTypePublication, PubliJorunalConference, i.ToString(), PubliResearchGroup, null, null, null);
            }
        }
        /// <summary>
        /// UnitTest for get PublicationPartOfTesis by id publication
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-4.4
        [Fact]
        public async Task GetPublicationPartOfTesisAsyncShouldReturnPublicationPartOfTesis()
        {
            // arrange
            var teams = GetPublicationPartOfTesis().ToList();
            var mockPublicationRepository = new Mock<IPublicationRepository>();
            mockPublicationRepository.Setup(repo =>repo.GetPublicationPartOfTesisAsync(DOI)).ReturnsAsync(teams);
            var teamService = new PublicationService(mockPublicationRepository.Object);
            // act
            var results = await teamService.GetPublicationPartOfTesisAsync(DOI);
            // assert
            results.Should().BeEquivalentTo(teams);
        }
        /// <summary>
        /// unitTest for get ProjectsAsociated by id publication
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-4.5
        [Fact]
        public async Task ProjectAsociatedToPublicationShouldReturnProjectAsociatedToPublication()
        {
            // arrange
            var teams = GetProjectsAsociate().ToList();
            var mockPublicationRepository = new Mock<IPublicationRepository>();
            mockPublicationRepository.Setup(repo =>
            repo.GetProjectsAsociatedAsync(DOI)).ReturnsAsync(teams);
            var teamService = new PublicationService(mockPublicationRepository.Object);
            // act
            var results = await teamService.GetProjectsAsociatedAsync(DOI);
            // assert
            results.Should().BeEquivalentTo(teams);
        }
        /// <summary>
        /// IntegrationTest for add tesis asociate to publication
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-4.5
        [Fact]
        public async Task AddPublicationPartOfTesisAsync()
        {
            //arrange
            const int id = 1;
            var publicationPartOfTesis = new PublicationPartOfTesis();
            publicationPartOfTesis.ThesisId = id;
            publicationPartOfTesis.PublicationId = DOI;
            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var publicService = new PublicationService(mockPublicationRepository.Object);
            //act
            await publicService.AddPublicationPartOfTesisAsync(publicationPartOfTesis);
            //assert
            publicationPartOfTesis.Should().Be(publicationPartOfTesis);
            mockPublicationRepository.Verify(repo => repo.SavePublicationPartOfTesisAsync(publicationPartOfTesis), Times.Once);
        }
        /// <summary>
        /// IntegrationTest for add publication
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-4.1
        [Fact]
        public async Task AddPublication()
        {
            //arrange
            var publication = new Publication("Prueba", "summary prueba", DateTime.Now,
                "Jornal", "name Journal", DOI, 5, null, null, null);

            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var publicService = new PublicationService(mockPublicationRepository.Object);
            //act
            await publicService.AddPublicationAsync(publication);
            //assert
            publication.Should().Be(publication);
            mockPublicationRepository.Verify(repo => repo.SaveAsync(publication), Times.Once);
        }
        [Fact]
        public async Task UndatePublication()
        {
            //arrange
            var publication = new Publication("Prueba", "summary prueba", DateTime.Now,
                "Jornal", "name Journal", DOI, 5, null, null, null);

            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var publicService = new PublicationService(mockPublicationRepository.Object);
            //act
            await publicService.AddPublicationAsync(publication);

            publication.Name = "nameModificado";
            await publicService.UndatePublicationAsync(publication);

            //assert
            publication.Should().Be(publication);
            mockPublicationRepository.Verify(repo => repo.UndatePublicationAsync(publication), Times.Once);
        }

        [Fact]
        public async Task GetPublicationPagedAsyncTest()
        {
            //arrange
            DateTime dt1 = new DateTime(2007, 8, 10);
            DateTime dt2 = new DateTime(2016, 12, 5);
            DateTime dt3 = new DateTime(2011, 7, 27);
            DateTime dt4 = new DateTime(2017, 3, 17);
            DateTime dt5 = new DateTime(2015, 5, 22);
            DateTime dt6 = new DateTime(2020, 1, 15);
            DateTime dt7 = new DateTime(2021, 1, 17);

            List<Publication> publicationsList = new List<Publication>
            {
                new Publication { Id="111.111", Name="Prueba1", Year = dt1, ResearchGroupId = 1},
                new Publication { Id="222.222", Name="Prueba2", Year = dt2, ResearchGroupId = 1},
                new Publication { Id="333.333", Name="Prueba3", Year = dt3, ResearchGroupId = 1},
                new Publication { Id="444.444", Name="Prueba4", Year = dt4, ResearchGroupId = 1},
                new Publication { Id="555.555", Name="Prueba5", Year = dt5, ResearchGroupId = 1},
                new Publication { Id="666.666", Name="Prueba6", Year = dt6, ResearchGroupId = 1},
                new Publication { Id="777.777", Name="Prueba7", Year = dt7, ResearchGroupId = 1}

            };

            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var publicService = new PublicationService(mockPublicationRepository.Object);
            mockPublicationRepository.Setup(repo => repo.GetPublicationPagedAsync(1, 4, 2)).ReturnsAsync(publicationsList);

            //act
            var _publications = await publicService.GetPublicationPagedAsync(1, 4, 2);

            //assert

            mockPublicationRepository.Verify(repo => repo.GetPublicationPagedAsync(1, 4, 2), Times.Once);
            _publications.Count().Should().Equals(1);

        }

        [Fact]
        public async Task GetPublicationCountAsyncTest()
        {
            //arrange
            var publication = new Publication("Prueba", "summary prueba", DateTime.Now,
                "Journal", "name Journal", DOI, 1, null, null, null);
            var publication2 = new Publication("Prueba2", "summary prueba", DateTime.Now,
                "Journal", "name Journal", DOI, 1, null, null, null);
            var publication3 = new Publication("Prueba3", "summary prueba", DateTime.Now,
                "Journal", "name Journal", DOI, 2, null, null, null);
            var publication4 = new Publication("Prueba4", "summary prueba", DateTime.Now,
                "Journal", "name Journal", DOI, 3, null, null, null);

            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var publicService = new PublicationService(mockPublicationRepository.Object);

            await publicService.AddPublicationAsync(publication);
            await publicService.AddPublicationAsync(publication2);
            await publicService.AddPublicationAsync(publication3);
            await publicService.AddPublicationAsync(publication4);

            //act
            var count = await publicService.GetPublicationCountAsync(1);

            //assert
            mockPublicationRepository.Verify(repo => repo.GetPublicationCountAsync(1), Times.Once);
            count.Should().Equals(2);
        }

        [Fact]
        public async Task getPublicationByGroupTest()
        {
            List<Publication> publicationsList = new List<Publication>
            {

                new Publication { Id="111.111", Name="Prueba1", Year = DateTime.Now, ResearchGroupId = 1},
                new Publication { Id="222.222", Name="Prueba2", Year = DateTime.Now, ResearchGroupId = 2},
                new Publication { Id="333.333", Name="Prueba3", Year = DateTime.Now, ResearchGroupId = 5},
                new Publication { Id="444.444", Name="Prueba4", Year = DateTime.Now, ResearchGroupId = 3},
                new Publication { Id="555.555", Name="Prueba5", Year = DateTime.Now, ResearchGroupId = 5},
                new Publication { Id="666.666", Name="Prueba6", Year = DateTime.Now, ResearchGroupId = 5}

            };

            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var publicService = new PublicationService(mockPublicationRepository.Object);
            mockPublicationRepository.Setup(repo => repo.getPublicationByGroup(5)).ReturnsAsync(publicationsList);

            //act
            var _publications = await publicService.getPublicationByGroup(5);

            //assert
            mockPublicationRepository.Verify(repo => repo.getPublicationByGroup(5), Times.Once);
            _publications.Count().Should().Equals(3);
        }

        [Fact]
        public async Task GetPublicationByTermCountAsyncTest()
        {
            //arrange
            var publication = new Publication("Prueba", "summary prueba", DateTime.Now,
                "Jornal", "name Journal", DOI, 5, null, null, null);
            var publication2 = new Publication("Prueba2", "summary prueba", DateTime.Now,
               "Jornal", "name Journal", DOI, 5, null, null, null);
            var publication3 = new Publication("Prueba3", "summary prueba", DateTime.Now,
               "Jornal", "name Journal", DOI, 5, null, null, null);
            var publication4 = new Publication("Test", "summary prueba", DateTime.Now,
               "Jornal", "name Journal", DOI, 5, null, null, null);

            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var publicService = new PublicationService(mockPublicationRepository.Object);

            await publicService.AddPublicationAsync(publication);
            await publicService.AddPublicationAsync(publication2);
            await publicService.AddPublicationAsync(publication3);
            await publicService.AddPublicationAsync(publication4);
            //act

            var count = await publicService.GetPublicationByTermCountAsync(1,"Prueba");

            //assert

            mockPublicationRepository.Verify(repo => repo.GetPublicationByTermCountAsync(1, "Prueba"), Times.Once);
            count.Should().Equals(3);

        }


        [Fact]
        public async Task GetPublicationByTermPagedAsyncTest() {

            //arrange

            DateTime dt1 = new DateTime(2015, 12, 31);
            DateTime dt2 = new DateTime(2018, 11, 27);
            DateTime dt3 = new DateTime(2010, 4, 7);
            DateTime dt4 = new DateTime(2013, 10, 7);
            DateTime dt5 = new DateTime(2013, 10, 8);

            List<Publication> publicationsList = new List<Publication>
            {

                new Publication { Id="111.111", Name="Prueba1", Year = dt1},
                new Publication { Id="111.222", Name="Prueba2", Year = dt2},
                new Publication { Id="111.333", Name="Prueba3", Year = dt3},
                new Publication { Id="111.444", Name="Prueba4", Year = dt4},
                new Publication { Id="111.555", Name="Prueba5", Year = dt5}

            };

            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var publicService = new PublicationService(mockPublicationRepository.Object);
            mockPublicationRepository.Setup(repo => repo.GetPublicationByTermPagedAsync(1, 1, 3, "prueba")).ReturnsAsync(publicationsList);

            //act
            var _publications = await publicService.GetPublicationByTermPagedAsync(1, 1, 3, "prueba");

            //assert

            mockPublicationRepository.Verify(repo => repo.GetPublicationByTermPagedAsync(1, 1, 3, "prueba"), Times.Once);
            _publications.Count().Should().Equals(3);
        }


        [Fact]
        public async Task IdEqualTest() {

            //arrange
            var publication = new Publication("Prueba", "summary prueba", DateTime.Now,
                "Jornal", "name Journal", "123", 5, null, null, null);


            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var publicService = new PublicationService(mockPublicationRepository.Object);
            mockPublicationRepository.Setup(repo => repo.IdEqual("123")).ReturnsAsync(publication);

            await publicService.AddPublicationAsync(publication);

            //act

            var publicationTest = await publicService.IdEqual("123");

            //assert

            publicationTest.Id.Should().Be("123");

            mockPublicationRepository.Verify(repo => repo.IdEqual("123"), Times.Once);
        }


        [Fact]
        public async Task GetPublicationByAuthorTest() {

            //arrange

            DateTime dt1 = new DateTime(2015, 12, 31);
            DateTime dt2 = new DateTime(2018, 11, 27);
            DateTime dt3 = new DateTime(2010, 4, 7);
            DateTime dt4 = new DateTime(2013, 10, 7);
            DateTime dt5 = new DateTime(2013, 10, 8);

            List<Publication> publicationsList = new List<Publication>
            {

                new Publication { Id="111.111", Name="Prueba1", Year = dt1,ResearchGroupId = 1},
                new Publication { Id="111.222", Name="Prueba2", Year = dt2,ResearchGroupId = 1},
                new Publication { Id="111.333", Name="Prueba3", Year = dt3,ResearchGroupId = 1},
                new Publication { Id="111.444", Name="Prueba4", Year = dt4,ResearchGroupId = 1},
                new Publication { Id="111.555", Name="Prueba5", Year = dt5,ResearchGroupId = 1}

            };

            List<string> dois = new List<string>(new string[] { "111.111", "111.222"});



            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var publicService = new PublicationService(mockPublicationRepository.Object);
            mockPublicationRepository.Setup(repo => repo.GetPublicationByAuthor(dois,1)).ReturnsAsync(publicationsList);

            //act
            var _publications = await publicService.GetPublicationByAuthor(dois, 1);

            //assert

            mockPublicationRepository.Verify(repo => repo.GetPublicationByAuthor(dois, 1), Times.Once);
            _publications.Count().Should().Equals(2);


        }

        [Fact]
        public async Task AddProjectAsociatedAsyncShouldAddProject()
        {
            //arrange
            const int id = 1;
            var projectAsociated = new ProjectAsociatedToPublication();
            projectAsociated.InvestigationProjectId = id;
            projectAsociated.PublicationId = DOI;
            var mockTeamRepository = new Mock<IPublicationRepository>();
            var teamService = new PublicationService(mockTeamRepository.Object);
            //act
            await teamService.AddProjectAsociatedAsync(projectAsociated);
            //assert
            projectAsociated.Should().Be(projectAsociated);
            mockTeamRepository.Verify(repo => repo.SaveProjectAsociatedAsync(projectAsociated), Times.Once);
        }

        [Fact]
        public async Task GetPublicationsFromIdShouldHavePublications()
        {
            //arrange
            var publications = GetPublications().ToList();
            IList<string> ids = new List<string>();
            var mockTeamRepository = new Mock<IPublicationRepository>();
            mockTeamRepository.Setup(repo => repo.GetPublicationsFromId(ids)).ReturnsAsync(publications);
            var publicationService = new PublicationService(mockTeamRepository.Object);
            //act
            var result = await publicationService.GetPublicationsFromId(ids);
            //assert
            result.Should().BeEquivalentTo(publications);
        }

        [Fact]
        public async Task GetPublicationsByTermPagedSummaryFindCorrectTerm()
        {
            //arrange
            int groupId = 1;
            int currentPage = 1;
            int size = 10;
            string term = "term";
            var publications = GetPublications().ToList();
            var mockTeamRepository = new Mock<IPublicationRepository>();
            mockTeamRepository.Setup(repo => repo.GetPublicationByTermPagedSummary(groupId, currentPage, size, term)).ReturnsAsync(publications);
            var publicationService = new PublicationService(mockTeamRepository.Object);
            //act
            var result = await publicationService.GetPublicationByTermPagedSummary(groupId, currentPage, size, term);
            //assert
            result.Should().BeEquivalentTo(publications);
        }

        [Fact]
        public async Task GetPublicationByTermCountAsyncSummaryCorrectCount()
        {
            int counter = 0;
            int id = 1;
            string term = "term";
            var publications = GetPublications().ToList();
            var mockTeamRepository = new Mock<IPublicationRepository>();
            mockTeamRepository.Setup(repo => repo.GetPublicationByTermCountAsyncSummary(id, term)).ReturnsAsync(counter);
            var publicationService = new PublicationService(mockTeamRepository.Object);
            //act
            var result = await publicationService.GetPublicationByTermCountAsyncSummary(id, term);
            //assert
            result.Should().Be(counter);
        }

        //ST-PH-3.8 Muestra de las referencias asociadas a una publicación.
        //Technical tasks: Generar pruebas automatizadas para esta historia.
        //Contributors: Christian Rojas & David Sánchez.
        [Fact]
        public async Task GetReferencesByIdTest()
        {

            //arrange
            DateTime dt1 = new DateTime(2015, 12, 31);
            var publicationPrueba = new Publication { Id = "111.111", Name = "Prueba1", Year = dt1, ResearchGroupId = 1 };


            List<ReferenceListPublication> listReferences = new List<ReferenceListPublication>
            {

                new ReferenceListPublication {IdPublication ="111.111", Order= 1,Reference= "Referencia",publication = publicationPrueba }

            };

            string DOI = "111.111";
            var mockTeamRepository = new Mock<IPublicationRepository>();
            mockTeamRepository.Setup(repo => repo.GetReferencesById(DOI)).ReturnsAsync(listReferences);
            var publicationService = new PublicationService(mockTeamRepository.Object);

            //act
            var result = await publicationService.GetReferencesById(DOI);

            //assert
            result.Count().Should().Be(1);
        }

        /// <summary>
        /// Unit test for DeletePublicationPartOfTesisAsync
        /// </summary>
        /// Author: Elvis Badilla & Diana Luna
        /// StoryID: ST-PH-4.11
        [Fact]
        public async Task DeletePublicationPartOfThesis()
        {
            const int id = 1;
            var publicationPartOfTesis = new PublicationPartOfTesis();
            publicationPartOfTesis.ThesisId = id;
            publicationPartOfTesis.PublicationId = DOI;
            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var publicService = new PublicationService(mockPublicationRepository.Object);
            //act
            await publicService.AddPublicationPartOfTesisAsync(publicationPartOfTesis);
            //assert
            publicationPartOfTesis.Should().Be(publicationPartOfTesis);

            await publicService.DeletePublicationPartOfThesis(publicationPartOfTesis.PublicationId);
            //assert
            publicationPartOfTesis.Should().Be(publicationPartOfTesis);

            mockPublicationRepository.Verify(repo => repo.DeletePublicationPartOfThesis(publicationPartOfTesis.PublicationId), Times.Once);
        }

        [Fact]
        /// <summary>
        /// Unit test for DeletePublicationPartOfProject
        /// </summary>
        /// Author: Elvis Badilla & Diana Luna
        /// StoryID: ST-PH-4.12
        public async Task DeletePublicationPartOfProject()
        {
            const int id = 1;
            var publicationPartOfProject = new ProjectAsociatedToPublication();
            publicationPartOfProject.InvestigationProjectId = id;
            publicationPartOfProject.PublicationId = DOI;
            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var publicService = new PublicationService(mockPublicationRepository.Object);
            //act
            await publicService.AddProjectAsociatedAsync(publicationPartOfProject);
            //assert
            publicationPartOfProject.Should().Be(publicationPartOfProject);

            await publicService.DeletePublicationPartOfThesis(publicationPartOfProject.PublicationId);
            //assert
            publicationPartOfProject.Should().Be(publicationPartOfProject);

            mockPublicationRepository.Verify(repo => repo.DeletePublicationPartOfThesis(publicationPartOfProject.PublicationId), Times.Once);
        }
        /// <summary>
        /// IntegrationTest for add publication with id exist
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-4.1
        [Fact]
        public async Task AddPublicationIdExist()
        {
            //arrange
            var publication = new Publication("Prueba", "summary prueba", DateTime.Now,
                "Jornal", "name Journal", DOI, 5, null, null, null);
            var publication2 = new Publication("Prueba", "summary prueba", DateTime.Now,
               "Jornal", "name Journal", DOI, 5, null, null, null);
            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var publicService = new PublicationService(mockPublicationRepository.Object);
            //act
            await publicService.AddPublicationAsync(publication);
            publication.Should().Be(publication);
            await publicService.AddPublicationAsync(publication2);
            //assert
            publication2.Should().Be(publication2);
            mockPublicationRepository.Verify(repo => repo.SaveAsync(publication), Times.Once);
            mockPublicationRepository.Verify(repo => repo.SaveAsync(publication2), Times.Once);
        }
        /// <summary>
        /// UnitTest for get PublicationPartOfTesis by id publication no Exist
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-4.4
        [Fact]
        public async Task GetPublicationPartOfTesisByIdNoExist()
        {
            // arrange
            var teams = GetPublicationPartOfTesis().ToList();
            var mockPublicationRepository = new Mock<IPublicationRepository>();
            mockPublicationRepository.Setup(repo => repo.GetPublicationPartOfTesisAsync("no existe")).ReturnsAsync(teams);
            var teamService = new PublicationService(mockPublicationRepository.Object);
            // act
            var results = await teamService.GetPublicationPartOfTesisAsync("no existe");
            // assert
            results.Should().BeEquivalentTo(teams);
        }
        /// <summary>
        /// unitTest for get ProjectsAsociated by id publication no Exist
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-4.5
        [Fact]
        public async Task ProjectAsociatedToPublicationIdNoExist()
        {
            // arrange
            var teams = GetProjectsAsociate().ToList();
            var mockPublicationRepository = new Mock<IPublicationRepository>();
            mockPublicationRepository.Setup(repo =>
            repo.GetProjectsAsociatedAsync("No existe")).ReturnsAsync(teams);
            var teamService = new PublicationService(mockPublicationRepository.Object);
            // act
            var results = await teamService.GetProjectsAsociatedAsync("No existe");
            // assert
            results.Should().BeEquivalentTo(teams);
        }
        /// <summary>
        /// IntegrationTest for add tesis asociate to publication That Exists
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-4.5
        [Fact]
        public async Task AddPublicationPartOfTesisThatExists()
        {
            //arrange
            const int id = 1;
            var publicationPartOfTesis = new PublicationPartOfTesis();
            publicationPartOfTesis.ThesisId = id;
            publicationPartOfTesis.PublicationId = DOI;
            var publicationPartOfTesis2 = new PublicationPartOfTesis();
            publicationPartOfTesis2.ThesisId = id;
            publicationPartOfTesis2.PublicationId = DOI;
            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var publicService = new PublicationService(mockPublicationRepository.Object);
            //act
            await publicService.AddPublicationPartOfTesisAsync(publicationPartOfTesis);
            //assert
            publicationPartOfTesis.Should().Be(publicationPartOfTesis);
            await publicService.AddPublicationPartOfTesisAsync(publicationPartOfTesis2);

            publicationPartOfTesis2.Should().Be(publicationPartOfTesis2);
            mockPublicationRepository.Verify(repo => repo.SavePublicationPartOfTesisAsync(publicationPartOfTesis), Times.Once);
            mockPublicationRepository.Verify(repo => repo.SavePublicationPartOfTesisAsync(publicationPartOfTesis2), Times.Once);
        }
        [Fact]
        public async Task GetPublicationByThreeFiltersTest()
        {
            //arrange
            var publication = new Publication("Xylophone", "summary prueba", DateTime.Now,
                "Jornal", "name Journal", DOI, 5, null, null, null);
            var publication2 = new Publication("Prueba2", "testing xylophone", DateTime.Now,
               "Jornal", "name Journal", DOI, 5, null, null, null);
            var publication3 = new Publication("Prueba3", "summary prueba", DateTime.Now,
               "Jornal", "name Journal", DOI, 5, null, null, null);
            var publication4 = new Publication("Prueba4", "summary prueba", DateTime.Now,
               "Jornal", "name Journal", "xylophone", 5, null, null, null);

            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var publicService = new PublicationService(mockPublicationRepository.Object);

            await publicService.AddPublicationAsync(publication);
            await publicService.AddPublicationAsync(publication2);
            await publicService.AddPublicationAsync(publication3);
            await publicService.AddPublicationAsync(publication4);

            //act
            var results = await publicService.GetPublicationByThreeFilters("xylophone", 5, 1, 4);

            //assert
            mockPublicationRepository.Verify(repo => repo.GetPublicationByThreeFilters("xylophone", 5, 1, 4), Times.Once);
            results.Count().Should().Equals(3);
        }

        [Fact]
        public async Task GetPublicationCountByThreeFiltersTest()
        {
            //arrange
            var publication = new Publication("Xylophone", "summary prueba", DateTime.Now,
                "Jornal", "name Journal", DOI, 5, null, null, null);
            var publication2 = new Publication("Prueba2", "testing xylophone", DateTime.Now,
               "Jornal", "name Journal", DOI, 5, null, null, null);
            var publication3 = new Publication("Prueba3", "summary prueba", DateTime.Now,
               "Jornal", "name Journal", DOI, 5, null, null, null);
            var publication4 = new Publication("Prueba4", "summary prueba", DateTime.Now,
               "Jornal", "name Journal", "xylophone", 5, null, null, null);

            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var publicService = new PublicationService(mockPublicationRepository.Object);

            await publicService.AddPublicationAsync(publication);
            await publicService.AddPublicationAsync(publication2);
            await publicService.AddPublicationAsync(publication3);
            await publicService.AddPublicationAsync(publication4);
            
            //act
            var count = await publicService.GetPublicationCountByThreeFilters("xylophone", 5, 1, 4);

            //assert
            mockPublicationRepository.Verify(repo => repo.GetPublicationCountByThreeFilters("xylophone", 5, 1, 4), Times.Once);
            count.Should().Equals(3);
        }
        [Fact]
        public async Task addReferenceTest() {

            var publication = new Publication("PublicationReference Test", "testeo of reference", DateTime.Now,
               "Journal", "JournalReference", "1998", 1, null, null, null);

            string reference = "Esto es una referencia";


            List<ReferenceListPublication> referencesList = new List<ReferenceListPublication>
            {

                new ReferenceListPublication{ IdPublication="1998", Reference = reference, Order = 1}

            };

            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var publicationService = new PublicationService(mockPublicationRepository.Object);

            await publicationService.AddPublicationAsync(publication);

            mockPublicationRepository.Setup(repo => repo.addReferenceToPublication("1998", reference, 1));
            mockPublicationRepository.Setup(repo => repo.GetReferencesById("1998")).ReturnsAsync(referencesList);

            //act
            await publicationService.addReference("1998", reference, 1);

            //assert

            var result = await publicationService.GetReferencesById("1998");

            result.First().Reference.Should().Be(reference);

        }

        [Fact]
        public async Task updateReferenceTest()
        {
            var publication = new Publication("PublicationReference Test", "testeo of reference", DateTime.Now,
              "Journal", "JournalReference", "1998", 1, null, null, null);

            string reference = "Esto es una referencia";
            string reference2 = "Esto es una referencia modificada";


            List<ReferenceListPublication> referencesList = new List<ReferenceListPublication>
            {

                new ReferenceListPublication{ IdPublication="1998", Reference = reference2, Order = 1}

            };

            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var publicationService = new PublicationService(mockPublicationRepository.Object);

            await publicationService.AddPublicationAsync(publication);

            mockPublicationRepository.Setup(repo => repo.addReferenceToPublication("1998", reference, 1));
            mockPublicationRepository.Setup(repo => repo.updateReferenceToPublication("1998", reference2, 1));
            mockPublicationRepository.Setup(repo => repo.GetReferencesById("1998")).ReturnsAsync(referencesList);

            //act
            await publicationService.addReference("1998", reference, 1);
            await publicationService.UpdateReference("1998", reference2, 1);


            //assert

            var result = await publicationService.GetReferencesById("1998");

            result.First().Reference.Should().Be(reference2);

        }




        //ST-PH-4.24 
        //Technical tasks: Generar pruebas automatizadas para esta historia.
        //Contributors: David Sánchez & Christian Rojas.
        [Fact]
        public async Task VerifyDOITest()
        {
            //arrange
            var samplePublication = new Publication("Sample name", "sample summary", DateTime.Now,
                "Conference", "sample conference", "512//:4096", 17, "img/DefaultImage.png", null, null);

            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var publicService = new PublicationService(mockPublicationRepository.Object);
            mockPublicationRepository.Setup
                (repo => repo.VerifyDOI("512//:4096")).ReturnsAsync(true);

            await publicService.AddPublicationAsync(samplePublication);

            //act
            var publicationTest = await publicService.VerifyDOI("512//:4096");

            //assert
            publicationTest.Should().Be(true);

            mockPublicationRepository.Verify(repo => repo.VerifyDOI("512//:4096"), Times.Once);
        }

    }
}
