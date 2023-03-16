using System.Collections.Generic;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.ResearchGroups.DTOs;
using ResearchRepository.Domain.ResearchAreas.Entities;

namespace ResearchRepository.Domain.ResearchGroups.Repositories
{
    public interface IResearchCenterRepository : IRepository<ResearchCenter>
    {
        /// <summary>
        /// Gets a list of all Research Centers DTO in the database
        /// Author: Nelson Alvarez
        /// StoryID: ST-MM-3.2
        /// </summary>
        /// <returns>ResearchCenter DTO in Enumerable class</returns>
        Task<IEnumerable<ResearchCenterDTO>> GetAllAsync();

        /// <summary>
        /// Gets a list of all Research Centers in the database
        /// Author: Sebastian Montero (Panas)
        /// StoryID: ST-PA-1.6
        /// </summary>
        /// <returns>ResearchCenter in IList class</returns>
        
        Task<IList<ResearchCenter>> GetAllCenters();

        /// <summary>
        /// Get all the research groups from a list
        /// </summary>
        /// Author: Sebastian Montero Castro
        /// StoryID: ST-PA-1.6
        /// <param name="centerId">Unique identifier of the center</param>
        /// <returns>a List of all the groups in the specified center</returns>
        Task<IList<ResearchGroup>> GetAllGroupsFromCenter(int centerId);

        /// <summary>
        /// saves the ResearchGroup object.
        /// </summary>
        /// Author: Dean Vargas
        /// StoryID: ST-MM-1.1
        /// <param name="group">The ResearchGroup object</param>
        /// <returns></returns>
        Task SaveGroupAsync(ResearchGroup group);

        /// <summary>
        /// Deletes the provided ResearchGroup object.
        /// </summary>
        /// Author: Dean Vargas
        /// StoryID: ST-MM-1.2
        /// <param name="group">The object of ResearchGroup to be deleted.</param>
        Task DeleteGroupAsync(ResearchGroup group);

        /// <summary>
        /// Gets a Research Center by ID
        /// Author: Nelson Alvarez
        /// StoryID: ST-MM-3.2
        /// </summary>
        /// <param name="id">Research Center ID</param>
        /// <returns>Research Center</returns>     
        Task<ResearchCenter?> GetByIdAsync(int id);

        /// <summary>
        /// Gets a Enumerable class of Research Groups by Center ID and do pagination infrastructure logic
        /// Author: Nelson Alvarez
        /// StoryID: ST-MM-3.2
        /// </summary>
        /// <param name="centerId">Reserach Center ID</param>
        /// <param name="currentPage">Page currently displayed</param>
        /// <param name="size">Size of pagination</param>
        /// <returns>List of Research Groups</returns>
        Task<IEnumerable<ResearchGroup>?> GetGroupsPaged(int idCenter, int currentPage, int size);
        
        /// <summary>
        /// Gets Research Groups found in a Center count
        /// Author: Nelson Alvarez
        /// StoryID: ST-MM-3.2
        /// </summary>
        /// <param name="centerId">Center ID</param>
        /// <returns>Research groups count</returns>
        Task<int> GetGroupsCount(int idCenter);

        /// <summary>
        /// Gets a Enumerable class of Research Groups by Center ID 
        /// and search term. Does pagination infrastructure logic
        /// Author: Nelson Alvarez
        /// StoryID: ST-MM-3.2
        /// </summary>
        /// <param name="centerId">Reserach Center ID</param>
        /// <param name="currentPage">Page currently displayed</param>
        /// <param name="size">Size of pagination</param>
        /// <param name="term">Search term</param>
        /// <returns>List of Research Groups</returns>
        Task<IEnumerable<ResearchGroup>?> GetGroupsByTermPaged(int idCenter, int currentPage, int size, string term);

        /// <summary>
        /// Gets a Enumerable class of All Research Groups (Active/Inactive) by Center ID 
        /// and search term. Does pagination infrastructure logic
        /// Author: Nelson Alvarez
        /// StoryID: ST-MM-3.2
        /// </summary>
        /// <param name="centerId">Reserach Center ID</param>
        /// <param name="currentPage">Page currently displayed</param>
        /// <param name="size">Size of pagination</param>
        /// <param name="term">Search term</param>
        /// <returns>List of Research Groups</returns>
        Task<IEnumerable<ResearchGroup>?> GetAllGroupsByTermPaged(int idCenter, int currentPage, int size, string term);
        /// <summary>
        /// Gets Research All Groups (Active/Inactive) found in a Center and matching search term count
        /// Author: Nelson Alvarez
        /// StoryID: ST-MM-3.2
        /// </summary>
        /// <param name="centerId">Center ID</param>
        /// <param name="term">Search term</param>
        /// <returns>Research groups count</returns>
        Task<int> GetAllGroupsByTermCount(int idCenter, string term);

        /// <summary>
        /// Gets Research Groups found in a Center and matching search term count
        /// Author: Nelson Alvarez
        /// StoryID: ST-MM-3.2
        /// </summary>
        /// <param name="centerId">Center ID</param>
        /// <param name="term">Search term</param>
        /// <returns>Research groups count</returns>
        Task<int> GetGroupsByTermCount(int idCenter, string term);

        /// <summary>
        /// Gets Research Groups found in a Center and matching search term and research area list.
        /// Author: Roberto Méndez
        /// StoryID: ST-MM-5.1
        /// </summary>
        /// <param name="centerId">Center ID</param>
        /// <param name="currentPage">Page currently displayed</param>
        /// <param name="size">Size of pagination</param>
        /// <param name="term">Search term</param>
        /// <param name="area">Research area list</param>
        /// <returns>Research groups count</returns>
        Task<IEnumerable<ResearchGroup>?> GetGroupsByAreaListAndTermPaged(int centerId, int currentPage, int size, HashSet<ResearchArea> area, string term);

        /// <summary>
        /// Gets Research Groups found in a Center and matching search term and research area list count
        /// Author: Roberto Méndez
        /// StoryID: ST-MM-5.1
        /// </summary>
        /// <param name="centerId">Center ID</param>
        /// <param name="term">Search term</param>
        /// <param name="area">Research area list</param>
        /// <returns>Research groups count</returns>
        Task<int> GetGroupsByAreaListAndTermCount(int centerId, HashSet<ResearchArea> area, string term);

        Task<IEnumerable<ResearchGroup>?> GetGroupsByAreaListPaged(int centerId, int currentPage, int size, HashSet<ResearchArea> area);

        /// <summary>
        /// Gets Research Groups count of groups found in a Center and matching research area list.
        /// Author: Roberto Méndez
        /// StoryID: ST-MM-5.1
        /// </summary>
        /// <param name="centerId">Center ID</param>
        /// <param name="area">Research area list</param>
        /// <returns>Research groups count</returns>
        Task<int> GetGroupsByAreaListCount(int centerId, HashSet<ResearchArea> area);

        Task<IEnumerable<ResearchGroup>?> GetActiveGroupsPaged(int centerId, int currentPage, int size);

        Task<int> GetActiveGroupsCount(int idCenter);

        Task<IList<ResearchGroup>> GetAllGroups();

    }
}
