using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.ResearchNews.DTOs;
using ResearchRepository.Domain.ResearchNews.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Application.ResearchNews
{
    public interface INewsService
    {
        //News
        /// <summary>
        /// Adds the News object to the database.
        /// </summary>
        /// Author: Rodrigo Contreras (Monkey Madness)
        /// StoryID: ST-MM-6.1
        /// <param name="news"></param>
        /// <returns></returns>
        Task CreateNewsAsync(News news);
        /// <summary>
        /// Saves the changes made to the news in the database.
        /// </summary>
        /// Author: Rodrigo Contreras (Monkey Madness)
        /// StoryID: ST-MM-6.5
        /// <param name="news"></param>
        /// <returns></returns>
        Task EditNewsAsync(News news);
        /// <summary>
        /// Deletes the News object from the database.
        /// </summary>
        /// Author: Rodrigo Contreras (Monkey Madness)
        /// StoryID: ST-MM-6.1
        /// <param name="news"></param>
        /// <returns></returns>
        Task DeleteNewsAsync(News news);
        /// <summary>
        /// Get the news given its ID.
        /// </summary>
        /// Author: Rodrigo Contreras
        /// StoryID: ST-MM-6.1
        /// <param name="id">ID of the research news</param>
        /// <returns>News given the ID</returns>
        Task<News?> GetNewsByIdAsync(int id);
        /// <summary>
        /// Get all the news from all the groups.
        /// </summary>
        /// Author: Rodrigo Contreras
        /// StoryID: ST-MM-6.1
        /// <returns>The list of all news</returns>
        Task<IEnumerable<NewsDTO>?> GetAllNewsAsync();
        /// <summary>
        /// Get the list of news given the ID of the group.
        /// </summary>
        /// Author: Rodrigo Contreras
        /// StoryID: ST-MM-6.1
        /// <param name="id">ID of the group id</param>
        /// <returns>List of News from that Group</returns>
        Task<IEnumerable<NewsDTO>?> GetNewsByGroupIdAsync(int id);
        /// <summary>
        /// Get the newsDTO of a given the groupID, number of elements to 
        /// retrieve and the current page.
        /// </summary>
        /// Author: Josué Vargas
        /// StoryID: ST-MM-4.4
        /// <param name="id">ID of the group where the newsDTO will retrieved</param>
        /// <param name="currentPage">Current page of the search</param>
        /// <param name="size">Number of elements to retrieve</param>
        Task<IEnumerable<NewsDTO>?> GetNewsByGroupIdPagedAsync(int id,int currentPage, int size);
        /// <summary>
        /// Get the newsDTO of a given the groupID, number of elements to 
        /// retrieve, term to be searched and the current page.
        /// </summary>
        /// Author: Josué Vargas
        /// StoryID: ST-MM-4.4
        /// <param name="id">ID of the group where the newsDTO will retrieved</param>
        /// <param name="currentPage">Current page of the search</param>
        /// <param name="size">Number of elements to retrieve</param>
        /// <param name="term">Term to be searched</param>
        Task<IEnumerable<NewsDTO>?> GetNewsByTermPagedAsync(int id, int currentPage, int size, string term);

        /// <summary>
        /// Returns the total count of News regardless of the group they beint to.
        /// </summary>
        /// Author: Rodrigo Contreras (Monkey Madness)
        /// StoryID: ST-MM-6.1
        /// <returns>Number of news</returns>
        Task<int> GetNewsCountAsync();
        /// <summary>
        /// Get the number of news given the ResearchGroup and a search term
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-6.2
        /// <param name="term">Search term</param>
        /// <param name="group">Group where the news will retrieved</param>
        Task<int> GetNewsByTermCount(ResearchGroup group, string term);
        /// <summary>
        /// Get the news of a given the ResearchGroup and a search term, number of elements to 
        /// retrieve and the current page.
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-6.2
        /// <param name="term">Search term</param>
        /// <param name="group">Group where the news will retrieved</param>
        /// <param name="currentPage">Current page of the search</param>
        /// <param name="size">Number of elements to retrieve</param>
        Task<int> GetNewsByGroupCount(ResearchGroup group);
        /// <summary>
        /// Changes News' main image to the specified image.
        /// </summary>
        /// Author: Rodrigo Contreras (Monkey Madness)
        /// StoryID: ST-MM-6.5
        /// <param name="news">The news object</param>
        /// <param name="image">The news image object</param>
        /// <returns></returns>
        Task ChangeNewsMainImage(News news, NewsImage image);
        /// <summary>
        /// Gets the news as readonly changes to this entity will not be saved.
        /// </summary>
        /// Author: Rodrigo Contreras
        /// StoryID: ST-MM-6.5
        /// <param name="id"></param>
        /// <returns></returns>
        Task<News?> GetNewsByIdAsReadOnly(int id);
        /// <summary>
        /// Deletes the provided NewsImage.
        /// </summary>
        /// Author: Rodrigo Contreras
        /// StoryID: ST-MM-6.5
        /// <param name="image">The news image to be deleted</param>
        /// <returns></returns>
        Task DeleteNewsImage(NewsImage image);
        /// <summary>
        /// Adds the person to the news.
        /// </summary>
        /// Author: Rodrigo Contreras (Monkey Madness)
        /// StoryID: ST-MM-6.4
        /// <param name="news">The news object</param>
        /// <param name="person">The person object</param>
        /// <returns></returns>
        Task AddPersonToNews(News news, Person person);
        /// <summary>
        /// Removes the person from the news.
        /// </summary>
        /// Author: Rodrigo Contreras (Monkey Madness)
        /// StoryID: ST-MM-6.4
        /// <param name="news">The news object</param>
        /// <param name="person">The person object</param>
        /// <returns></returns>
        Task RemovePersonFromNews(News news, Person person);
    }
}
