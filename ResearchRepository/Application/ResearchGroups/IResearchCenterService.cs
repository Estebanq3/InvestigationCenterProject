using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.ResearchAreas.Entities;
using ResearchRepository.Domain.ResearchGroups.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Application.ResearchGroups
{
    public interface IResearchCenterService
    {
        /// <summary>
        /// Adds the ResearchGroup object to the database.
        /// </summary>
        /// Author: Dean Vargas (Monkey Madness)
        /// StoryID: ST-MM-1.1
        /// <param name="group"></param>
        /// <returns></returns>
        Task CreateGroupAsync(ResearchGroup group);
        /// <summary>
        /// Deletes the ResearchGroup object from the database.
        /// </summary>
        /// Author: Dean Vargas (Monkey Madness)
        /// StoryID: ST-MM-1.2
        /// <param name="group"></param>
        /// <returns></returns>
        Task DeleteGroupAsync(ResearchGroup group);
        /// <summary>
        /// Get all the Research Centers
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-3.2
        /// <returns>All research centers</returns>
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
        /// Get a Research Center given the ID of that center
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-3.2
        /// <param name="id">Unique identifier of the center</param>
        /// <returns>A Reasearch Center</returns>
        Task<ResearchCenter?> GetByIdAsync(int id);

        /// <summary>
        /// Get the groups by current page associated to a Research Center
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-3.2
        /// <param name="idCenter">ID of the Research Center</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="size">Number of Groups to retreive per page</param>
        /// <returns>List of groups given the page and the size</returns>
        Task<IEnumerable<ResearchGroup>?> GetGroupsPagedAsync(int idCenter, int currentPage, int size);

        /// <summary>
        /// Get the total number to groups associated to a Research Center
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-3.2
        /// <param name="idCenter">ID of the center</param>
        /// <returns>Total number of groups</returns>
        Task<int> GetGroupsCountAsync(int idCenter);
        Task<int> GetActiveGroupsCountAsync(int idCenter);
        /// <summary>
        /// Get a list of groups by current page and search term, associated to a Research Center
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-3.2
        /// <param name="idCenter">ID of the Research Center</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="size">Number of Groups to retreive per page</param>
        /// <param name="term">Search term to use in the query</param>
        /// <returns>List of groups given the page and the size and search term</returns>
        Task<IEnumerable<ResearchGroup>?> GetGroupsByTermPagedAsync(int idCenter, int currentPage, int size, string term);

        /// <summary>
        /// Get the total number to groups associated to a Research Center given a search term
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-3.2
        /// <param name="idCenter">ID of the center</param>
        /// <param name="term">Search term to use in the query</param>
        /// <returns>Total number of groups given the search term</returns>
        Task<int> GetGroupsByTermCountAsync(int idCenter, string term);

        /// <summary>
        /// Get groups associated to a Research Center given a search term and a list of research areas.
        /// </summary>
        /// Author: Roberto Mendez
        /// StoryID: ST-MM-5.1
        /// <param name="centerId">ID of the center</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="size">Number of Groups to retreive per page</param>
        /// <param name="term">Search term to use in the query</param>
        /// <param name="area">List of areas to use in the query</param>
        /// <returns>Total number of groups given the search term and list of areas</returns>
        Task<IEnumerable<ResearchGroup>?> GetGroupsByAreaListAndTermPaged(int centerId, int currentPage, int size, HashSet<ResearchArea> area, string term);

        /// <summary>
        /// Get the total number to groups associated to a Research Center given a search term and a list of research areas.
        /// </summary>
        /// Author: Roberto Mendez
        /// StoryID: ST-MM-5.1
        /// <param name="centerId">ID of the center</param>
        /// <param name="term">Search term to use in the query</param>
        /// <param name="area">List of areas to use in the query</param>
        /// <returns>Total number of groups given the search term and list of research areas</returns>
        Task<int> GetGroupsByAreaListAndTermCount(int centerId, HashSet<ResearchArea> area, string term);

        /// <summary>
        /// Get groups associated to a Research Center given a list of research areas.
        /// </summary>
        /// Author: Roberto Mendez
        /// StoryID: ST-MM-5.1
        /// <param name="centerId">ID of the center</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="size">Number of Groups to retreive per page</param>
        /// <param name="area">List of areas to use in the query</param>
        /// <returns>Total number of groups given the search term and list of areas</returns>
        Task<IEnumerable<ResearchGroup>?> GetGroupsByAreaListPaged(int centerId, int currentPage, int size, HashSet<ResearchArea> area);

        /// <summary>
        /// Get the total number to groups associated to a Research Center given a list of research areas.
        /// </summary>
        /// Author: Roberto Mendez
        /// StoryID: ST-MM-5.1
        /// <param name="centerId">ID of the center</param>
        /// <param name="area">List of areas to use in the query</param>
        /// <returns>Total number of groups given the list of research areas</returns>
        Task<int> GetGroupsByAreaListCount(int centerId, HashSet<ResearchArea> area);

        Task<IEnumerable<ResearchGroup>?> GetActiveGroupsPaged(int centerId, int currentPage, int size);

        /// <summary>
        /// Get a list of groups (active/inactive) by current page and search term, associated to a Research Center
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-1.1
        /// <param name="idCenter">ID of the Research Center</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="size">Number of Groups to retreive per page</param>
        /// <param name="term">Search term to use in the query</param>
        /// <returns>List of groups given the page and the size and search term</returns>
        Task<IEnumerable<ResearchGroup>?> GetAllGroupsByTermPagedAsync(int idCenter, int currentPage, int size, string term);

        /// <summary>
        /// Get the total number (active/inactive) to groups associated to a Research Center given a search term
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-1.1
        /// <param name="idCenter">ID of the center</param>
        /// <param name="term">Search term to use in the query</param>
        /// <returns>Total number of groups given the search term</returns>
        Task<int> GetAllGroupsByTermCountAsync(int idCenter, string term);

        Task<IList<ResearchGroup>> GetAllGroups();
    }
}
