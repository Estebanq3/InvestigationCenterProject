using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ResearchRepository.Domain.ResearchNews.Entities;
using ResearchRepository.Domain.ResearchNews.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_ResearchRepository;
using Xunit;

namespace IntegrationTests.Infrastructure.ResearchNews.Repositories
{
    public class NewsIntegrationTests : IClassFixture<NewsFactory<Startup>>
    {
        private readonly NewsFactory<Startup> _factory;

        public NewsIntegrationTests(NewsFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllNewsShouldReturnAllNews()
        {
            //arange
            const int newsCount = 9;
            var repository = _factory.Server.Services.GetRequiredService<INewsRepository>();

            //act
            var news = await repository.GetAllNewsAsync();

            //assert
            news.Should().HaveCount(newsCount);
        }
        
        [Fact]
        public async Task GetByIdAsyncWithInvalidIdReturnsNull()
        {
            //arrange
            const int newsId = 10; //doesnt exist
            var repository = _factory.Server.Services.GetRequiredService<INewsRepository>();

            //act
            var news = await repository.GetNewsByIdAsync(newsId);

            //assert
            news.Should().BeNull();
        }

        [Fact]
        public async Task DeleteNewsAsyncShouldDeleteNews()
        {
            //arrange
            const int newsId = 1;
            var repository = _factory.Server.Services.GetRequiredService<INewsRepository>();
            News? news = await repository.GetNewsByIdAsync(newsId);
            //act
            await repository.DeleteNewsAsync(news!);
            //assert
            var deletedNews = await repository.GetNewsByIdAsync(newsId);
            deletedNews.Should().BeNull();
            //cleanup
            _factory.SeedDatabaseForTests((DbContext)repository.UnitOfWork);
        }

        [Fact]
        public async Task DeleteNewsImageShouldDeleteNewsImage()
        {
            //arrange
            const int newsId = 2;
            var repository = _factory.Server.Services.GetRequiredService<INewsRepository>();
            var news = await repository.GetNewsByIdAsync(newsId);
            var newsImage = new NewsImage("img/news/10.png");
            newsImage.SetNews(news!);
            await repository.SaveNewsAsync(news!);
            //act
            await repository.DeleteNewsImage(newsImage);
            //assert
            news = await repository.GetNewsByIdAsync(newsId);
            news!.AssociatedImages.Should().NotContain(newsImage);
            //cleanup
            _factory.SeedDatabaseForTests((DbContext)repository.UnitOfWork);
        }

        [Fact]
        public async Task GetNewsByGroupIdShouldReturnNewsFromGroup()
        {
            //arrange
            const int groupId = 2;
            var repository = _factory.Server.Services.GetRequiredService<INewsRepository>();
            //act
            var newsList = await repository.GetNewsByGroupIdAsync(groupId);
            //assert
            int count = 4;
            foreach(var news in newsList)
            {
                news.Title.Should().Be($"Noticia prueba {count++}");
            }
        }

        [Fact]
        public async Task GetNewsByGroupPagedAsyncShouldReturnPagedNewsList()
        {
            //arrange
            const int groupId = 3;
            const int currentPage = 1;
            const int pageSize = 2;
            var repository = _factory.Server.Services.GetRequiredService<INewsRepository>();
            //act
            var newsList = await repository.GetNewsByGroupIdPagedAsync(groupId, currentPage, pageSize);

            //assert
            newsList!.Count().Should().Be(pageSize);
        }

        [Fact]
        public async Task GetNewsByTermPagedAsyncShouldReturnPagedNewsList()
        {
            //arrange
            const int groupId = 3;
            const int currentPage = 1;
            const int pageSize = 2;
            const string term = "term";
            const int numNewsWithTerm = 2; //configured seed_data accordingly
            var repository = _factory.Server.Services.GetRequiredService<INewsRepository>();
            //act
            var newsList = await repository.GetNewsByTermPagedAsync(groupId, currentPage, pageSize, term);
            //assert
            newsList!.Count().Should().Be(numNewsWithTerm);
        }

        [Fact]
        public async Task GetNewsCountShouldReturnCount()
        {
            //arrange
            const int newsCount = 9;
            var repository = _factory.Server.Services.GetRequiredService<INewsRepository>();
            //act
            var count = await repository.GetNewsCountAsync();
            //assert
            count.Should().Be(newsCount);
        }

        [Fact]
        public async Task GetNewsAsNotTrackingShouldReturnReadOnlyNews()
        {
            //arrange
            const int newsId = 2;
            NewsImage testImage = new NewsImage("test/path");
            var repository = _factory.Server.Services.GetRequiredService<INewsRepository>();
            //act
            var readonlyNews = await repository.GetNewsAsNotTracking(newsId);
            testImage.SetNews(readonlyNews!);
            await repository.SaveNewsAsync(readonlyNews!);
            //assert
            var news = await repository.GetNewsByIdAsync(newsId);
            news!.MainImage.Should().NotBe(testImage);
        }

        [Fact]
        public async Task GetNewsByTermCountReturnsCount()
        {
            //arrange
            const int groupId = 3;
            const int newsWithTerm = 2;
            const string term = "term";
            var repository = _factory.Server.Services.GetRequiredService<INewsRepository>();
            //act
            var count = await repository.GetNewsByTermCount(groupId, term);
            //assert
            count.Should().Be(newsWithTerm);
        }

        [Fact]
        public async Task GetNewsByGroupCountReturnsCount()
        {
            //arrange
            const int groupId = 2;
            const int newsFromGroup = 3;
            var repository = _factory.Server.Services.GetRequiredService<INewsRepository>();
            //act
            var count = await repository.GetNewsByGroupCount(groupId);
            //assert
            count.Should().Be(newsFromGroup);
        }
    }

    
}
