using System.Collections.Generic;
using System.Threading.Tasks;

using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.InvestigationProjects.Entities;
using ResearchRepository.Domain.InvestigationProjects.DTOs;

namespace ResearchRepository.Domain.InvestigationProjects.Repositories
{
    public interface IInvestigationProjectsRepository : IRepository<InvestigationProject>
    {
        Task<IEnumerable<InvestigationProjectDTO>> GetAllAsync();
        Task<InvestigationProject?> GetByIdAsync(int id);
        Task<int> GetProjectsCount(int idGroup);
        Task<IEnumerable<InvestigationProject>?> GetProjectsByTermPaged(int currentPage, int size, string term, int idGroup);
        Task<int> GetProjectsByTermCount(string term, int idGroup);
        
        Task<IEnumerable<InvestigationProject>?> GetProjectsPaged(int currentPage, int size,int idGroup);
        Task<IEnumerable<InvestigationProjectDTO>?> GetFirstProjects(int idGroup);
        Task SaveAsync(InvestigationProject project);
        Task SaveImageAsync(ProjectsImages projectImage);
        Task RemoveImageAsync(string url, int projectId);
        Task<IList<ProjectsImages>> GetImagesAsync(int id);

        Task UpdateAsync(int id, string Name, System.DateTime StartDate, System.DateTime EndDate, int groupid, string description, string summary, string image);

        /// <summary>
        /// Delete an specific investigation project
        /// </summary>
        /// Author: Oscar Navarro y Sebastian
        /// StoryID: ST-HC-1.7
        /// <param name="id">Id of the project that is going to be deleted</param>
        /// <returns> Task completed </returns>
        Task DeleteProject(int id);

        /// <summary>
        /// Returns an id of a project given a name 
        /// </summary>
        /// Author: Sofia Campos
        /// StoryID: ST-HC-1.24
        /// <param name="name">name of the project</param>
        /// <returns> Task completed </returns>
        Task<int> GetIDByNameAsync(string name);

        int GetLastProjectId();

        /// <summary>
        /// Return a list of projects of a specific collaborator
        /// </summary>
        /// Author: Carlos Mora Membreño
        /// StoryID: ST-PA-3.8
        /// <param name="ids">List of ids projects</param>
        /// <returns> List of projects </returns>
        Task<IList<InvestigationProject>> GetProjectsFromId(IList<int> ids);

        /// <summary>
        /// Returns the number of active projects in a specific research group.
        /// </summary>
        /// Author: Gabriel Revillat Zeledón
        /// StoryID: ST-HC-1.32
        /// <param name="groupId">Group ID to which the project belongs.</param>
        /// <returns>The number of active projects.</returns>
        Task<int> GetActiveProjectsCount(int groupId);

        /// <summary>
        /// Returns a paged list of active projects.
        /// </summary>
        /// Author: Gabriel Revillat Zeledón
        /// StoryID: ST-HC-1.32
        /// <param name="currentPage">The current page number.</param>
        /// <param name="size">The number of items per page.</param>
        /// <param name="groupId">Group ID to which the project belongs.</param>
        /// <returns>A paged list of active projects.</returns>
        Task<IEnumerable<InvestigationProject>?> GetActiveProjectsPaged(int currentPage,
                                                                        int size,
                                                                        int groupId);
    }
}