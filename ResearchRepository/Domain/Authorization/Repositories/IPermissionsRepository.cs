using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ResearchRepository.Domain.Authorization.Repositories
{
    public interface IPermissionsRepository
    {

        /// <summary>
        /// Says if the current user has a specific claim (permission).
        /// </summary>
        /// Author: Andrea Alvarado
        /// StoryID: ST-PA-1.1, ST-PA-1.2, ST-PA-1.3, ST-PA-1.4, ST-PA-1.5
        /// <param name="claim">the name of the claim (permission)</param>
        /// <returns>Whether the user has the permission or not</returns>
        Task<bool> AuthorizeClaim(string claim);

        /// <summary>
        /// Says if the current user belongs to a specific group.
        /// </summary>
        /// Author: Andrea Alvarado
        /// StoryID: ST-PA-1.1, ST-PA-1.2, ST-PA-1.3, ST-PA-1.4, ST-PA-1.5
        /// <param name="groupId">The id of the group.</param>
        /// <returns>Whether the user belongs to the group or not</returns>
        Task<bool> AuthorizeGroup(int groupId);

        /// <summary>
        /// Returns a list of booleans that indicates which groups a person is the administrator of
        /// </summary>
        /// Author: Andrea Alvarado
        /// StoryID: ST-PA-1.12
        /// <param name="groupCount">The amount of groups in the system</param>
        /// <param name="email">The email of the person</param>
        /// <returns>List of booleans</returns>
        Task<IList<bool>> AdministratorOfGroupBoolList(int groupCount, string email);

        /// <summary>
        /// Returns a list of booleans that indicates which groups a person is the collaborator of
        /// </summary>
        /// Author: Andrea Alvarado
        /// StoryID: ST-PA-1.12
        /// <param name="groupCount">The amount of groups in the system</param>
        /// <param name="email">The email of the person</param>
        /// <returns>List of booleans</returns>
        Task<IList<bool>> CollaboratorOfGroupBoolList(int groupCount, string email);

        /// <summary>
        /// Associates or disassociates a person to the group administrator role in a specific group
        /// </summary>
        /// Author: Andrea Alvarado
        /// StoryID: ST-PA-1.12
        /// <param name="email">The email of the person</param>
        /// <param name="groupId">The id of the group</param>
        /// <param name="assign">Says if the person should be Associated or disassociated</param>
        Task changeAdmiGroup(string email, int groupId, bool assign);

        /// <summary>
        /// Associates or disassociates a person to the group collaborator role in a specific group
        /// </summary>
        /// Author: Andrea Alvarado
        /// StoryID: ST-PA-1.12
        /// <param name="email">The email of the person</param>
        /// <param name="groupId">The id of the group</param>
        /// <param name="assign">Says if the person should be Associated or disassociated</param>
        Task changeColabGroup(string email, int groupId, bool assign);


    }
}
