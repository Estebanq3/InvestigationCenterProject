using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.ResearchGroups.DTOs;
using ResearchRepository.Domain.ResearchGroups.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Domain.ResearchGroups.Repositories
{
    public interface IResearchGroupRepository : IRepository<ResearchGroup>
    {
        /// <summary>
        /// Save the Group object.
        /// </summary>
        /// Author: Rodrigo Contreras
        /// StoryID: ST-MM-6.1
        /// <param name="group"></param>
        /// <returns></returns>
        Task SaveAsync(ResearchGroup group);

        /// <summary>
        /// Gets a Research Group by ID
        /// Author: Nelson Alvarez
        /// StoryID: ST-MM-3.2
        /// </summary>
        /// <param name="id">Research Group ID</param>
        /// <returns>Research Group</returns>
        Task<ResearchGroup> GetById(int id);

        /// <summary>
        /// Gets count of Research Groups in the database
        /// Author: Nelson Alvarez
        /// StoryID: ST-MM-3.2
        /// </summary>
        /// <returns>Research groups count</returns>
        Task<int> GetCountAsync();
    }
}
