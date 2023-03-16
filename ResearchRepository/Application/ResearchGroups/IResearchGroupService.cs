using ResearchRepository.Domain.ResearchGroups.DTOs;
using ResearchRepository.Domain.ResearchGroups.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Application.ResearchGroups
{
    public interface IResearchGroupService
    {
        /// <summary>
        /// Get the research group given the ID of that research group
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-3.2
        /// <param name="idCenter">ID of the research Group</param>
        /// <returns>Research groups given the ID</returns>
        Task<ResearchGroup> GetById(int idCenter);

        /// <summary>
        /// Get the total amount of groups in the database
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-3.2
        /// <returns>Number of groups</returns>
        Task<int> GetCountAsync();

        /// <summary>
        /// Get a group given the id and change the state
        /// </summary>
        /// <param name="idGroup">ID of the group to modify</param>
        /// <param name="state">Active = true, Inactive = False</param>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-1.6
        /// <returns></returns>
        Task ChangeStateGroup(int idGroup, bool state);
    }
}
