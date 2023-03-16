using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.ResearchAreas.Entities;
using ResearchRepository.Domain.Core.Repositories;

namespace ResearchRepository.Domain.ResearchAreas.Repositories
{
    public interface IResearchAreaRepository : IRepository<ResearchArea>
    {
        /// <summary>
        /// Save changes made to a research area and its related entities
        /// </summary>
        /// Author: Nelson Alvarez
        /// <param name="researchArea">research area to save</param>
        Task SaveAsync(ResearchArea researchArea);
        /// <summary>
        /// Get all research areas
        /// </summary>
        /// Author: Nelson Alvarez
        /// <returns>Research areas enumerable</returns>
        Task<IEnumerable<ResearchArea>> GetAllAsync();
        /// <summary>
        /// Get a research area or subarea by Id
        /// </summary>
        /// Author: Nelson Alvarez
        /// <param name="id">Unique identifier of research areas</param>
        /// <returns>Research area</returns>
        Task<ResearchArea?> GetByIdAsync(int id);
        /// <summary>
        /// Removes a research area or subarea from the database
        /// </summary>
        /// Author: Nelson Alvarez
        /// <param name="researchArea">research area to remove</param>
        Task DeleteResearchArea(ResearchArea researchArea);
        /// <summary>
        /// Get subareas of a research area with a given id
        /// </summary>
        /// Author: Nelson Alvarez
        /// <param name="id">Unique identifier of research area</param>
        /// <returns>Enumerable of research subareas</returns>
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
        Task<ResearchAreaProject> GetResearchAreaProjectAssociation(int projectId, int areaId);

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
