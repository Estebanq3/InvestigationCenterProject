using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components;
using ResearchRepository.Domain.Authentication.ValueObjects;
using System.Security.Claims;

namespace ResearchRepository.Application.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> RegisterRequestAsync(Register r);

        Task<bool> SignInRequestAsync(Register r, bool isPersistent);

        bool IsSignedIn(ClaimsPrincipal user);
        Task SignOut();

        Task<bool> ChangePassword(IdentityUser user, string oldPassword, string newPassword);
        string EncryptString(string data, string key);
        string Decrypt(string data, string key);
        Task<IdentityUser> getUserByEmail(string email);
        Task<bool> SignInInternalAsync(string token, bool isIpersistent);
        Task<bool> passwordIsValid(IdentityUser user, string password);
        Task<bool> confirmAccount(string email);
        Task<bool> isConfirmed(string email);
        Task sendConfirmation(string email);
        Task<bool> emailIsAlreadyRegistered(string email);
        Task sendPasswordReset(string email);
        Task<bool> resetPassword(string email, string newPassword);
    }
}
