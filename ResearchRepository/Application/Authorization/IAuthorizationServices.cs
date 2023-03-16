using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ResearchRepository.Application.Authorization
{
    public interface IAuthorizationServices
    {
        /// <summary>
        /// Adds the default roles established into the database (Administrador, Administrador De Centro, Administrador de Grupo)
        /// </summary>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-1.5, ST-PA-1.2, ST-PA-1.3
        /// <returns> Task completed </returns>
        Task configureRoles();

        /// <summary>
        /// Assigns a role to a specified user
        /// </summary>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-1.5, ST-PA-1.2, ST-PA-1.3, ST-PA-3.8
        /// <param name="email">The email of the user thats gonna be assigned the role</param>
        /// <param name="role">The name of the role that is going to be assigned to the user</param>
        /// <returns> Task completed </returns>
        Task assignRole(string email, string role);

        /// <summary>
        /// Removes a role from a specified user
        /// </summary>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-1.5, ST-PA-1.2, ST-PA-1.3, ST-PA-3.8
        /// <param name="email">The email of the user thats gonna be removed from the role</param>
        /// <param name="role">The name of the role that is going to be removed from the user</param>
        /// <returns> Task completed </returns>
        Task removeRole(string email, string role);

        /// <summary>
        /// Switches the state of the user in a specified role. If the user is part of the role and this Task is called,
        /// the role is removed from the user. If the user isn't part of the role and this Task is called, the user is added.
        /// </summary>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-1.5, ST-PA-1.2, ST-PA-1.3, ST-PA-3.8
        /// <param name="email">The email of the user thats gonna be assigned/removed from the role</param>
        /// <param name="role">The name of the role that is going to be assigned/removed from the user</param>
        /// <returns> Task completed </returns>
        Task switchRoleState(string email, string role, bool isPart);

        /// <summary>
        /// Verifies if a user is part of a role.
        /// </summary>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-1.5, ST-PA-1.2, ST-PA-1.3, ST-PA-3.8
        /// <param name="email">The email of the user thats gonna be verified</param>
        /// <param name="role">The name of the role that is going to be verified</param>
        /// <returns> A boolean: TRUE if the user is part of the specified role, false otherwise </returns>
        Task<bool> isPartOfRole(string email, string role);

        /// <summary>
        /// Gets a list of the users registered that match the searched string
        /// </summary>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-1.5, ST-PA-1.2, ST-PA-1.3, ST-PA-3.8
        /// <param name="searched">the string that is going to be used to match the users found</param>
        /// <returns> A List of Users whose emails match the searched string </returns>
        Task<IList<IdentityUser>> getUsersBySearch(string searched);
        //gets
        Task<IList<IdentityUser>> getUsers();
        Task<IList<string>> getRoles();

        /// <summary>
        /// Returns a list with the names of the roles a user belongs to
        /// </summary>
        /// Author: Andrea Alvarado
        /// StoryID: ST-PA-1.14
        /// <param name="email">user's email</param>
        /// <returns> The List of the roles the person belongs to </returns>
        Task<IList<string>> getUserRoles(string email);

        /// <summary>
        /// Asign permanent claim(s) to the role "Administrador"
        /// </summary>
        /// Author: Andrea Alvarado
        /// StoryID: ST-PA-1.5
        Task SeedClaimsForSuperAdministrator();

        /// <summary>
        /// Adds all the claims to a role
        /// </summary>
        /// Author: Andrea Alvarado
        /// StoryID: ST-PA-1.1, ST-PA-1.2, ST-PA-1.3, ST-PA-1.4, ST-PA-1.5
        /// <param name="role">the role that the claims are going to be added to</param>
        Task AddPermissionClaim(IdentityRole role);

        /// <summary>
        /// Returns a list with all of the claims´ names
        /// </summary>
        /// Author: Andrea Alvarado
        /// StoryID: ST-PA-1.1, ST-PA-1.2, ST-PA-1.3, ST-PA-1.4, ST-PA-1.5
        /// <returns>A list with all of the claims´ names</returns>
        List<string> getAllClaims();

        /// <summary>
        /// Returns a list of booleans corresponding to the claims a role has.
        /// </summary>
        /// Author: Andrea Alvarado
        /// StoryID: ST-PA-1.1, ST-PA-1.2, ST-PA-1.3, ST-PA-1.4, ST-PA-1.5
        /// <param name="roleName">the name of the role</param>
        /// <returns>List of booleans corresponding to the claims the role has.</returns>
        Task<List<bool>> getboolRoleClaims(string roleName);

        /// <summary>
        /// Updates the claims that a role has
        /// </summary>
        /// Author: Andrea Alvarado
        /// StoryID: ST-PA-1.1, ST-PA-1.2, ST-PA-1.3, ST-PA-1.4, ST-PA-1.5
        /// <param name="roleName">The name of the role</param>
        /// <param name="selectedClaims">The claims that are going to be added to the role</param>
        Task Update(string roleName, List<string> selectedClaims);
    }
}
