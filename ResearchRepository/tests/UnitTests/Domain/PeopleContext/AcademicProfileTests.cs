using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ResearchRepository.Domain.People.Entities;
using FluentAssertions;
using ResearchRepository.Domain.People;

namespace UnitTests.Domain.PeopleContext
{
    public class AcademicProfileTests
    {
        //ID: ST-PA-3.15 
        //Tareas: PIIB22021-511 Crear entidades, mapeo y servicios
        //Participantes: Andrea Alvarado y Sebastián Montero

        private static string Email = "email@ucr.ac.cr";
        private static string Biography = "Biography";
        private static string pic = "./img/ProfilePictures/default.png";
        private static string degree = "Dr.";
        private static string facebook = "https://www.facebook.com/";
        private static string github = "https://github.com/";
        private static string linkedIn = "https://www.linkedin.com/";
        private static string title = "N/A";
        private static string tel = "88888888";

        [Fact]
        public void BiographyEmptyReturnEmpty()
        {
            //arrange
            var input = string.Empty;

            //act
            var result = new AcademicProfile(Email,input,pic,degree,facebook,github,linkedIn,title,tel);

            //assert
            result.Biography.Should().Be(string.Empty);
        }

        [Fact]
        public void picEmptyReturnEmpty()
        {
            //arrange
            var input = string.Empty;

            //act
            var result = new AcademicProfile(Email, Biography, input, degree, facebook, github, linkedIn, title, tel);

            //assert
            result.ProfilePic.Should().Be(string.Empty);
        }



        [Fact]
        public void NullDegreeReturnNull()
        {
            //arrange
            string? input = null;

            //act
            var result = new AcademicProfile(Email, Biography, pic, input, facebook, github, linkedIn, title, tel);

            //assert
            result.Degree.Should().Be(null);
        }

        [Fact]
        public void NullFacebookLinkReturnNull()
        {
            //arrange
            string? input = null;

            //act
            var result = new AcademicProfile(Email, Biography, pic, degree, input, github, linkedIn, title, tel);

            //assert
            result.FacebookLink.Should().Be(null);
        }

        [Fact]
        public void GithubLinkEmptyReturnEmpty()
        {
            //arrange
            var input = string.Empty;

            //act
            var result = new AcademicProfile(Email, Biography, pic, degree, facebook, input, linkedIn, title, tel);

            //assert
            result.GitHubLink.Should().Be(string.Empty);
        }


        [Fact]
        public void LinkedInEmptyReturnEmpty()
        {
            //arrange
            var input = string.Empty;

            //act
            var result = new AcademicProfile(Email, Biography, pic, degree, facebook, github, input, title, tel);

            //assert
            result.LinkedInLink.Should().Be(string.Empty);
        }

        [Fact]
        public void NullTitleReturnNull()
        {
            //arrange
            string? input = null;

            //act
            var result = new AcademicProfile(Email, Biography, pic, degree, facebook, github, linkedIn, input, tel);

            //assert
            result.Title.Should().Be(null);
        }

        [Fact]
        public void NullTelLinkReturnNull()
        {
            //arrange
            string? input = null;

            //act
            var result = new AcademicProfile(Email, Biography, pic, degree, facebook, github, linkedIn, title, input);

            //assert
            result.Tel.Should().Be(null);
        }


        [Fact]
        public void NewAcademicProfileWithoutPerson()
        {
            // act
            var profile = new AcademicProfile(Email, Biography, pic, degree, facebook, github, linkedIn, title, tel);

            // assert
            profile.Person.Should().Be(null);
        }

        [Fact]
        public void EmailStringReturnsSameString()
        {

            //act
            var result = new AcademicProfile(Email, Biography, pic, degree, facebook, github, linkedIn, title, tel);

            //assert
            result.Email.Should().Be(Email);
        }

    }
}
