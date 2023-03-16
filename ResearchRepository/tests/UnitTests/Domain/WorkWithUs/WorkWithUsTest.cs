using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ResearchRepository.Domain.WorkWithUs.Entities;
using FluentAssertions;
using ResearchRepository.Domain.WorkWithUs;
namespace UnitTests.Domain.WorkWithUs
{
    public class WorkWithUsTests
    {
        private static string Email = "email@ucr.ac.cr";

        private static string Name = "Nombre";

        private static string Description = "Description";

        private static string Prerequisites = "Prerequisites";

        private static string Pregrado = "Pregrado";

        private static string Posgrado = "Posgrado";

        private static string Voluntario = "Voluntario";

        private static string Image = "Image";

        [Fact]
        public void NameEmptyReturnEmpty()
        {
            //arrange
            var input = string.Empty;

            //act
            var result = new WorkInfo(input, Description, Image, Prerequisites, Pregrado, Posgrado, Voluntario, Email);

            //assert
            result.Name.Should().Be(string.Empty);
        }

       
        [Fact]
        public void NullImageReturnNull()
        {
            string? input = null;

            //act
            var result = new WorkInfo(Name, Description, input, Prerequisites, Pregrado, Posgrado, Voluntario, Email);

            //assert
            result.ImageName.Should().Be(null);
        }


        [Fact]
        public void EmailStringReturnsSameString()
        {

            //act
            var result = new WorkInfo(Name, Description, Image, Prerequisites, Pregrado, Posgrado, Voluntario, Email);

            //assert
            result.Email.Should().Be(Email);
        }




    }
}
