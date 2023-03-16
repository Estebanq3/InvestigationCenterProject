using ResearchRepository.Domain.InvestigationProjects.Entities;
using ResearchRepository.Domain.InvestigationProjects.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Application.InvestigationProjects
{
    public interface IInvestigationProjectService
    {
        /// <summary>
        /// Get all the investigation projects
        /// </summary>
        /// Author: 
        /// StoryID: 
        /// <returns>All the investigation projects </returns>
        Task<IEnumerable<InvestigationProjectDTO>> GetAllAsync();
        Task<InvestigationProject?> GetByIdAsync(int id);
        Task<int> GetProjectsCountAsync(int idGroup);
        Task<IEnumerable<InvestigationProject>?> GetProjectsByTermPagedAsync(int currentPage, int size, string term, int idGroup);
        Task<IEnumerable<InvestigationProjectDTO>?> GetFirstProjects(int idGroup);

        /// <summary>
        /// Get the total number of groups given a search term
        /// </summary>
        /// Author: 
        /// StoryID: ST-HC-4.1 
        /// <param name="idGroup">ID of the group</param>
        /// <param name="term">Search term to use in the query</param>
        /// <returns>Total number of projects given the search term</returns>
        Task<int> GetProjectsByTermCountAsync(string term, int idGroup);
        Task<IEnumerable<InvestigationProject>?> GetProjectsPagedAsync(int currentPage, int size, int idGroup);

        /// <summary>
        /// Delete an specific investigation project
        /// </summary>
        /// Author: Oscar Navarro y Sebastian
        /// StoryID: ST-HC-1.7
        /// <param name="id">Id of the project that is going to be deleted</param>
        /// <returns> Task completed </returns>
        Task DeleteProject(int id);

        /// <summary>
        /// Add project to a the projects table on the database
        /// </summary>
        /// Author: Esteban Quesada
        /// StoryID: ST-HC-4.1 
        /// <param name="project"> Project entity to add</param>
        /// <returns>Task completed</returns>
        Task AddProjectAsync(InvestigationProject project);
        /// <summary>
        /// Add images to a the projects on the database
        /// </summary>
        /// Author: Esteban Quesada
        /// StoryID: ST-HC-1.35 
        /// <param name="projectImage"> ProjectsImage entity to add</param>
        /// <returns>Task completed</returns>
        Task AddImageAsync(ProjectsImages projectImage);

        /// <summary>
        /// Remove an image of a project on the databse
        /// </summary>
        /// Author: Oscar Navarro
        /// StoryID: ST-HC-1.27 
        /// <param name="url"> URL of the image</param>
        /// <param name="projectId"> Id of the project</param>
        /// <returns>Task completed</returns>
        Task RemoveImageAsync(string url,int projectId);

        /// <summary>
        /// Get the images of a project on the database
        /// </summary>
        /// Author: Oscar Navarro
        /// StoryID: ST-HC-2.22 
        /// <param name="id"> Id of the project</param>
        /// <returns>List of the images URLs</returns>
        Task<IList<ProjectsImages>> GetImagesAsync(int id);

        /// <summary>
        /// Update an investigation project to  on the database
        /// </summary>
        /// Author: Esteban Quesada and Oscar Navarro
        /// StoryID: ST-HC-1.6
        /// <param name="id"> Id of the project that is going to be updated</param>
        /// /// <param name="Name"> New name to be updated</param>
        /// /// <param name="StartDate"> New stardate to be updated</param>
        /// /// <param name="groupid"> Id of group of the project that is going to be updated</param>
        /// /// <param name="description"> Description name to be updated</param>
        /// /// /// <param name="summary"> New summary to be updated</param>
        /// /// /// <param name="image"> New image to be updated</param>
        /// <returns>Task completed</returns>
        Task UpdateProject(int id, string Name, DateTime StartDate, DateTime EndDate, int groupid, string description, string summary, string image);

        /// <summary>
        /// Get the max id 
        /// </summary>
        /// Author: Oscar Navarro
        /// StoryID: 4.1
        /// <returns>The max id of a project</returns>
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
        /// Returns an id of a project given a name 
        /// </summary>
        /// Author: Sofia Campos
        /// StoryID: ST-HC-1.24
        /// <param name="name">name of the project</param>
        /// <returns> Task completed </returns>
        Task<int> GetIDByNameAsync(string name);

        /// <summary>
        /// Gets a project and changes its state based on the given parameters.
        /// </summary>
        /// Author: Gabriel Revillat Zeledón
        /// StoryID: ST-HC-1.32
        /// <param name="projectId">
        /// ID of the project whose state is to be changed.
        /// </param>
        /// <param name="state">
        /// New state of project. True if active, false otherwise.
        /// </param>
        /// <returns>The completed task.</returns>
        Task ChangeProjectState(int projectId, bool state);

        /// <summary>
        /// Returns the number of active projects in a specific research group.
        /// </summary>
        /// Author: Gabriel Revillat Zeledón
        /// StoryID: ST-HC-1.32
        /// <param name="groupId">Group ID to which the project belongs.</param>
        /// <returns>The number of active projects.</returns>
        Task<int> GetActiveProjectsCountAsync(int groupId);

        /// <summary>
        /// Returns a paged list of active projects.
        /// </summary>
        /// Author: Gabriel Revillat Zeledón
        /// StoryID: ST-HC-1.32
        /// <param name="currentPage">The current page number.</param>
        /// <param name="size">The number of items per page.</param>
        /// <param name="groupId">Group ID to which the project belongs.</param>
        /// <returns>A paged list of active projects.</returns>
        Task<IEnumerable<InvestigationProject>?> GetActiveProjectsPagedAsync(int currentPage,
                                                                             int size,
                                                                             int groupId);
    }
}