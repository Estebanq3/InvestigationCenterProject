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
using ResearchRepository.Domain.People;
using ResearchRepository.Domain.People.Repositories;
using FluentAssertions;
using WebApplication_ResearchRepository;
using IntegrationTests.Infrastructure.People;

namespace IntegrationTests.Infrastructure.People.Repositories
{
    public class AcademicProfileRepositoryIntegrationTestClass :
    IClassFixture<AcademicFactory<Startup>>
    {
        private readonly AcademicFactory<Startup> _factory;
        public
        AcademicProfileRepositoryIntegrationTestClass(AcademicFactory<Startup>
        factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task GetAllAcademicProfiles()
        {
            int total = 3;

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IAcademicProfileRepository>();

            //act
            var count = await repository.GetAsync();

            //assert
            count.Count().Equals(total);

        }

        [Fact]
        public async Task GetPersonByEmail()
        {
            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IAcademicProfileRepository>();

            string email = "GREIVIN.SANCHEZGARITA@ucr.ac.cr";

            //act
            var profile = await repository.SearchPersonByEmail(email);

            //assert
            profile.Email.Should().Be(email);


        }


        [Fact]
        public async Task getTheUnitsAPersonWorksFor()
        {
            int total = 2;
            string email = "andrea.alvaradoacon@ucr.ac.cr";

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IAcademicProfileRepository>();

            //act
            var count = await repository.GetPersonWorksForUnitByEmail(email);

            //assert
            count.Count().Equals(total);

        }


        [Fact]
        public async Task getTheUniversitiesAPersonBelongsTo()
        {
            int total = 2;
            string email = "andrea.alvaradoacon@ucr.ac.cr";

            // arrange
            var repository = _factory.Server.Services.GetRequiredService<IAcademicProfileRepository>();

            //act
            var count = await repository.GetPersonBelongsToUniversityByEmail(email);

            //assert
            count.Count().Equals(total);

        }


    }
}
