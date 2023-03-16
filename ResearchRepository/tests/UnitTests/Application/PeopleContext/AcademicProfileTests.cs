using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.People.Repositories;
using ResearchRepository.Application.People.Implementations;
using Moq;
using Xunit;
using FluentAssertions;
using ResearchRepository.Domain.Authentication.ValueObjects;

namespace UnitTests.Application.PeopleContext
{
    public class AcademicProfileTests
    {
        private static string unit = "CITIC";
        private static string _university = "UCR";
        private static string email = "andrea.alvaradoacon@ucr.ac.cr";
        private static string description = "Description";
        private static string academicDegree = "degree";
        private static string title = "title";
        private static string facebookLink = "facebook.com";
        private static string githubLink = "github.com";
        private static string linkedInLink = "linkedIn.com";
        private static string tel = "88888888";
        private static string pic = "/userPic.png";


        private static IList<PersonWorksForUnit> CreatePersonWorksForUnitList()
        {
            var list = new List<PersonWorksForUnit>();
            for (int i = 1; i < 3; i++)
            {
                var entity = new PersonWorksForUnit();
                entity.Email = email;
                entity.UnitName = unit;
                list.Add(entity);
            }
            return list;
        }

        private static IList<PersonsBelongsToUniversity> CreatePersonsBelongsToUniversityList()
        {
            var list = new List<PersonsBelongsToUniversity>();
            for (int i = 1; i < 3; i++)
            {
                var entity = new PersonsBelongsToUniversity();
                entity.PersonEmail = email;
                entity.UniversityName = _university;
                list.Add(entity);
            }
            return list;
        }

        private AcademicProfile CreateProfile()
        {
            return new AcademicProfile(email, description, pic, academicDegree, facebookLink, githubLink, linkedInLink, title, tel);
        }

        private static IList<AcademicProfile> CreateAcademicProfileList()
        {
            var list = new List<AcademicProfile>();
            for (int i = 1; i < 3; i++)
            {
                var entity = new AcademicProfile();
                entity.Email = email;
                entity.Biography = description;
                entity.Degree = academicDegree;
                entity.Title = title;
                entity.FacebookLink = facebookLink;
                entity.GitHubLink = githubLink;
                entity.LinkedInLink = linkedInLink;
                entity.Tel = tel;
                list.Add(entity);
            }
            return list;
        }


        [Fact]
        public async Task GetAsyncTest() {
            //Arrange
            var list = CreateAcademicProfileList();
            var mock = new Mock<IAcademicProfileRepository>();
            var academicProfileService = new AcademicProfileService(mock.Object);
            mock.Setup(r => r.GetAsync()).ReturnsAsync(list);

            //Act
            var academicProfiles = await academicProfileService.GetAsync();

            // assert
            academicProfiles.Should().BeEquivalentTo(list);
        }

        [Fact]
        public async Task SearchPersonByEmailTest() {
            //Arrange
            AcademicProfile academicProfile = new AcademicProfile(email,description,null,academicDegree,facebookLink,githubLink,linkedInLink,title,tel);
            var mock = new Mock<IAcademicProfileRepository>();
            var academicProfileService = new AcademicProfileService(mock.Object);
            mock.Setup(r => r.SearchPersonByEmail(email)).ReturnsAsync(academicProfile);

            //Act
            var profile = await academicProfileService.SearchPersonByEmail(email);

            //assert
            profile.Should().BeEquivalentTo(academicProfile);
        }

        [Fact]
        public async Task GetPersonBelongsToAcademicUnitTest() {
            //Arrange
            var list = CreatePersonWorksForUnitList();
            var mock = new Mock<IAcademicProfileRepository>();
            var academicProfileService = new AcademicProfileService(mock.Object);
            mock.Setup(r => r.GetPersonWorksForUnitByEmail(email)).ReturnsAsync(list);

            //Act
            var personWorksForUnitList = await academicProfileService.GetPersonWorksForUnitByEmail(email);

            // assert
            personWorksForUnitList.Should().BeEquivalentTo(list);

        }



        [Fact]
        public async Task GetPersonBelongsToUniversityByEmailTest()
        {
            //Arrange
            var list = CreatePersonsBelongsToUniversityList();
            var mock = new Mock<IAcademicProfileRepository>();
            var academicProfileService = new AcademicProfileService(mock.Object);
            mock.Setup(r => r.GetPersonBelongsToUniversityByEmail(email)).ReturnsAsync(list);

            //Act
            var personWorksForUniversity = await academicProfileService.GetPersonBelongsToUniversityByEmail(email);

            // assert
            personWorksForUniversity.Should().BeEquivalentTo(list);

        }


        [Fact]
        public async void registerUser()
        {
            var mock = new Mock<IAcademicProfileRepository>();
            var academicService = new AcademicProfileService(mock.Object);
            Register newUser = new Register(email, "password");
            mock.Setup(r => r.registerUser(newUser)).Verifiable();
            //act
            await academicService.registerUser(newUser);
            //assert
            mock.Verify();

        }

        
        [Fact]
        public async void updateProfileData()
        {
            var mock = new Mock<IAcademicProfileRepository>();
            var academicService = new AcademicProfileService(mock.Object);
            AcademicProfile profile = CreateProfile();
            AcademicProfile p2 = CreateProfile(); 
            mock.Setup(r => r.updateProfileData(email, "user", "lastname", "lastname2", "biography2", academicDegree, title, facebookLink, githubLink, linkedInLink, tel, pic)).Verifiable();
            //act
            await academicService.updateProfileData(email, "user", "lastname", "lastname2", "biography2", academicDegree, title, facebookLink, githubLink, linkedInLink, tel, pic);
            //assert
            mock.Verify();
        }
       
              
        [Fact]
        public void  DeletePersonWorksForUnit()
        {
            var mock = new Mock<IAcademicProfileRepository>();
            var academicService = new AcademicProfileService(mock.Object);
            PersonWorksForUnit person = new PersonWorksForUnit(email);
            mock.Setup(r => r.DeletePersonWorksForUnit(person)).Verifiable();
            //act
            academicService.DeletePersonWorksForUnit(person);
            //assert
            mock.Verify();
        }
        
        [Fact]
        public async void AddPersonBelongsToAcademicUnit()
        {
            var mock = new Mock<IAcademicProfileRepository>();
            var academicService = new AcademicProfileService(mock.Object);
            mock.Setup(r => r.AddPersonBelongsToAcademicUnit("Default",email)).Verifiable();
            //act
            await academicService.AddPersonBelongsToAcademicUnit("Default",email);
            //assert
            mock.Verify();
            
        }

        [Fact]
        public void DeletePersonBelongsToUniversity()
        {
            var mock = new Mock<IAcademicProfileRepository>();
            var academicService = new AcademicProfileService(mock.Object);
            PersonsBelongsToUniversity p = new PersonsBelongsToUniversity();
            mock.Setup(r => r.DeletePersonBelongsToUniversity(p)).Verifiable();
            //act
            academicService.DeletePersonBelongsToUniversity(p);
            //assert
            mock.Verify();
        }
        [Fact]
        public async Task AddPersonBelongsToUniversity()
        {
            var mock = new Mock<IAcademicProfileRepository>();
            var academicService = new AcademicProfileService(mock.Object);
            mock.Setup(r => r.AddPersonBelongsToUniversity("Default", email)).Verifiable();
            //act
            await academicService.AddPersonBelongsToUniversity("Default", email);
            //assert
            mock.Verify();
        }
    }
}
