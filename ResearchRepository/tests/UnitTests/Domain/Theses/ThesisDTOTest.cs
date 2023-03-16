using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ResearchRepository.Domain.Theses.DTOs;
using FluentAssertions;

namespace UnitTests.Domain.Theses
{
    public class ThesisDTOTest
    {
        private static long Id = 1;
        private static String Name = "Test";
        private static DateTime PublicationDate = DateTime.Now;
        private static String Summary = "This is a summary of a test";
        private static long InvestigationGroupId = 1;
        private static String DOI = "123";
        private static String Image = "picture-default.png";
        private static String Type = "Licenciatura";
        private static String Reference = "This is a reference";

        /// <summary>
        /// This method return an thesis with empty Name 
        /// </summary>
        /// Author: Oscar Navarro- Sebastian Gonzalez
        [Fact]

        public void EmptyNameReturnEmpty()
        {
            var input = String.Empty;

            var result = new ThesisDTO(Id, input, PublicationDate, Summary, InvestigationGroupId, DOI, Image, Type, Reference);

            result.Name.Should().Be(String.Empty);
        }
        /// <summary>
        /// This method return an thesis with null Name 
        /// </summary>
        /// Author: Oscar Navarro- Sebastian Gonzalez
        [Fact]
        public void NullNameReturnNull()
        {
            String? input = null;

            var result = new ThesisDTO(Id, input, PublicationDate, Summary, InvestigationGroupId, DOI, Image, Type, Reference);

            result.Name.Should().Be(null);
        }
        /// <summary>
        /// This method return an thesis with empty Summary 
        /// </summary>
        /// Author: Oscar Navarro- Sebastian Gonzalez
        [Fact]
        public void EmptySummaryReturnEmpty()
        {
            var input = String.Empty;

            var result = new ThesisDTO(Id, input, PublicationDate, input, InvestigationGroupId, DOI, Image, Type, Reference);

            result.Summary.Should().Be(String.Empty);
        }
        /// <summary>
        /// This method return an thesis with null Summary 
        /// </summary>
        /// Author: Oscar Navarro- Sebastian Gonzalez
        [Fact]
        public void NullSummaryReturnNull()
        {
            String? input = null;

            var result = new ThesisDTO(Id, input, PublicationDate, input, InvestigationGroupId, DOI, Image, Type, Reference);

            result.Summary.Should().Be(null);
        }
        /// <summary>
        /// This method return an thesis with empty DOI 
        /// </summary>
        /// Author: Sebastian Gonzalez-Oscar Navarro
        [Fact]
        public void EmptyDOIReturnEmpty()
        {
            var input = String.Empty;

            var result = new ThesisDTO(Id, Name, PublicationDate, Summary, InvestigationGroupId, input, Image, Type, Reference);

            result.DOI.Should().Be(String.Empty);
        }
        /// <summary>
        /// This method return an thesis with null DOI 
        /// </summary>
        /// Author: Sebastian Gonzalez-Oscar Navarro
        [Fact]
        public void NullDOIReturnNull()
        {
            String? input = null;

            var result = new ThesisDTO(Id, Name, PublicationDate, Summary, InvestigationGroupId, input, Image, Type, Reference);

            result.DOI.Should().Be(null);
        }
        /// <summary>
        /// This method return an thesis with empty Reference 
        /// </summary>
        /// Author: Sebastian Gonzalez-Oscar Navarro
        [Fact]
        public void EmptyReferenceReturnEmpty()
        {
            var input = String.Empty;

            var result = new ThesisDTO(Id, Name, PublicationDate, Summary, InvestigationGroupId, DOI, Image, Type, input);

            result.Reference.Should().Be(String.Empty);
        }
        /// <summary>
        /// This method return an thesis with null Reference
        /// </summary>
        /// Author: Sebastian Gonzalez-Oscar Navarro
        [Fact]
        public void NullReferenceReturnNull()
        {
            String? input = null;

            var result = new ThesisDTO(Id, Name, PublicationDate, Summary, InvestigationGroupId, DOI, Image, Type, input);

            result.Reference.Should().Be(null);
        }

        /// <summary>
        /// This method return an thesis with empty image
        /// </summary>
        /// Author: Steven Nuñez
        /// Collaborator: Gabriel Revillat
        [Fact]
        public void EmptyImageReturnEmpty()
        {
            var input = String.Empty;

            var result = new ThesisDTO(Id, Name, PublicationDate, Summary, InvestigationGroupId, DOI, input, Type, Reference);

            result.Image.Should().Be(String.Empty);
        }
        /// <summary>
        /// This method return an thesis with null image
        /// </summary>
        /// Author: Steven Nuñez
        /// Collaborator: Gabriel Revillat
        [Fact]
        public void NullImageReturnNull()
        {
            String? input = null;

            var result = new ThesisDTO(Id, Name, PublicationDate, Summary, InvestigationGroupId, DOI, input, Type, Reference);

            result.Image.Should().Be(null);
        }


    }
}
