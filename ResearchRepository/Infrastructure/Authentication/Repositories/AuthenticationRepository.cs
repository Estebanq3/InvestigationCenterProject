using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ResearchRepository.Domain.Authentication.Repositories;
using ResearchRepository.Domain.Authentication.ValueObjects;
using ResearchRepository.Application.People;
using System.Security.Claims;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using ResearchRepository.Domain.Core.ValueObjects;

namespace ResearchRepository.Infrastructure.Authentication.Repositories
{
    internal class AuthenticationRepository : IAuthenticationRepository
    {
        private UserManager<IdentityUser> userManag;
        private SignInManager<IdentityUser> signInManag;
        private NavigationManager navigationManager;
        private IDataProtectionProvider _dataProtecter;
        private IEmailSender sender;
        private IAcademicProfileService profileService;
        private IPersonService personService;

        public AuthenticationRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, NavigationManager nav, IDataProtectionProvider dataProtector,
            IAcademicProfileService profileServ, IPersonService personServ, IEmailSender send)
        {
            this.userManag = userManager;
            this.signInManag = signInManager;
            navigationManager = nav;
            _dataProtecter = dataProtector;
            sender = send;
            profileService = profileServ;
            personService = personServ;
        }


        public async Task<bool> RegisterRequestAsync(Register r)//retornar bool
        {
            bool success = false; 
            var user = new IdentityUser()
            {
                UserName = r.Email,
                Email = r.Email,
                EmailConfirmed = false
            };
            var result = await userManag.CreateAsync(user,r.Password);
            
            if (result.Succeeded)
            {
                await sendConfirmation(r.Email);
                success = true;
                await personService.registerUser(r);
                await profileService.registerUser(r);
            }
            return success;
        }

        public async Task<bool> SignInRequestAsync(Register r, bool isPersistent){
            bool result = false;
            var user = await userManag.FindByNameAsync(r.Email);
            if (user != null) {
                if (await userManag.CheckPasswordAsync(user, r.Password) == true)
                {
                    if (await userManag.IsEmailConfirmedAsync(user))
                    {
                        var token = await userManag.GenerateUserTokenAsync(user, TokenOptions.DefaultProvider, "Login");
                        string data = user.Id + "|" + token;
                        var protector = _dataProtecter.CreateProtector("Login");
                        var protectedData = protector.Protect(data);
                        string persistent;
                        if (isPersistent)
                        {
                            persistent = "true";
                        }
                        else
                        {
                            persistent = "false";
                        }

                        navigationManager.NavigateTo("/login?key=" + protectedData, true);
                        result = true;
                    }
                    else {
                        navigationManager.NavigateTo("/confirmAccount/" + EncryptString(r.Email, "confirm"), true);
                    }
                }
            }

            return result;
        }
       
        public bool IsSignedIn(ClaimsPrincipal user){
            return  signInManag.IsSignedIn(user);
        }

        public async Task SignOut() {
            await signInManag.SignOutAsync();
        }

        public async Task<bool> SignInInternalAsync(string token, bool isPersisten)
        {
            var dataProtector = _dataProtecter.CreateProtector("Login");
            var data = dataProtector.Unprotect(token);
            var parts = data.Split('|');
            var identityUser = await userManag.FindByIdAsync(parts[0]);

            if (identityUser == null)
            {
                return false;//couldnt sign in
            }

            var isTokenValid = await userManag.VerifyUserTokenAsync(identityUser, TokenOptions.DefaultProvider, "Login", parts[1]);

            if (isTokenValid)
            {
                var isPersistent = isPersisten;//bool.Parse(parts[2]);

                await userManag.ResetAccessFailedCountAsync(identityUser);

                await signInManag.SignInAsync(identityUser, isPersistent);

                return true;//could sign in
            }

            return false;//couldnt sign in
        }

        public async Task<bool> ChangePassword( IdentityUser user, string oldPassword, string newPassword) {
            IdentityResult result = await userManag.ChangePasswordAsync(user,  oldPassword,  newPassword);
            return result.Succeeded;
        }

        public async Task<bool> passwordIsValid(IdentityUser user, string password) {
            return await userManag.CheckPasswordAsync(user, password);
        }
        public string EncryptString(string data, string key)
        {
            var protector = _dataProtecter.CreateProtector(key);
            return protector.Protect(data);
            
        }

        public string Decrypt(string data,string key)
        {
            var protector = _dataProtecter.CreateProtector(key);
            return protector.Unprotect(data);
            
        }

        public Task<IdentityUser> getUserByEmail(string email) {
            return userManag.FindByEmailAsync(email);
        }

        public async Task<bool> confirmAccount(string email) {
            bool accountConfirmed = false;
            var user = await userManag.FindByEmailAsync(email);
            if (user != null) {
                var token = await userManag.GenerateEmailConfirmationTokenAsync(user);
                await userManag.ConfirmEmailAsync(user, token);
                if (await userManag.IsEmailConfirmedAsync(user)) {
                    accountConfirmed = true;
                }
            }
            return accountConfirmed;
        }
        public async Task<bool> isConfirmed(string email) {
            bool confirmed = false;
            var user = await userManag.FindByEmailAsync(email);
            if (user != null) {
                confirmed = await userManag.IsEmailConfirmedAsync(user);
            }
            return confirmed;
        }

        public async Task sendConfirmation(string userEmail) {
           var user = await userManag.FindByEmailAsync(userEmail);
            if (user != null && ! await userManag.IsEmailConfirmedAsync(user)) {
                StandarizedEmail email = new StandarizedEmail(2);
                email.setAccountConfirmationEmail("https://localhost:44331/confirm/" + EncryptString(userEmail, "accountConfirmation"));
                await sender.SendEmailAsync(userEmail, "Confirmación de Cuenta", email.getContent());
            }
        }

        public async Task<bool> emailIsAlreadyRegistered(string email) {
            bool isRegistered = false;
            var user = await userManag.FindByNameAsync(email);
            if (user != null) {
                isRegistered = true;
            }
            return isRegistered;
        }
        public async Task sendPasswordReset(string email) {
            var user = await userManag.FindByEmailAsync(email);
            if (user != null) {
                var resetToken = await userManag.GeneratePasswordResetTokenAsync(user);
                await sender.SendEmailAsync(email, "Recuperación de Contraseña", "Para recuperar su contraseña haga <a href=\"https://localhost:44331/passwordRecovery/" + EncryptString(email, "recovery") + "\"> clic aquí </a>") ;
            }
        }
        public async Task<bool> resetPassword(string email, string newPassword) {
            bool passwordChanged = false;            
            var user = await userManag.FindByEmailAsync(email);
            if (user != null) {
                var token = await userManag.GeneratePasswordResetTokenAsync(user);
                await userManag.ResetPasswordAsync(user, token, newPassword);
                if (await userManag.CheckPasswordAsync(user,newPassword)) {
                    passwordChanged = true;
                }
            }
            return passwordChanged;
        }
    }
}
