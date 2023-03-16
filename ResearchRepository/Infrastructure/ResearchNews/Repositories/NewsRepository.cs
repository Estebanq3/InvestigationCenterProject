using Microsoft.EntityFrameworkCore;
using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.ResearchNews.DTOs;
using ResearchRepository.Domain.ResearchNews.Entities;
using ResearchRepository.Domain.ResearchNews.Repositories;
using ResearchRepository.Infrastructure.ResearchGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Infrastructure.ResearchNews.Repositories
{
    internal class NewsRepository : INewsRepository
    {
        private readonly ResearchGroupsDbContext _dbContext;

        public IUnitOfWork UnitOfWork => _dbContext;

        public NewsRepository(ResearchGroupsDbContext context)
        {
            _dbContext = context;
        }

        public async Task DeleteNewsAsync(News news)
        {
            if(news.MainImage != null)
            {
                _dbContext.Entry(news.MainImage).State = EntityState.Detached;
                foreach (var i in news.AssociatedImages)
                {
                    _dbContext.Entry(i).State = EntityState.Detached;
                }
            }
            _dbContext.Remove(news);
            await _dbContext.SaveEntitiesAsync();
        }

        public async Task DeleteNewsImage(NewsImage newsImage)
        {
            var imageToDelete = await _dbContext.NewsImage.FirstOrDefaultAsync(i => i.Id == newsImage.Id);
            if(imageToDelete != null)
                _dbContext.Entry(imageToDelete).State = EntityState.Deleted;
            await _dbContext.SaveEntitiesAsync();

        }

        public async Task<News?> GetNewsByIdAsync(int id)
        {
            return await _dbContext.News.Include(n => n.Group).Include(i => i.AssociatedImages).Include(i => i.MainImage).Include(p => p.AssociatedPeople).FirstOrDefaultAsync(n => n.Id == id && n.Deleted != true);
        }

        public async Task<IEnumerable<NewsDTO>?> GetNewsByGroupIdAsync(int id)
        {
            return await _dbContext.News.Where(n => n.Group!.Id == id && n.Deleted != true).OrderByDescending(n => n.PublicationDate).Select(n => new NewsDTO(n.Id, n.Title.ToString(), n.Description.ToString(), n.MainImage!, n.CreationDate, n.PublicationDate, n.EndDate)).ToListAsync();
        }
        public async Task<IEnumerable<NewsDTO>?> GetNewsByGroupIdPagedAsync(int id, int currentPage, int size)
        {
            return await _dbContext.News.Where(n => n.Group!.Id == id && n.Deleted != true).OrderByDescending(n => n.PublicationDate).Select(n => new NewsDTO(n.Id, n.Title.ToString(), n.Description.ToString(), n.MainImage!, n.CreationDate, n.PublicationDate, n.EndDate)).Skip((currentPage - 1) * size).Take(size).ToListAsync();
        }

        public async Task<IEnumerable<NewsDTO>?> GetNewsByTermPagedAsync(int id, int currentPage, int size, string term)
        {
            var data = await _dbContext.News.Where(n => n.Group!.Id == id && n.Deleted != true).ToListAsync();

            return await Task.FromResult(data.Where(n => n.Title.ToString().ToLower().Contains(term.ToLower())).OrderByDescending(n => n.PublicationDate)
                .Select(n => new NewsDTO(n.Id, n.Title.ToString(), n.Description.ToString(), n.MainImage!, n.CreationDate, n.PublicationDate, n.EndDate))
             .Skip((currentPage - 1) * size).Take(size));
        }

        public async Task<IEnumerable<NewsDTO>?> GetAllNewsAsync()
        {
            return await _dbContext.News.OrderByDescending(n => n.PublicationDate).Select(n => new NewsDTO(n.Id, n.Title.ToString(), n.Description.ToString(), n.MainImage!, n.CreationDate, n.PublicationDate, n.EndDate)).ToListAsync();
        }

        public async Task<int> GetNewsCountAsync()
        {
            return await _dbContext.News.CountAsync();
        }

        public async Task SaveNewsAsync(News news)
        {
            var mainImage = news.ClearMainImage();
            
            using (var transactionSaveNews = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Update(news);
                    _dbContext.SaveChanges();
                    transactionSaveNews.Commit();
                }
                catch (Exception ex)
                {
                    transactionSaveNews.Rollback();
                    throw new Exception("Could not create News object.");
                }
            }
            //Multiple transactions, due to circular dependency we must create the news
            //and assign the mainImage after.
            if (mainImage != null)
            {
                using (var transactionSaveMainImage = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        news.SetMainImage(mainImage);
                        _dbContext.Update(news);
                        _dbContext.SaveChanges();
                        transactionSaveMainImage.Commit();
                    }
                    catch (Exception ex)
                    {
                        transactionSaveMainImage.Rollback();
                        throw new Exception("Could not assign main image to the News object.");
                    }
                }
            }
        }

        public async Task EditNewsAsync(News news)
        {
            var newsToInsert = await _dbContext.News.FirstOrDefaultAsync(n => n.Id == news.Id);
            if (newsToInsert != null)
            {
                _dbContext.Entry(newsToInsert).State = EntityState.Detached;
                _dbContext.Entry(news).State = EntityState.Modified;
                foreach (var image in news.AssociatedImages)
                {
                    if (image.Id == 0)
                    {
                        _dbContext.Add(image);
                    }
                }
            }
            _dbContext.Update(news);
            await _dbContext.SaveEntitiesAsync();
        }

        public async Task<int> GetNewsByTermCount(int groupId, string term)
        {
            var data = await _dbContext.News.Where(n => n.Group.Id == groupId && n.Deleted != true).OrderByDescending(n => n.PublicationDate).ToListAsync();

            return await Task.FromResult(data.Where(n => n.Title.ToString().ToLower().Contains(term.ToLower())).Count());
        }

        public async Task<int> GetNewsByGroupCount(int groupId)
        {
            return await _dbContext.News.Where(n => n.Group.Id == groupId && n.Deleted != true).CountAsync();
        }

        public async Task<News?> GetNewsAsNotTracking(int id)
        {
            return await _dbContext.News
                .Include(n => n.Group).AsNoTracking()
                .Include(i => i.AssociatedImages).AsNoTracking()
                .Include(i => i.MainImage).AsNoTracking()
                .Include(p => p.AssociatedPeople).AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id && n.Deleted != true);
        }
    }
}

