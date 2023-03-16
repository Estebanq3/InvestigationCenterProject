using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ResearchRepository.Domain.InvestigationProjects.DTOs;
using FluentAssertions;

namespace UnitTests.Domain.ResearchProjects.DTOs
{
    public class ProjectDTOTest
    {
        private static long Id = 1;
        private static String Name = "Test1";
        private static DateTime StartDate = DateTime.Now;
        private static DateTime EndDate = DateTime.Now;
        private static long InvestigationGroupID = 1;
        private static String Description = "This is a test"; 
        private static String Summary = "This is a summary of a test"; 
        private static String Image = "picture-default.png";

        [Fact]
        public void EmptyNameReturnEmpty() {
            var input = String.Empty;

            var result = new InvestigationProjectDTO(Id, input, StartDate, EndDate, InvestigationGroupID, Description, Summary, Image);

            result.Name.Should().Be(String.Empty);
        }

        /// <summary>
        /// This method tests if a null name returns null.
        /// </summary>
        /// Author: Sofia Campos (Pair programming during sprint 2)
        /// Collaborator: Gabriel Revillat, Steven Nuñez
        [Fact]
        public void NullNameReturnNull()
        {
            String? input = null;

            var result = new InvestigationProjectDTO(Id, input, StartDate, EndDate, InvestigationGroupID, Description, Summary, Image);

            result.Name.Should().Be(null);
        }

        [Fact]
        public void EmptyDescriptionReturnEmpty()
        {
            var input = String.Empty;

            var result = new InvestigationProjectDTO(Id, Name, StartDate, EndDate, InvestigationGroupID, input, Summary, Image);

            result.Description.Should().Be(String.Empty);
        }

        [Fact]
        public void NullDescriptionReturnNull()
        {
            String? input = null;

            var result = new InvestigationProjectDTO(Id, Name, StartDate, EndDate, InvestigationGroupID, input, Summary, Image);

            result.Description.Should().Be(null);
        }

        [Fact]
        public void EmptySummaryReturnEmpty()
        {
            var input = String.Empty;

            var result = new InvestigationProjectDTO(Id, Name, StartDate, EndDate, InvestigationGroupID, Description, input, Image);

            result.Summary.Should().Be(String.Empty);
        }

        /// <summary>
        /// This method tests if a null summary returns null.
        /// </summary>
        /// Author: Sofia Campos (Pair programming during sprint 2)
        /// Collaborator: Gabriel Revillat, Steven Nuñez
        [Fact]
        public void NullSummaryReturnNull()
        {
            String? input = null;

            var result = new InvestigationProjectDTO(Id, Name, StartDate, EndDate, InvestigationGroupID, Description, input, Image);

            result.Summary.Should().Be(null);
        }

        /// <summary>
        /// This method return an project with null image
        /// </summary>
        /// Author: Steven Nuñez
        /// Collaborator: Gabriel Revillat
        [Fact]
        public void EmptyImageReturnEmpty()
        {
            var input = String.Empty;

            var result = new InvestigationProjectDTO(Id, Name, StartDate, EndDate, InvestigationGroupID, Description, Summary, input);

            result.Image.Should().Be(String.Empty);
        }

        /// <summary>
        /// This method return an project with null image
        /// </summary>
        /// Author: Steven Nuñez
        /// Collaborator: Gabriel Revillat
        [Fact]
        public void NullImageReturnNull()
        {
            String? input = null;

            var result = new InvestigationProjectDTO(Id, Name, StartDate, EndDate, InvestigationGroupID, Description, Summary, input);

            result.Image.Should().Be(null);
        }

        [Fact]
        public void EndDateGreaterThanStartDate()
        {
            string aux1 = "Jan 1, 2009";
            string aux2 = "Jan 1, 2020";
            var starDate = DateTime.Parse(aux1);
            var endDate = DateTime.Parse(aux2);

            var result = new InvestigationProjectDTO(Id, Name, starDate, endDate, InvestigationGroupID, Description, Summary, Image);

            result.EndDate.Should().NotBeOnOrBefore(result.StartDate);
        }

        /// <summary>
        /// This method tests the fact that the start date is smaller than the end date
        /// </summary>
        /// Author: Sofia Campos (Pair programming during sprint 2)
        /// Collaborator: Gabriel Revillat, Steven Nuñez
        [Fact]
        public void StartDateSmallerThanEndDate()
        {
            string aux1 = "Jan 1, 2009";
            string aux2 = "Jan 1, 2020";

            var starDate = DateTime.Parse(aux1);
            var endDate = DateTime.Parse(aux2);

            var result = new InvestigationProjectDTO(Id, Name, starDate, endDate, InvestigationGroupID, Description, Summary, Image);

            result.StartDate.Should().NotBeAfter(result.EndDate); 
        }


    }
}
