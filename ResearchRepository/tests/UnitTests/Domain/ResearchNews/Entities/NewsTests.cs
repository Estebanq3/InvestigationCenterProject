using FluentAssertions;
using ResearchRepository.Domain.Core.Exceptions;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.ResearchNews.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ResearchRepository.Domain.People.Entities;

namespace UnitTests.Domain.ResearchNews.Entities
{
    /// <summary>
    /// Tests for News entity
    /// </summary>
    /// Author: Rodrigo Contreras (Monkey Madness)
    public class NewsTests
    {
        private static readonly RequiredString Name = RequiredString.TryCreate("Title").Success();
        private static readonly RequiredString Description = RequiredString.TryCreate("Description").Success();
        private static readonly DateTime Start = DateTime.Now;
        private static readonly DateTime Publication = DateTime.Now;
        private static readonly DateTime End = DateTime.Today.AddDays(1);
        private static readonly string? gDescription = "Esto es una descripcion";
        private static readonly string? Image = "/img/imagen.png";
        private static readonly NewsImage myImage = new NewsImage(Image);

        private IEnumerable<ResearchGroup> GetGroups()
        {
            var center = new ResearchCenter(Name, gDescription, Image, "Citic");
            for (var i = 1; i <= 5; ++i)
            {
                yield return new ResearchGroup(Name, gDescription, Image, DateTime.Now, center);
            }
        }

        private IEnumerable<NewsImage> GetImages()
        {
            for (var i = 1; i <= 5; ++i)
            {
                yield return new NewsImage(Image!);
            }
        }

        private IEnumerable<Person> GetPeople()
        {
            for(var i = 1; i <= 5; ++i)
            {
                yield return new Person($"person{i}@ucr.ac.cr", "Nombre", "Apellido1", "Apellido2", "Pais");
            }
        }

        [Fact]
        public void AssignImage()
        {
            // act
            var group = GetGroups().First();
            var news = new News(Name, Description, null,null,Start,Publication,End, group);

            // assert
            news.MainImage.Should().BeNull();

            news = new News(1, Name, Description, null, null, Start, Publication, End, group);

            news.Invoking(t => t.SetMainImage(myImage)).Should().Throw<DomainException>();
            news.Invoking(t => t.AddAssociatedImage(myImage)).Should().NotThrow();

            news.Invoking(t => t.SetMainImage(myImage)).Should().NotThrow();
            news.MainImage.Should().NotBeNull();

            news.Invoking(t => t.ClearMainImage()).Should().NotThrow();
            news.MainImage.Should().BeNull();

        }

        [Fact]
        public void ReAssignGroup()
        {
            // act
            var group = GetGroups().First();
            var news = new News(Name, Description, null, null, Start, Publication, End, group);
            // assert
            news.Invoking(t => t.AssignGroup(group)).Should().NotThrow();
        }

        
        [Fact]
        public void GetsTesting()
        {
            // act
            var group = GetGroups().First();
            var news = new News(Name, Description, null, null, Start, Publication, End, group);

            // assert
            var gDescription = news.Description;
            news.Description.Should().BeSameAs(Description);

            var gEnd = news.EndDate;
            news.EndDate.Should().BeSameDateAs(End);

            var gGroup = news.Group;
            news.Group.Should().BeSameAs(group);

            var gTitle = news.Title;
            news.Title.Should().BeSameAs(Name);

            var gStart = news.CreationDate;
            news.CreationDate.Should().BeSameDateAs(Start);

            var gPublication = news.PublicationDate;
            news.PublicationDate.Should().BeSameDateAs(Publication);

            var gVideo = news.VideoURL;
            news.VideoURL.Should().BeNull();

            var gPDF = news.DocumentURN;
            news.DocumentURN.Should().BeNull();

            var gDeleted = news.Deleted;
            news.Deleted.Should().BeFalse();

            var gImages = news.AssociatedImages;
            news.AssociatedImages.Should().BeEmpty();

            var gMainImage = news.MainImageId;
            news.MainImageId.Should().BeNull();
        }

        [Fact]
        public void AddValidAssociatedImageShouldAddImage()
        {
            //assign
            var group = GetGroups().First();
            var news = new News(Name, Description, null, null, Start, Publication, End, group);
            var image = GetImages().First();
            //act
            news.AddAssociatedImage(image);
            //assert
            news.AssociatedImages.Should().Contain(image);
        }

        [Fact]
        public void AddInvalidAssociatedImageShouldThrow()
        {
            //assign
            var group = GetGroups().First();
            var news = new News(Name, Description, null, null, Start, Publication, End, group);
            var image = GetImages().First();
            //act
            news.AddAssociatedImage(image);
            //assert
            news.Invoking(t => t.AddAssociatedImage(image)).Should().Throw<DomainException>().WithMessage("Image is already associated to this news.");
        }

        [Fact]
        public void RemoveValidAssociatedImageShouldRemoveImage()
        {
            //assign
            var group = GetGroups().First();
            var news = new News(Name, Description, null, null, Start, Publication, End, group);
            IEnumerable<NewsImage> newsImages = GetImages();
            var image = newsImages.First();
            var secondImage = newsImages.Last();
            //act
            news.AddAssociatedImage(image);
            news.AddAssociatedImage(secondImage);
            //assert
            news.Invoking(t => t.RemoveAssociatedImage(image)).Should().NotThrow();
            news.AssociatedImages.Should().NotContain(image);
            news.AssociatedImages.Should().Contain(secondImage);
        }

        [Fact]
        public void RemoveInvalidAssociatedImageShouldThrow()
        {
            //assign
            var group = GetGroups().First();
            var news = new News(Name, Description, null, null, Start, Publication, End, group);
            IEnumerable<NewsImage> newsImages = GetImages();
            var image = newsImages.First();
            var secondImage = newsImages.Last();
            //act
            news.AddAssociatedImage(image);
            //assert
            news.Invoking(t => t.RemoveAssociatedImage(secondImage)).Should().Throw<DomainException>().WithMessage("Image isn't associated to this news.");
        }

        [Fact]
        public void AddValidAssociatedPersonShouldAddImage()
        {
            //assign
            var group = GetGroups().First();
            var news = new News(Name, Description, null, null, Start, Publication, End, group);
            var person = GetPeople().First();
            //act
            news.AddAssociatedPerson(person);
            //assert
            news.AssociatedPeople.Should().Contain(person);
        }

        [Fact]
        public void AddInvalidAssociatedPersonShouldThrow()
        {
            //assign
            var group = GetGroups().First();
            var news = new News(Name, Description, null, null, Start, Publication, End, group);
            var person = GetPeople().First();
            //act
            news.AddAssociatedPerson(person);
            //assert
            news.Invoking(t => t.AddAssociatedPerson(person)).Should().Throw<DomainException>().WithMessage("Person is already associated to this news.");
        }

        [Fact]
        public void RemoveValidAssociatedPersonShouldRemoveImage()
        {
            //assign
            var group = GetGroups().First();
            var news = new News(Name, Description, null, null, Start, Publication, End, group);
            IEnumerable<Person> people = GetPeople();
            var person = people.First();
            var secondPerson = people.Last();
            //act
            news.AddAssociatedPerson(person);
            news.AddAssociatedPerson(secondPerson);
            //assert
            news.Invoking(t => t.RemoveAssociatedPerson(person)).Should().NotThrow();
            news.AssociatedPeople.Should().NotContain(person);
            news.AssociatedPeople.Should().Contain(secondPerson);
        }

        [Fact]
        public void RemoveInvalidAssociatedPersonShouldThrow()
        {
            //assign
            var group = GetGroups().First();
            var news = new News(Name, Description, null, null, Start, Publication, End, group);
            IEnumerable<Person> people = GetPeople();
            var person = people.First();
            var secondPerson = people.Last();
            //act
            news.AddAssociatedPerson(person);
            //assert
            news.Invoking(t => t.RemoveAssociatedPerson(secondPerson)).Should().Throw<DomainException>().WithMessage("Person isn't associated to this news.");
        }
    }
}
