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
    public class AcademicUnitTests
    {
        //ID: ST-PA-3.15 
        //Tareas: PIIB22021-511 Crear entidades, mapeo y servicios
        //Participantes: Andrea Alvarado y Sebastián Montero

        private static string name = "CITIC";

        [Fact]
        public void NameEmptyReturnEmpty()
        {
            //arrange
            var input = string.Empty;

            //act
            var result = new AcademicUnit(input);

            //assert
            result.Name.Should().Be(string.Empty);
        }

        [Fact]
        public void NullNameReturnNull()
        {
            //arrange
            string? input = null;

            //act
            var result = new AcademicUnit(input);

            //assert
            result.Name.Should().Be(null);
        }




    }
}
