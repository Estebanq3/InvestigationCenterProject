using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ResearchRepository.Domain.ResearchNews.DTOs;
using FluentAssertions;
using ResearchRepository.Domain.ResearchNews.Entities;

namespace UnitTests.Domain.ResearchNews.DTOs
{
    /// <summary>
    /// We this test we focus on the Description (Html) and the Image path
    /// </summary>
    /// Author: Tyron Fonseca
    public class NewsDTOTests
    {
        private static readonly int Id = 1;
        private static readonly string Title = "Titulo";
        private static readonly string ExpectedDescription = "Esta es una string normal.";
        private static readonly NewsImage? Image = new NewsImage("/img/imagen.png");
        private static readonly string? ImagePlaceholder = "img/news-placeholder.jpg";
        private static readonly DateTime PublicationDate = DateTime.Now;
        private static readonly DateTime CreationDate = DateTime.Now;
        private static readonly DateTime EndDate = DateTime.Now;

        [Fact]
        public void DescriptionEmptyReturnEmpty()
        {
            //arrange
            var input = string.Empty;

            //act
            var result = new NewsDTO(Id, Title, input, Image, CreationDate, PublicationDate, EndDate);

            //assert
            result.Description.Should().Be(string.Empty);
        }

        [Fact]
        public void DescriptionNullReturnNull()
        {
            //arrange
            string? input = null;

            //act
            var result = new NewsDTO(Id, Title, input, Image, CreationDate, PublicationDate, EndDate);

            //assert
            result.Description.Should().Be(null);
        }


        [Fact]
        public void DescriptionNormalStringReturnNormalString()
        {
            //arrange
            string? input = "Esta es una string normal.";

            //act
            var result = new NewsDTO(Id, Title, input, Image, CreationDate, PublicationDate, EndDate);

            //assert
            result.Description.Should().Be(input);
        }

        [Fact]
        public void DescriptionHTMLStringReturnNormalString()
        {
            //arrange
            string? input = "<a>Esta es una string normal.</a>";
            

            //act
            var result = new NewsDTO(Id, Title, input, Image, CreationDate, PublicationDate, EndDate);

            //assert
            result.Description.Should().Be(ExpectedDescription);
        }

        [Fact]
        public void DescriptionHTMLInValidStringReturnNormalString()
        {
            //arrange
            string? input = "<a>Esta es una string normal.</";

            //act
            var result = new NewsDTO(Id, Title, input, Image, CreationDate, PublicationDate, EndDate);

            //assert
            result.Description.Should().Be(ExpectedDescription);
        }


        [Fact]
        public void ImageEmptyStringReturnPlaceHolder()
        {
            //arrange

            //act
            var result = new NewsDTO(Id, Title, ExpectedDescription, null, CreationDate, PublicationDate, EndDate);

            //assert
            result.MainImage.Should().Be(ImagePlaceholder);
        }

        [Fact]
        public void ImageNullStringReturnPlaceHolder()
        {
            //arrange
            NewsImage? input = null;

            //act
            var result = new NewsDTO(Id, Title, ExpectedDescription, input, CreationDate, PublicationDate, EndDate);

            //assert
            result.MainImage.Should().Be(ImagePlaceholder);
        }

        [Fact]
        public void ImageStringReturnSameString()
        {
            //arrange

            //act
            var result = new NewsDTO(Id, Title, ExpectedDescription, Image, CreationDate, PublicationDate, EndDate);

            //assert
            result.MainImage.Should().Be(Image.Path);
        }
    }
}
