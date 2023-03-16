using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResearchRepository.Domain.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using ResearchRepository.Domain.Authentication.ValueObjects;
using System.Security.Claims;

namespace ResearchRepository.Domain.Authentication.Repositories
{
    public interface IAuthenticationRepository
    {
        /// <summary>
        /// Registers a new user
        /// </summary>
        /// Author: Dylan Arias
        /// StoryID: ST-PA-4.2
        /// <param name="r">The register object that handles the register information</param>
        /// <returns> Bool indicating if operartion was successful. </returns>
        Task<bool> RegisterRequestAsync(Register r);

        /// <summary>
        /// Requests the sign in for a user
        /// </summary>
        /// Author: Dylan Arias
        /// StoryID: ST-PA-4.3
        /// <param name="r">The register object that handles the sign in information</param>
        /// <param name="isPersistent">Indicates is user wants to stay signed in after leaving</param>
        /// <returns> Bool indicating if operartion was successful </returns>
        Task<bool> SignInRequestAsync(Register r, bool isPersistent);

        /// <summary>
        /// Verifies if the user is signed in to an account
        /// </summary>
        /// Author: Dylan Arias
        /// StoryID: ST-PA-4.2, ST-PA-4.4, ST-PA-4.7
        /// <param name="user">The claim that verifies whether the user is signed in or not</param>
        /// <returns> Bool: TRUE if the user is signed in, FALSE if otherwise </returns>
        bool IsSignedIn(ClaimsPrincipal user);

        /// <summary>
        /// Logs out of the current account
        /// </summary>
        /// Author: Greivin Sanchez
        /// StoryID: ST-PA-4.2, ST-PA-4.4, ST-PA-4.7
        /// <returns> The completed task </returns>
        Task SignOut();
        /// <summary>
        /// Makes the HTTP request to sign in the user
        /// </summary>
        /// Author: Dylan Arias
        /// StoryID: ST-PA-4.3
        /// <param name="token">Token requiered for sign in</param>
        /// <param name="isPersistent">Indicates is user wants to stay signed in after leaving</param>
        /// <returns> Bool indicating if operartion was successful </returns>
        Task<bool> SignInInternalAsync(string token,bool isPersistent);

        /// <summary>
        /// Changes the password of the current user
        /// </summary>
        /// Author: Sebastian Montero
        /// StoryID: ST-PA-4.1, ST-PA-4.2, ST-PA-4.4, ST-PA-4.7
        /// <param name="user">The user object containing all the information regarding the account</param>
        /// <param name="oldPassword">The current password that wants to be changed</param>
        /// <param name="newPassword">The new password</param>
        /// <returns> Bool: TRUE if the password is changes successfully, false otherwise </returns>
        Task<bool> ChangePassword(IdentityUser user, string oldPassword, string newPassword);

        /// <summary>
        /// Encrypts a string of data using a key
        /// </summary>
        /// Author: Greivin Sanchez
        /// StoryID: ST-PA-4.1, ST-PA-4.6
        /// <param name="data">the string that is going to be encrypted</param>
        /// <param name="key">the string that is going to be used as key to encrypt data</param>
        /// <returns> the encrypted string </returns>
        string EncryptString(string data, string key);

        /// <summary>
        /// Decrypts a string of data using a key
        /// </summary>
        /// Author: Greivin Sanchez
        /// StoryID: ST-PA-4.1, ST-PA-4.6
        /// <param name="data">the string that is going to be decrypted</param>
        /// <param name="key">the string that is going to be used as key to decrypt data</param>
        /// <returns> the decrypted string </returns>
        string Decrypt(string data, string key);
        Task<bool> passwordIsValid(IdentityUser user, string password);

        Task<bool> confirmAccount(string email);

        Task sendConfirmation(string email);

        Task<bool> isConfirmed(string email);
        //GETS
        Task<IdentityUser> getUserByEmail(string email);

        Task<bool> emailIsAlreadyRegistered(string email);

        Task sendPasswordReset(string email);
        Task<bool> resetPassword(string email, string newPassword);
    }
}
