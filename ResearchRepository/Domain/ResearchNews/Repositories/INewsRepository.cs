using ResearchRepository.Domain.ResearchNews.DTOs;
using ResearchRepository.Domain.ResearchNews.Entities;
using ResearchRepository.Domain.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResearchRepository.Domain.ResearchGroups.Entities;

namespace ResearchRepository.Domain.ResearchNews.Repositories
{
    public interface INewsRepository : IRepository<News>
    {
        /// <summary>
        /// Saves the News object.
        /// </summary>
        /// Author: Rodrigo Contreras
        /// StoryID: ST-MM-6.1
        /// <param News="news">The news object</param>
        /// <returns></returns>
        Task SaveNewsAsync(News news);

        /// <summary>
        /// Edits and saves changes made to the news object.
        /// </summary>
        /// Author: Rodrigo Contreras
        /// StoryID: ST-MM-6.1
        /// <param News="news">The news object</param>
        /// <returns></returns>
        Task EditNewsAsync(News news);
        /// <summary>
        /// Get the news given its ID.
        /// </summary>
        /// Author: Rodrigo Contreras
        /// StoryID: ST-MM-6.1
        /// <param name="id">ID of the research news</param>
        /// <returns>News given the ID</returns>
        Task<News?> GetNewsByIdAsync(int id);

        /// <summary>
        /// Get the list of news given the ID of the group.
        /// </summary>
        /// Author: Rodrigo Contreras
        /// StoryID: ST-MM-6.1
        /// <param name="id">ID of the group id</param>
        /// <returns>List of News from that Group</returns>
        Task<IEnumerable<NewsDTO>?> GetNewsByGroupIdAsync(int id);

        /// <summary>
        /// Get the newsDTOs of a given the groupID, number of elements to 
        /// retrieve and the current page.
        /// </summary>
        /// Author: Josué Vargas
        /// StoryID: ST-MM-4.5
        /// <param name="id">Groupid where the news will retrieved</param>
        /// <param name="currentPage">Current page of the search</param>
        /// <param name="size">Number of elements to retrieve</param>
        Task<IEnumerable<NewsDTO>?> GetNewsByGroupIdPagedAsync(int id,int currentPage, int size);
        /// <summary>
        /// Get the newsDTOs of a given the groupID and a search term, number of elements to 
        /// retrieve and the current page.
        /// </summary>
        /// Author: Josué Vargas
        /// StoryID: ST-MM-4.5
        /// <param name="term">Search term</param>
        /// <param name="id">Groupid where the news will retrieved</param>
        /// <param name="currentPage">Current page of the search</param>
        /// <param name="size">Number of elements to retrieve</param>
        Task<IEnumerable<NewsDTO>?> GetNewsByTermPagedAsync(int id, int currentPage, int size, string term);

        /// <summary>
        /// Get all the news from all the groups.
        /// </summary>
        /// Author: Rodrigo Contreras
        /// StoryID: ST-MM-6.1
        /// <returns>The list of all news</returns>
        Task<IEnumerable<NewsDTO>?> GetAllNewsAsync();

        /// <summary>
        /// Get the count of total news in the database.
        /// </summary>
        /// Author: Rodrigo Contreras
        /// StoryID: ST-MM-6.1
        /// <returns>The count of total news in the database</returns>
        Task<int> GetNewsCountAsync();
        /// <summary>
        /// Get the number of news given the ResearchGroup
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-6.2
        /// <param name="group">Group where the news will retrieved</param>
        Task<int> GetNewsByGroupCount(int groupId);
        /// <summary>
        /// Get the number of news given the ResearchGroup and a search term
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-6.2
        /// <param name="term">Search term</param>
        /// <param name="group">Group where the news will retrieved</param>
        Task<int> GetNewsByTermCount(int groupId, string term);
        /// <summary>
        /// Save the provided news object.
        /// </summary>
        /// Author: Rodrigo Contreras
        /// StoryID: ST-MM-6.1
        /// <param name="news">The object of news to be saved.</param>
        Task DeleteNewsAsync(News news);

        /// <summary>
        /// Delete the news image object.
        /// </summary>
        /// Author: Rodrigo Contreras
        /// StoryID: ST-MM-6.5
        /// <param name="newsImage">The news image object to be deleted.</param>
        Task DeleteNewsImage(NewsImage newsImage);
        /// <summary>
        /// Gets the news as not tracked. Readonly, changes to this object will not be saved.
        /// </summary>
        /// Author: Rodrigo Contreras
        /// StoryID: ST-MM-6.5
        /// <param name="id"></param>
        /// <returns></returns>
        Task<News?> GetNewsAsNotTracking(int id);
    }
}
