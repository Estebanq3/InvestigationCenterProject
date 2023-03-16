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
using ResearchRepository.Domain.PublicationContext;
using ResearchRepository.Domain.PublicationContext.Repositories;
using FluentAssertions;
using WebApplication_ResearchRepository;
using IntegrationTests.Infrastructure.PublicationContext;
using ResearchRepository.Domain.PublicationContext.Entities;

namespace IntegrationTests.PublicationContext.Infrastructure.Repositories
{
    public class PublicationRepositoryIntegrationTestClass :
    IClassFixture<PublicationFactory<Startup>>
    {
        private readonly PublicationFactory<Startup> _factory;
        public
        PublicationRepositoryIntegrationTestClass(PublicationFactory<Startup>
        factory)
        {
            _factory = factory;
        }

     
        [Fact]
        public async Task GetPublicationPagedAsyncTest()
        {
            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IPublicationRepository>();

            //act
            var result = await repository.GetPublicationPagedAsync(7, 3, 2);

            //assert
            result.Count().Should().Equals(1);
        }

        /*[Fact]
        public async Task GetPublicationCountAsyncTest()
        {
            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IPublicationRepository>();

            //act
            var count = await repository.GetPublicationCountAsync(7);

            //assert
            count.Should().Be(5);
        }*/

        [Fact]
        public async Task getPublicationByGroupTest()
        {
            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IPublicationRepository>();

            //act
            var result = await repository.getPublicationByGroup(7);

            //assert
            result.Count().Equals(5);
        }

        [Fact]
        public async Task GetReferencesByIdTest()
        {
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IPublicationRepository>();

            //act
            var listReferences = await repository.GetReferencesById("11.11.11/22.22.22");

            //assert
            listReferences.Count().Should().Be(4);
        }


        [Fact]
        public async Task GetPublicationByTermPagedAsyncTest()
        {

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IPublicationRepository>();

            string term = "test";

            //act
            var count = await repository.GetPublicationByTermPagedAsync(1, 1, 2, term);

            //assert
            count.Count().Equals(3);

        }

        [Fact]
        public async Task GetPublicationByTermCountAsyncTest() {

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IPublicationRepository>();

            string term = "prueba";

            //act
            var count = await repository.GetPublicationByTermCountAsync(1,term);

            //assert
            count.Should().Be(3);

        }

        [Fact]
        public async Task IdEqualTest()
        {

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IPublicationRepository>();

            string term = "10.1112/CLEI52000.2020.00070";

            //act
            var publication = await repository.IdEqual(term);

            //assert
            publication.Id.Should().Be(term);

        }

        [Fact]
        public async Task GetPublicationByAuthorTest()
        {

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IPublicationRepository>();

            List<string> dois = new List<string>(new string[] { "10.1112/CLEI52000.2020.00070", "10.1111/CLEI52000.2020.00069" });
            

            //act
            var publications = await repository.GetPublicationByAuthor(dois,1);

            //assert
            publications.Count().Should().Equals(2);
        }

        /// <summary>
        /// IntegrationTest for get PublicationPartOfTesis by id publication
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-4.4
        [Fact]
        public async Task GetPublicationPartOfTesisdReturnPublicationPartOfTesis()
        {
            const int teamCount = 4;
            // arrange
            var repository =
            _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            // act
            var teams = await repository.GetPublicationPartOfTesisAsync("10.1109/CLEI52000.2020.00067");
            // assert
            teams.Should().HaveCount(teamCount);
        }
        /// <summary>
        /// IntegrationTest for get ProjectsAsociated by id publication
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-4.5
        [Fact]
        public async Task GetProjectsAsociatedReturnProjectsAsociated()
        {
            const int teamCount = 4;
            // arrange
            var repository =
            _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            // act
            var teams = await repository.GetProjectsAsociatedAsync("10.1109/CLEI52000.2020.00067");
            // assert
            teams.Should().HaveCount(teamCount);
        }
        /// <summary>
        /// IntegrationTest for add publication
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-4.4
        [Fact]
        public async Task SaveAsyncPublication()
        {
            // arrange
            int resultPre = 0;
            int resultPos = 0;
            var repository =
            _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            var publication = new Publication("test", "summary test", DateTime.Now, "Journal", "journal test", "DOI test", 1, null, null, null);
            resultPre = await repository.GetPublicationCountAsync(1);
            // act
            await repository.SaveAsync(publication);

            resultPos = await repository.GetPublicationCountAsync(1);

            if (resultPre < resultPos)
            {
                resultPos = resultPos - 1;
            }
            // assert
            resultPre.Should().Be(resultPos);

        }
        /// <summary>
        /// IntegrationTest for add tesis asociate to publication
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-4.5
        [Fact]
        public async Task AddPublicationPartOfTesisAsync()
        {
            // arrange
            var resultPre = 0;
            var resultPos = 0;
            var repository =
            _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            var PublicationPartOfTesis = new PublicationPartOfTesis();
            PublicationPartOfTesis.PublicationId = "10.1112/CLEI52000.2020.00070";
            PublicationPartOfTesis.ThesisId = 4;
            var resultPre2 = await repository.GetPublicationsAsociatedToThesisAsync(4);
            // act
            await repository.SavePublicationPartOfTesisAsync(PublicationPartOfTesis);

            var resultPos2 = await repository.GetPublicationsAsociatedToThesisAsync(4);
            resultPre = resultPre2.Count();
            resultPos = resultPos2.Count() - 1;
            // assert
            resultPre.Should().Be(resultPos);

        }
        /// <summary>
        /// IntegrationTest for undate publication 
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-4.6
        [Fact]
        public async Task undatePublication()
        {
            // arrange
            string chance = "summary test undate";
            var repository =
            _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            var publication = new Publication("prueba", chance, DateTime.Now, "Journal", "journal test", "10.1109/CLEI52000.2020.00068", 1, null, null, null);
            // act
            await repository.UndatePublicationAsync(publication);
            var resultPos = await repository.IdEqual("10.1109/CLEI52000.2020.00068");

            // assert
            resultPos.Summary.Should().Be(chance);

        }
        //Revisar resultado
        [Fact]
        public async Task SaveProjectAsociatedAsyncShouldPersistNewAsociated()
        {
            // arrange
            var resultPre = 0;
            var resultPos = 0;
            var repository = _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            var project = new ProjectAsociatedToPublication();
            project.PublicationId = "10.1112/CLEI52000.2020.00070";
            project.InvestigationProjectId = 4;
            var resultPre2 = await repository.GetPublicationsAsociatedToProjectAsync(4);
            // act
            await repository.SaveProjectAsociatedAsync(project);
            var resultPos2 = await repository.GetPublicationsAsociatedToProjectAsync(4);
            resultPre = resultPre2.Count();
            resultPos = resultPos2.Count() - 1;
            // assert
            resultPre.Should().Be(resultPos);
        }

        [Fact]
        public async Task GetPublicationsFromIdShouldHavePublications()
        {
            //arrange
            IList<string> ids = new List<string>(new string[] { "10.1109/CLEI52000.2020.00068", "10.1109/CLEI52000.2020.00067", "10.1110/CLEI52000.2020.00068" });
            var repository = _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            //act
            var count = await repository.GetPublicationsFromId(ids);
            //assert
            count.Count().Should().Equals(3);
        }

        [Fact]
        public async Task GetPublicationsByTermPagedSummaryFindCorrectTerm()
        {
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            string term = "prueba";
            //act
            var count = await repository.GetPublicationByTermPagedSummary(1, 1, 10, term);
            //assert
            count.Count().Equals(3);
        }

        //ST-PH-3.8 Muestra de las referencias asociadas a una publicación.
        //Technical tasks: Generar pruebas automatizadas para esta historia.
        //Contributors: Christian Rojas & David Sánchez.
        [Fact]
        public async Task GetPublicationByTermCountAsyncSummaryCorrectCount()
        {
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            string term = "prueba";
            //act
            var count = await repository.GetPublicationByTermCountAsync(1, term);
            //assert
            count.Should().Be(3);
        }

        /// <summary>
        /// IntegrationTest for DeletePublicationPartOfTesisAsync
        /// </summary>
        /// Author: Elvis Badilla & Diana Luna
        /// StoryID: ST-PH-4.11
        [Fact]
        public async Task DeletePublicationPartOfTesisAsync()
        {
            // arrange
            var resultPre = 0;
            var resultPos = 0;
            var repository =
            _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            var PublicationPartOfTesis = new PublicationPartOfTesis();
            PublicationPartOfTesis.PublicationId = "10.1109/CLEI52000.2020.00068";
            PublicationPartOfTesis.ThesisId = 4;
            var resultPre2 = await repository.GetPublicationsAsociatedToThesisAsync(4);
            // act
            await repository.DeletePublicationPartOfThesis(PublicationPartOfTesis.PublicationId);

            var resultPos2 = await repository.GetPublicationsAsociatedToThesisAsync(4);
            resultPre = resultPre2.Count()-1;
            resultPos = resultPos2.Count();
            // assert
            resultPre.Should().Be(resultPos);

        }

        /// <summary>
        /// IntegrationTest for DeletePublicationPartOfProjectAsync
        /// </summary>
        /// Author: Elvis Badilla & Diana Luna
        /// StoryID: ST-PH-4.12
        [Fact]
        public async Task DeletePublicationPartOfProjectAsync()
        {
            // arrange
            var resultPre = 0;
            var resultPos = 0;
            var repository = _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            var projectAsociatedToPublication = new ProjectAsociatedToPublication();
            projectAsociatedToPublication.PublicationId = "10.1109/CLEI52000.2020.00068";
            projectAsociatedToPublication.InvestigationProjectId = 3;
            var resultPre2 = await repository.GetPublicationsAsociatedToProjectAsync(3);
            // act
            await repository.DeletePublicationPartOfProject(projectAsociatedToPublication.PublicationId);

            var resultPos2 = await repository.GetPublicationsAsociatedToProjectAsync(3);
            resultPre = resultPre2.Count() - 1;
            resultPos = resultPos2.Count();
            // assert
            resultPre.Should().Be(resultPos);

        }
        /// <summary>
        /// IntegrationTest for add publication with identical id
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-4.1
        [Fact]
        public async Task SaveAsyncPublicationByIdExists()
        {
            // arrange
            int resultPre = 0;
            int resultPos = 0;
            var repository =
            _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            var publication = new Publication("prueba", "summary test", DateTime.Now, "Journal", "journal test", "10.1109/CLEI52000.2020.00068", 1, null, null, null);
            resultPre = await repository.GetPublicationCountAsync(1);
            // act
            await repository.SaveAsync(publication);

            resultPos = await repository.GetPublicationCountAsync(1);
            // assert
            resultPre.Should().Be(resultPos);

        }
        /// <summary>
        /// IntegrationTest for add tesis asociate to publication with identical id
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-4.5
        [Fact]
        public async Task AddPublicationPartOfTesisByIdExists()
        {
            // arrange
            var resultPre = 0;
            var resultPos = 0;
            var repository =
            _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            var PublicationPartOfTesis = new PublicationPartOfTesis();
            PublicationPartOfTesis.PublicationId = "10.1109/CLEI52000.2020.00068";
            PublicationPartOfTesis.ThesisId = 1;
            var resultPre2 = await repository.GetPublicationsAsociatedToThesisAsync(1);
            // act
            await repository.SavePublicationPartOfTesisAsync(PublicationPartOfTesis);

            var resultPos2 = await repository.GetPublicationsAsociatedToThesisAsync(1);
            resultPre = resultPre2.Count();
            resultPos = resultPos2.Count();
            // assert
            resultPre.Should().Be(resultPos);

        }

        /// <summary>
        /// IntegrationTest for get PublicationPartOfTesis by id publication That Does Not There
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-4.5
        [Fact]
        public async Task GetPublicationPartOfTesisdThatDoesNotThere()
        {
            const int teamCount = 0;
            // arrange
            var repository =
            _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            // act
            var teams = await repository.GetPublicationPartOfTesisAsync("no exist");
            // assert
            teams.Should().HaveCount(teamCount);
        }
        /// <summary>
        /// IntegrationTest for get ProjectsAsociated by id publication That Does Not There
        /// </summary>
        /// Author: Elvis Badilla
        /// StoryID: ST-PH-4.6
        [Fact]
        public async Task GetProjectsAsociatedThatDoesNotThere()
        {
            const int teamCount = 0;
            // arrange
            var repository =
            _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            // act
            var teams = await repository.GetProjectsAsociatedAsync("no exist");
            // assert
            teams.Should().HaveCount(teamCount);
        }
        /// <summary>
        /// IntegrationTest for DeletePublicationPartOfTesisAsync Does Not There
        /// </summary>
        /// Author: Elvis Badilla & Diana Luna
        /// StoryID: ST-PH-4.11
        [Fact]
        public async Task DeletePublicationPartOfTesisDoesNotThere()
        {
            // arrange
            var resultPre = 0;
            var resultPos = 0;
            var repository =
            _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            var PublicationPartOfTesis = new PublicationPartOfTesis();
            PublicationPartOfTesis.PublicationId = "no existe";
            PublicationPartOfTesis.ThesisId = 4;
            var resultPre2 = await repository.GetPublicationsAsociatedToThesisAsync(4);
            // act
            await repository.DeletePublicationPartOfThesis(PublicationPartOfTesis.PublicationId);

            var resultPos2 = await repository.GetPublicationsAsociatedToThesisAsync(4);
            resultPre = resultPre2.Count();
            resultPos = resultPos2.Count();
            // assert
            resultPre.Should().Be(resultPos);

        }

        /// <summary>
        /// IntegrationTest for DeletePublicationPartOfProjectAsync Does Not There
        /// </summary>
        /// Author: Elvis Badilla & Diana Luna
        /// StoryID: ST-PH-4.12
        [Fact]
        public async Task DeletePublicationPartOfProjectDoesNotThere()
        {
            // arrange
            var resultPre = 0;
            var resultPos = 0;
            var repository = _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            var projectAsociatedToPublication = new ProjectAsociatedToPublication();
            projectAsociatedToPublication.PublicationId = "no existe";
            projectAsociatedToPublication.InvestigationProjectId = 3;
            var resultPre2 = await repository.GetPublicationsAsociatedToProjectAsync(3);
            // act
            await repository.DeletePublicationPartOfProject(projectAsociatedToPublication.PublicationId);

            var resultPos2 = await repository.GetPublicationsAsociatedToProjectAsync(3);
            resultPre = resultPre2.Count();
            resultPos = resultPos2.Count();
            // assert
            resultPre.Should().Be(resultPos);

        }

        [Fact]
        public async Task GetPublicationByThreeFilters()
        {
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IPublicationRepository>();

            //act
            var results = await repository.GetPublicationByThreeFilters("xYloPhoNe", 512, 1, 4);

            //assert
            results.Count().Should().Be(3);
        }

        [Fact]
        public async Task GetPublicationCountByThreeFilters()
        {
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IPublicationRepository>();

            //act
            var count = await repository.GetPublicationCountByThreeFilters("xYloPhoNe", 512, 1, 4);
            
            //assert
            count.Should().Be(3);
        }

        [Fact]
        public async Task VerifyDOITest()
        {
            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            string someDoi = "512//:4096";

            //act
            var publication = await repository.VerifyDOI(someDoi);

            //assert
            publication.Should().Be(false);
        }

        [Fact]
        public async Task addReferenceToPublicationTest()
        {
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            string referenceText = "Esto es una referencia de prueba";
            var reference = new ReferenceListPublication("10.1112/CLEI52000.2020.00070", 1, referenceText);

            //act
            await repository.addReferenceToPublication("10.1112/CLEI52000.2020.00070", referenceText, 1);
            //assert
            var refList = await repository.GetReferencesById("10.1112/CLEI52000.2020.00070");
            refList.First().Reference.Should().Be(referenceText);
        }

        [Fact]
        public async Task updateReferenceToPublicationTest()
        {
            //arrange
            var repository = _factory.Server.Services.GetRequiredService<IPublicationRepository>();
            string referenceTextUpdated = "Esto es una referencia de prueba actualizada";
            var reference = new ReferenceListPublication("10.1112/CLEI52000.2020.00070", 1, referenceTextUpdated);

            //act
            await repository.updateReferenceToPublication("10.1112/CLEI52000.2020.00070", referenceTextUpdated, 1);

            //assert
            var refList = await repository.GetReferencesById("10.1112/CLEI52000.2020.00070");
            refList.First().Reference.Should().Be(referenceTextUpdated);
        }
    }
}