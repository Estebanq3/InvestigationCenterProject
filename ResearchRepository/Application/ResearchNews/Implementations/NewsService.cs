using ResearchRepository.Application.ResearchNews;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.ResearchNews.DTOs;
using ResearchRepository.Domain.ResearchNews.Entities;
using ResearchRepository.Domain.ResearchNews.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Application.ResearchNews.Implementations
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }
        public async Task CreateNewsAsync(News news)
        {
            ResearchGroup group = news.Group!;
            group.AddNewsToGroup(news);
            await _newsRepository.SaveNewsAsync(news);
        }

        public async Task EditNewsAsync(News news)
        {
            await _newsRepository.EditNewsAsync(news);
        }

        public async Task DeleteNewsAsync(News news)
        {
            var group = news.Group;
            group.RemoveNewsFromGroup(news);
            await _newsRepository.DeleteNewsAsync(news);
        }

        public async Task<IEnumerable<NewsDTO>?> GetAllNewsAsync()
        {
            return await _newsRepository.GetAllNewsAsync();
        }

        public async Task<IEnumerable<NewsDTO>?> GetNewsByGroupIdAsync(int id)
        {
            return await _newsRepository.GetNewsByGroupIdAsync(id);
        }
        public async Task<IEnumerable<NewsDTO>?> GetNewsByGroupIdPagedAsync(int id,int currentPage,int size)
        {
            return await _newsRepository.GetNewsByGroupIdPagedAsync(id,currentPage,size);
        }
        public async Task<IEnumerable<NewsDTO>?> GetNewsByTermPagedAsync(int id, int currentPage, int size,string term)
        {
            return await _newsRepository.GetNewsByTermPagedAsync(id, currentPage, size,term);
        }

        public async Task<News?> GetNewsByIdAsync(int id)
        {
            return await _newsRepository.GetNewsByIdAsync(id);
        }

        public async Task<int> GetNewsCountAsync()
        {
            return await _newsRepository.GetNewsCountAsync();
        }

        public async Task<int> GetNewsByTermCount(ResearchGroup group, string term)
        {
            return await _newsRepository.GetNewsByTermCount(group.Id, term);
        }

        public async Task<int> GetNewsByGroupCount(ResearchGroup group)
        {
            return await _newsRepository.GetNewsByGroupCount(group.Id);
        }
        public async Task ChangeNewsMainImage(News news, NewsImage image)
        {
            news.SetMainImage(image);
            await _newsRepository.SaveNewsAsync(news);
        }

        public async Task<News?> GetNewsByIdAsReadOnly(int id)
        {
            return await _newsRepository.GetNewsAsNotTracking(id);
        }

        public async Task DeleteNewsImage(NewsImage image)
        {
            var news = image.News;
            news.RemoveAssociatedImage(image);
            await _newsRepository.DeleteNewsImage(image);
        }

        public async Task AddPersonToNews(News news, Person person)
        {
            news.AddAssociatedPerson(person);
            await _newsRepository.SaveNewsAsync(news);
        }

        public async Task RemovePersonFromNews(News news, Person person)
        {
            news.RemoveAssociatedPerson(person);
            await _newsRepository.SaveNewsAsync(news);
        }
    }
}
    
