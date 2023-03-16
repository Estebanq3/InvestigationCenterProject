using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using ResearchRepository.Domain.PublicationContext;


namespace UnitTests.Domain.PublicationContext
{
    /// <summary>
    /// In this test we focus on the Id of the Publication for the relation between Investigation Projects and Publications. 
    /// </summary>
    /// Author: Diana Luna
    public class ProjectAsociatedToPublicationTests
    {
        private static readonly string PublicationId = "";
        private static readonly int InvestigationProjectId = -1;

        [Fact]
        public void PublicationIdEmptyReturnEmpty() {
            //arrange
            var input = string.Empty;

            //act
            var result = new ProjectAsociatedToPublication();
            result.PublicationId = input;

            //assert
            result.PublicationId.Should().Be(string.Empty);
        }
    }
}
