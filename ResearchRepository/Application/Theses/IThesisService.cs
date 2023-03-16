using ResearchRepository.Domain.Theses.Entities;
using ResearchRepository.Domain.Theses.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Application.Theses
{
    public interface IThesisService
    {
        /// <summary>
        /// Get all the theses
        /// </summary>
        /// Author: 
        /// StoryID: 
        /// <returns>All the theses </returns>
        Task<IEnumerable<ThesisDTO>> GetAllAsync();
        Task<Thesis?> GetByIdAsync(int id);
        Task<int> GetThesesCountAsync(int idGroup);
        Task<IEnumerable<Thesis>?> GetThesesByTermPagedAsync(int currentPage, int size, string term, int idGroup);

        /// <summary>
        /// Returns an specific thesis given a name 
        /// </summary>
        /// Author: Sofia Campos
        /// StoryID: ST-HC-1.24
        /// <param name="thesis">An object of Thesis</param>
        /// <returns> Task completed </returns>
        Task<Thesis?> GetByNameAsync(string name, DateTime publicationDate);

        /// <summary>
        /// Get the total number of groups given a search term
        /// </summary>
        /// Author: 
        /// StoryID: 
        /// <param name="idGroup">ID of the group</param>
        /// <param name="term">Search term to use in the query</param>
        /// <returns>Total number of theses given the search term</returns>
        Task<int> GetThesesByTermCountAsync(string term, int idGroup);
        Task<IEnumerable<Thesis>?> GetThesesPagedAsync(int currentPage, int size, int idGroup);


        /// <summary>
        /// Get all thesesDTOs of a group given the Group ID.
        /// </summary>
        /// Author: Josué Vargas
        /// StoryID: ST-MM-2.4
        /// <param long="idGroup">Id of the group</param>
        /// <returns> Task completed </returns>
        Task<IEnumerable<ThesisDTO>?> GetThesesByGroupIDAsync(long idGroup);

        /// <summary>
        /// Store an specific project part of a thesis. 
        /// </summary>
        /// Author: Sofia Campos 
        /// StoryID: ST-HC-1.17
        /// <param ThesisPartOfProject="thesisPartOfProject">An object ThesisPartOfProject</param>
        /// <returns> Task completed </returns>
        Task SaveProjectPartOfThesisAsync(ThesisPartOfProject thesisPartOfProject);

        /// <summary>
        /// Store an specific thesis. 
        /// </summary>
        /// Author: Sofia Campos
        /// StoryID: ST-HC-1.8
        /// <param name="thesis">An object of Thesis</param>
        /// <returns> Task completed </returns>
        Task AddThesisAsync(Thesis thesis);

        /// <summary>
        /// Delete an specific thesis
        /// </summary>
        /// Author: Steven Nuñez
        /// StoryID: ST-HC-1.10
        /// <param name="id">Id of the thesis that is going to be deleted</param>
        /// <returns> Task completed </returns>
        Task DeleteThesis(int id);

        /// <summary>
        /// Delete an specific project part of a thsis
        /// </summary>
        /// Author: Sofia Campos
        /// StoryID: ST-HC-1.17
        /// <param name="id">Id of the project</param>
        /// <returns> Task completed </returns>
        Task DeleteProjectPartOfThesis(int id);

        /// <summary>
        /// Delete an specific thesis part of a project
        /// </summary>
        /// Author: Steven Nuñez
        /// StoryID: ST-HC-1.16
        /// <param name="id">Id of the thesis</param>
        /// <returns> Task completed </returns>
        Task DeleteThesisPartOfProject(int id);

        /// <summary>
        /// Get all the thesis of a project. 
        /// </summary>
        /// Author: Sebastian Gonzalez
        /// StoryID: ST-HC-2.2
        /// <param name="projectId">An id of a project</param>
        /// <returns> List of all thesis of a project </returns>
        Task<IList<ThesisPartOfProject>> GetAsyncThesisPartOfProjectFromId(int projectId);

        /// <summary>
        /// This method return an list of projects of an specific thesis
        /// </summary>
        /// <param name = "thesisId" > thesis id</param>
        /// Author: Sofia Campos
        /// StoryID: ST-HC-1.17
        /// <returns>Return an list of projects of a specific thesis</returns>
        Task<IList<ThesisPartOfProject>> GetAsyncProjectsPartOfThesisFromId(int thesisId);

        /// <summary>
        /// Get all the thesis of a project. 
        /// </summary>
        /// Author: Sebastian Gonzalez
        /// StoryID: ST-HC-2.2
        /// <param name ="_thesis">An id of a project</param>
        /// <returns> List of all thesis of a project </returns>
        Task<IList<ThesisPartOfProject>> GetThesisOfProjectById(IList<ThesisPartOfProject> _thesis);

        /// <summary>
        /// Updates a specific thesis.
        /// </summary>
        /// Author: Gabriel Revillat Zeledon
        /// StoryID: ST-HC-1.9
        /// <param name="id">New id to update the thesis.</param>
        /// <param name="name">New name to update the thesis.</param>
        /// <param name="publicationDate">New date to update the thesis.</param>
        /// <param name="summary">New summary to update the thesis.</param>
        /// <param name="groupId">New groupId to update the thesis.</param>
        /// <param name="image">New image to update the thesis.</param>
        /// <param name="doi">New DOI to update the thesis.</param>
        /// <param name="type">New type to update the thesis.</param>
        /// <returns>Task completed.</returns>
        Task UpdateThesisAsync(int id, string name, DateTime publicationDate,
                               string summary, long groupId, String image,
                               String doi, String type, String reference,
                               byte[]? attachment, String? attachmentName);

        int GetLastThesisId();

        /// <summary>
        /// This method return an list of thesis corresponding to a list of ids
        /// </summary>
        /// <param name = "ids" > list of ids</param>
        /// Author: Andrea Alvarado
        /// StoryID: ST-PA-3.11
        /// <returns>Return an list of thesis</returns>
        Task<IList<Thesis>> GetThesesFromIds(IList<int> ids);
        /// <summary>
        /// This method return an list of projects of a specific thesis id
        /// </summary>
        /// <param name = "thesisId" > project id</param>
        /// Author: Sebastian Gonzalez
        /// StoryID: ST-HC-1.26
        /// <returns>Return an list of project of a specific thesis</returns>
        Task<IList<int>> GetProjectFromThesisId(int thesisId);

        /// <summary>
        /// Gets a thesis and changes its state based on the given parameters.
        /// </summary>
        /// Author: Gabriel Revillat Zeledón
        /// StoryID: ST-HC-1.33
        /// <param name="thesisId">
        /// ID of the thesis whose state is to be changed.
        /// </param>
        /// <param name="state">
        /// New state of project. True if active, false otherwise.
        /// </param>
        /// <returns>The completed task.</returns>
        Task ChangeThesisState(int thesisId, bool state);

        /// <summary>
        /// Returns the number of active theses in a specific research group.
        /// </summary>
        /// Author: Gabriel Revillat Zeledón
        /// StoryID: ST-HC-1.33
        /// <param name="groupId">Group ID to which the thesis belongs.</param>
        /// <returns>The number of active theses.</returns>
        Task<int> GetActiveThesesCountAsync(int groupId);

        /// <summary>
        /// Returns a paged list of active theses.
        /// </summary>
        /// Author: Gabriel Revillat Zeledón
        /// StoryID: ST-HC-1.33
        /// <param name="currentPage">The current page number.</param>
        /// <param name="size">The number of items per page.</param>
        /// <param name="groupId">Group ID to which the thesis belongs.</param>
        /// <returns>A paged list of active theses.</returns>
        Task<IEnumerable<Thesis>?> GetActiveThesesPagedAsync(int currentPage, int size,
                                                             int groupId);

        /// <summary>
        /// This method return an list of subareas of a specific thesis id
        /// </summary>
        /// <param name = "id" > thesis id</param>
        /// Author: Sebastian Gonzalez
        /// StoryID: ST-HC-1.26
        /// <returns>Return an list of subareas of a specific thesis</returns>
        Task<IList<ResearchAreaThesis>> GetAssociatedAreas(int id);
        /// <summary>
        /// This method return a subarea of a specific thesis id
        /// </summary>
        /// <param name = "thesisId" > thesis id</param>
        /// Author: Sebastian Gonzalez
        /// StoryID: ST-HC-1.26
        /// <returns>Return a subarea of a specific thesis</returns>
        Task<ResearchAreaThesis> GetResearchAreaThesisAssociation(int thesisId, int areaId);
        /// <summary>
        /// This method add a subarea of to a specific thesis id
        /// </summary>
        /// <param name = "association" > project id</param>
        /// Author: Sebastian Gonzalez
        /// StoryID: ST-HC-1.26
        /// <returns>Complete Task</returns>
        Task AddAssociatedArea(ResearchAreaThesis association);
        /// <summary>
        /// This method delete a subarea of to a specific thesis id
        /// </summary>
        /// <param name = "thesisId" > project id</param>
        /// Author: Sebastian Gonzalez
        /// StoryID: ST-HC-1.26
        /// <returns>Complete Task</returns>
        Task DeleteAssociatedArea(int thesisId, int areaId);
    }
}