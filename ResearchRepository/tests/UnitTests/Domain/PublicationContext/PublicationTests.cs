using FluentAssertions;
using ResearchRepository.Domain.PublicationContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.PublicationContext
{
    public class PublicationTests
    {

        [Fact]
        public void checkPublicationSummaryTest()
        {
            //arrange
            var publication = new Publication("Some name", null, DateTime.Now,
                "Journal", "Some Journal", "1717/7777", 1, null, null, null);

            //act
            publication.checkPublicationSummary();

            var resultingSummary = publication.Summary;

            //assert
            resultingSummary.Should().Be("Esta publicacion no contiene un resumen");
        }

        [Fact]
        public void orderPublicationByYearTest()
        {
            //arrange

            DateTime dt1 = new DateTime(2015, 12, 31);
            DateTime dt2 = new DateTime(2018, 11, 27);  
            DateTime dt3 = new DateTime(2010, 4, 7);
            DateTime dt4 = new DateTime(2013, 10, 7);

            List<Publication> publicationsList = new List<Publication>
            {

                new Publication { Id="111.111", Name="Prueba1", Year = dt1},
                new Publication { Id="111.222", Name="Prueba2", Year = dt2},
                new Publication { Id="111.333", Name="Prueba3", Year = dt3},
                new Publication { Id="111.444", Name="Prueba4", Year = dt4}

            };

            //act
            //publicationsList = new Publication().orderByYear(publicationsList).ToList();
            var ordenada = new Publication().orderByYear(publicationsList).ToList();

            //assert
            ordenada.Should().Equal(publicationsList.OrderByDescending(publications => publications.Year));
        }




    }
}
