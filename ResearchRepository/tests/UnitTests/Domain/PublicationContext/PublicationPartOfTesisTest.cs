using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ResearchRepository.Domain.PublicationContext;
using FluentAssertions;

namespace UnitTests.Domain.PublicationContext
{
    public class PublicationPartOfTesisTest
    {

        [Fact]
        public void PublicationIdEmptyReturnEmpty()
        {
            //arrange
            var input = string.Empty;

            //act
            var result = new PublicationPartOfTesis();

            result.PublicationId = input;
            //assert
            result.PublicationId.Should().Be(string.Empty);
        }

        [Fact]
        public void thesisIDAsigne()
        {
            //arrange
            var input = 2;

            //act
            var result = new PublicationPartOfTesis();

            result.ThesisId = input;
            //assert
            result.ThesisId.Should().Be(input);
        }
        [Fact]
        public void publicationIDAsigne()
        {
            //arrange
            var input = "Prueba";

            //act
            var result = new PublicationPartOfTesis();

            result.PublicationId = input;
            //assert
            result.PublicationId.Should().Be(input);
        }
    }
}
