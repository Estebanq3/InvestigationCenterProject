using Moq;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.ResearchNews.DTOs;
using ResearchRepository.Domain.ResearchNews.Entities;
using ResearchRepository.Domain.ResearchNews.Repositories;
using ResearchRepository.Application.ResearchNews.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.People.Entities;

namespace UnitTests.Application.ResearchNews
{
    public class NewsServiceTests
    {
        private static readonly string Title = "News Title";
        private static readonly string Description = "News Description";
        private static readonly NewsImage MainImage = new NewsImage("img/news-placeholder.jpg");
        private static readonly DateTime CreationDate = new DateTime(2021, 11, 18, 12, 15, 0);
        private static readonly DateTime PublicationDate = new DateTime(2021, 11, 19, 7, 0, 0);
        private static readonly DateTime EndDate = new DateTime(2021, 11, 25, 0, 0, 0);
        private static readonly ResearchCenter Center = new ResearchCenter(RequiredString.TryCreate("Center Name").Success(), null, null, null);
        private static readonly ResearchGroup Group = new ResearchGroup(1,RequiredString.TryCreate("Group Name").Success(), "Group Description", null, DateTime.Now, Center);
        private static readonly Person Person = new Person("person@ucr.ac.cr", "Nombre", "Apellido1", "Apellido2", "Pais");
        
        private static IEnumerable<NewsDTO> GetNews()
        {
            const int newsCount = 1000;
            for (int n = 1; n <= newsCount; ++n)
            {
                yield return new NewsDTO(n, Title, Description, MainImage, CreationDate, PublicationDate, EndDate );
            }
        }

        [Fact]
        public async Task GetAllNewsAsyncShouldReturnNewsDTOs()
        {
            //arrange
            var news = GetNews().ToList();
            var mockNewsRepository = new Mock<INewsRepository>();
            mockNewsRepository.Setup(repo => repo.GetAllNewsAsync()).ReturnsAsync(news);
            var newsService = new NewsService(mockNewsRepository.Object);

            //act
            var results = await newsService.GetAllNewsAsync();

            //assert
            results.Should().BeEquivalentTo(news);
        }

        [Fact]
        public async Task GetNewsByIdAsyncWithValidIdShouldReturnNews()
        {
            //arrange
            const int id = 1;
            var news = new News(
                id,
                RequiredString.TryCreate(Title).Success(),
                RequiredString.TryCreate(Description, 8000).Success(),
                null,
                null,
                CreationDate,
                PublicationDate,
                EndDate,
                Group);

            var mockNewsRepository = new Mock<INewsRepository>();
            mockNewsRepository.Setup(repo => repo.GetNewsByIdAsync(id)).ReturnsAsync(news);
            var newsService = new NewsService(mockNewsRepository.Object);

            //act
            var result = await newsService.GetNewsByIdAsync(id);

            //assert
            result.Should().Be(news);
        }

        [Fact]
        public async Task CreateNewsWithValidGroupShouldAddNews()
        {
            //arrange
            const int id = 1;
            var news = new News(
                id,
                RequiredString.TryCreate(Title).Success(),
                RequiredString.TryCreate(Description, 8000).Success(),
                null,
                null,
                CreationDate,
                PublicationDate,
                EndDate,
                Group);

            var mockNewsRepository = new Mock<INewsRepository>();
            var newsService = new NewsService(mockNewsRepository.Object);

            //act
            await newsService.CreateNewsAsync(news);

            //assert
            Group.News.Should().Contain(news);
            news.Group.Should().Be(Group);
            mockNewsRepository.Verify(repo => repo.SaveNewsAsync(news), Times.Once);
        }

        [Fact]
        public async Task DeleteNewsAsyncShouldRemoveNews()
        {
            //arrange
            const int id = 2;
            var news = new News(
                id,
                RequiredString.TryCreate(Title).Success(),
                RequiredString.TryCreate(Description, 8000).Success(),
                null,
                null,
                CreationDate,
                PublicationDate,
                EndDate,
                Group);

            var mockNewsRepository = new Mock<INewsRepository>();
            var newsService = new NewsService(mockNewsRepository.Object);
            await newsService.CreateNewsAsync(news);

            //act
            await newsService.DeleteNewsAsync(news);

            //assert
            Group.News.Should().NotContain(news);
            mockNewsRepository.Verify(repo => repo.DeleteNewsAsync(news), Times.Once);
            mockNewsRepository.Verify(repo => repo.SaveNewsAsync(news), Times.Once);
        }

        [Fact]
        public async Task ChangeNewsMainImageShouldModifyMainImage()
        {
            //arange
            const int id = 2;
            var news = new News(
                id,
                RequiredString.TryCreate(Title).Success(),
                RequiredString.TryCreate(Description, 8000).Success(),
                null,
                null,
                CreationDate,
                PublicationDate,
                EndDate,
                Group);

            var mockNewsRepository = new Mock<INewsRepository>();
            mockNewsRepository.Setup(repo => repo.GetNewsByIdAsync(id)).ReturnsAsync(news);
            var newsService = new NewsService(mockNewsRepository.Object);
            await newsService.CreateNewsAsync(news);
            //act
            news.AddAssociatedImage(MainImage);
            await newsService.ChangeNewsMainImage(news, MainImage);

            //assert
            news.MainImage.Should().Be(MainImage);
            MainImage.News.Should().Be(news);
            mockNewsRepository.Verify(repo => repo.SaveNewsAsync(news), Times.Exactly(2));
        }

        [Fact]
        public async Task DeleteNewsImageShouldDeleteImage()
        {
            //arange
            const int id = 3;
            var news = new News(
                id,
                RequiredString.TryCreate(Title).Success(),
                RequiredString.TryCreate(Description, 8000).Success(),
                null,
                null,
                CreationDate,
                PublicationDate,
                EndDate,
                Group);

            var mockNewsRepository = new Mock<INewsRepository>();
            mockNewsRepository.Setup(repo => repo.GetNewsByIdAsync(id)).ReturnsAsync(news);
            var newsService = new NewsService(mockNewsRepository.Object);
            await newsService.CreateNewsAsync(news);
            news.AddAssociatedImage(MainImage);
            await newsService.ChangeNewsMainImage(news, MainImage);

            //act
            await newsService.DeleteNewsImage(MainImage);

            //assert
            news.MainImage.Should().BeNull();
            news.AssociatedImages.Should().NotContain(MainImage);
            mockNewsRepository.Verify(repo => repo.SaveNewsAsync(news), Times.Exactly(2));
            mockNewsRepository.Verify(repo => repo.DeleteNewsImage(MainImage), Times.Once);
        }

        [Fact]
        public async Task GetNewsByGroupIdAsyncShouldReturnNewsList()
        {
            //arrange
            const int id = 1;
            var newsList = GetNews().Take(3);
            var mockNewsRepository = new Mock<INewsRepository>();
            mockNewsRepository.Setup(repo => repo.GetNewsByGroupIdAsync(id)).ReturnsAsync(newsList);
            var newsService = new NewsService(mockNewsRepository.Object);
            //act
            var result = await newsService.GetNewsByGroupIdAsync(id);
            //assert
            result.Should().BeEquivalentTo(newsList);
        }

        [Fact]
        public async Task GetNewsByGroupIdPagedAsyncShouldReturnNewsList()
        {
            //arrange
            const int id = 1;
            const int currentPage = 1;
            const int size = 3;
            var newsList = GetNews().Take(3);
            var mockNewsRepository = new Mock<INewsRepository>();
            mockNewsRepository.Setup(repo => repo.GetNewsByGroupIdPagedAsync(id, currentPage, size)).ReturnsAsync(newsList);
            var newsService = new NewsService(mockNewsRepository.Object);
            //act
            var result = await newsService.GetNewsByGroupIdPagedAsync(id, currentPage, size);
            //assert
            result.Should().BeEquivalentTo(newsList);
            result.Should().HaveCount(size);
        }

        [Fact]
        public async Task GetNewsByTermPagedAsyncShouldReturnNewsList()
        {
            //arrange
            const int id = 1;
            const int currentPage = 1;
            const int size = 3;
            const string term = "term";
            var newsList = GetNews().Take(3);
            var mockNewsRepository = new Mock<INewsRepository>();
            mockNewsRepository.Setup(repo => repo.GetNewsByTermPagedAsync(id, currentPage, size,term)).ReturnsAsync(newsList);
            var newsService = new NewsService(mockNewsRepository.Object);
            //act
            var result = await newsService.GetNewsByTermPagedAsync(id, currentPage, size, term);
            //assert
            result.Should().BeEquivalentTo(newsList);
            result.Should().HaveCount(size);
        }

        [Fact]
        public async Task GetNewsCountAsyncShouldReturnCount()
        {
            //arrange
            const int count = 1000;
            var mockNewsRepository = new Mock<INewsRepository>();
            mockNewsRepository.Setup(repo => repo.GetNewsCountAsync()).ReturnsAsync(count);
            var newsService = new NewsService(mockNewsRepository.Object);
            //act
            var result = await newsService.GetNewsCountAsync();
            //assert
            result.Should().Be(count);
        }

        [Fact]
        public async Task GetNewsByTermCountShouldReturnCount()
        {
            //arrange
            const int count = 1000;
            const string term = "term";
            var mockNewsRepository = new Mock<INewsRepository>();
            mockNewsRepository.Setup(repo => repo.GetNewsByTermCount(Group.Id, term)).ReturnsAsync(count);
            var newsService = new NewsService(mockNewsRepository.Object);
            //act
            var result = await newsService.GetNewsByTermCount(Group, term);
            //assert
            result.Should().Be(count);
        }

        [Fact]
        public async Task GetNewsByGroupCountShouldReturnCount()
        {
            //arrange
            const int count = 1000;
            var mockNewsRepository = new Mock<INewsRepository>();
            mockNewsRepository.Setup(repo => repo.GetNewsByGroupCount(Group.Id)).ReturnsAsync(count);
            var newsService = new NewsService(mockNewsRepository.Object);
            //act
            var result = await newsService.GetNewsByGroupCount(Group);
            //assert
            result.Should().Be(count);
        }

        [Fact]
        public async Task GetNewsByIdAsReadOnlyShouldReturnNewsObject()
        {
            //arrange
            const int id = 1;
            var news = new News(
                id,
                RequiredString.TryCreate(Title).Success(),
                RequiredString.TryCreate(Description, 8000).Success(),
                null,
                null,
                CreationDate,
                PublicationDate,
                EndDate,
                Group);
            var mockNewsRepository = new Mock<INewsRepository>();
            mockNewsRepository.Setup(repo => repo.GetNewsAsNotTracking(id)).ReturnsAsync(news);
            var newsService = new NewsService(mockNewsRepository.Object);
            //act
            var result = await newsService.GetNewsByIdAsReadOnly(id);
            //assert
            result.Should().Be(news);
        }

        [Fact]
        public async Task AddPersonToNewsShouldAddPerson()
        {
            //arrange
            const int newsId = 1;
            var news = new News(
                newsId,
                RequiredString.TryCreate(Title).Success(),
                RequiredString.TryCreate(Description, 8000).Success(),
                null,
                null,
                CreationDate,
                PublicationDate,
                EndDate,
                Group);
            var mockNewsRepository = new Mock<INewsRepository>();
            var newsService = new NewsService(mockNewsRepository.Object);
            //act
            await newsService.AddPersonToNews(news, Person);
            //assert
            news.AssociatedPeople.Should().Contain(Person);
            mockNewsRepository.Verify(repo => repo.SaveNewsAsync(news), Times.Once);
            
        }

        [Fact]
        public async Task RemovePersonFromNews()
        {
            //arrange
            const int newsId = 1;
            var news = new News(
                newsId,
                RequiredString.TryCreate(Title).Success(),
                RequiredString.TryCreate(Description, 8000).Success(),
                null,
                null,
                CreationDate,
                PublicationDate,
                EndDate,
                Group);
            var mockNewsRepository = new Mock<INewsRepository>();
            var newsService = new NewsService(mockNewsRepository.Object);
            await newsService.AddPersonToNews(news, Person);
            //act
            await newsService.RemovePersonFromNews(news, Person);
            //assert
            news.AssociatedPeople.Should().NotContain(Person);
            mockNewsRepository.Verify(repo => repo.SaveNewsAsync(news), Times.Exactly(2));
        }
    }

    
}
