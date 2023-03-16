using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.ResearchAreas.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;

namespace ResearchRepository.Application.ResearchAreas
{
    public interface IResearchAreaService
    {
        /// <summary>
        /// Get all research areas with their subareas
        /// </summary>
        /// Author: Nelson Alvarez
        /// <returns>All research areas</returns>
        Task<IEnumerable<ResearchArea>> GetResearchAreaAsync();
        /// <summary>
        /// Get a research area by id
        /// </summary>
        /// Author: Nelson Alvarez
        /// <param name="id">Unique identifier of the research area</param>
        /// <returns>Research area</returns>
        Task<ResearchArea?> GetResearchAreaByIdAsync(int id);
        /// <summary>
        /// Adds a group to a subarea
        /// </summary>
        /// Author: Nelson Alvarez
        /// <param name="researchArea">Subarea to get new group</param>
        /// <param name="researchGroup">Group to add</param>
        Task AddGroupToSubAreaAsync(ResearchArea researchArea, ResearchGroup researchGroup);
        /// <summary>
        /// Removes a group from a research subarea
        /// </summary>
        /// Author: Nelson Alvarez
        /// <param name="researchArea">research subarea to remove from</param>
        /// <param name="researchGroup">reserach group to remove</param>
        Task RemoveGroupFromSubAreaAsync(ResearchArea researchArea, ResearchGroup researchGroup);
        /// <summary>
        /// Deletes a research area or subarea
        /// </summary>
        /// Author: Nelson Alvarez
        /// <param name="researchArea">area or subarea to remove</param>
        Task DeleteResearchArea(ResearchArea researchArea);
        /// <summary>
        /// Adds a new research area or subarea
        /// </summary>
        /// Author: Nelson Alvarez
        /// <param name="researchArea">new research area</param>
        Task AddResearchArea(ResearchArea researchArea);
        /// <summary>
        /// Gets the subareas of a research area matching an ID
        /// </summary>
        /// Author: Nelson Alvarez
        /// <param name="id">ID to match research area</param>
        /// <returns>Research subareas</returns>
        Task<IEnumerable<ResearchArea>> GetSubAreaAsync(int id);

        /// <summary>
        /// This method return an list of subareas of a specific project id
        /// </summary>
        /// <param name = "id" > project id</param>
        /// Author: Steven Nuñez
        /// StoryID: ST-HC-2.25
        /// <returns>Return an list of subareas of a specific project</returns>
        Task<IList<ResearchAreaProject>> GetAssociatedAreas(int id);

        /// <summary>
        /// This method return a subarea of a specific project id
        /// </summary>
        /// <param name = "projectId" > project id</param>
        /// Author: Steven Nuñez
        /// StoryID: ST-HC-2.25
        /// <returns>Return a subarea of a specific project</returns>
        Task<ResearchAreaProject> GetResearchAreaThesisAssociation(int projectId, int areaId);

        /// <summary>
        /// This method delete a subarea of to a specific project id
        /// </summary>
        /// <param name = "projectId" > project id</param>
        /// Author: Steven Nuñez
        /// StoryID: ST-HC-1.37
        /// <returns>Complete Task</returns>
        Task DeleteAssociatedArea(int projectId, int areaId);

        /// <summary>
        /// This method add a subarea of to a specific project id
        /// </summary>
        /// <param name = "association" > project id</param>
        /// Author: Steven Nuñez
        /// StoryID: ST-HC-1.36
        /// <returns>Complete Task</returns>
        Task AddAssociatedArea(ResearchAreaProject association);
    }
}
