using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Application.Authentication;
using ResearchRepository.Domain.Authentication.Repositories;
using ResearchRepository.Domain.Authentication.ValueObjects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ResearchRepository.Application.Authentication.Implementations
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        public AuthenticationService(IAuthenticationRepository auth)
        {
            _authenticationRepository = auth;
        }

        public async Task<bool> RegisterRequestAsync(Register r)
        {
            return await _authenticationRepository.RegisterRequestAsync(r);
        }

        public async Task<bool> SignInRequestAsync(Register r, bool isPersistent)
        {
            return await _authenticationRepository.SignInRequestAsync(r, isPersistent);
        }

        public bool IsSignedIn(ClaimsPrincipal user)
        {
            return  _authenticationRepository.IsSignedIn( user); 
        }

        public async Task SignOut() {
            await _authenticationRepository.SignOut();
        }

        public async Task<bool> ChangePassword(IdentityUser user, string oldPassword, string newPassword) {
            return await _authenticationRepository.ChangePassword(user, oldPassword, newPassword);
        }

        public string EncryptString(string data, string key)
        {
            return _authenticationRepository.EncryptString(data,  key);
        }

        public string Decrypt(string data,string key) {
            return _authenticationRepository.Decrypt(data, key);
        }
        public async Task<IdentityUser> getUserByEmail(string email) {
            return await _authenticationRepository.getUserByEmail(email);
        }
        public async Task<bool> SignInInternalAsync(string token, bool isPersistent){
            return await _authenticationRepository.SignInInternalAsync(token,isPersistent);
        }

        public async Task<bool> passwordIsValid(IdentityUser user, string password) {
            return await _authenticationRepository.passwordIsValid(user, password);
        }
        public async Task<bool> confirmAccount(string email) {
            return await _authenticationRepository.confirmAccount(email);
        }
        public async Task<bool> isConfirmed(string email) {
            return await _authenticationRepository.isConfirmed(email);
        }
        public async Task sendConfirmation(string email) {
            await _authenticationRepository.sendConfirmation(email);
        }
        public async Task<bool> emailIsAlreadyRegistered(string email) {
            return await _authenticationRepository.emailIsAlreadyRegistered(email);
        }
        public async Task sendPasswordReset(string email) {
            await _authenticationRepository.sendPasswordReset(email);
        }
        public async Task<bool> resetPassword(string email, string newPassword) {
            return await _authenticationRepository.resetPassword(email, newPassword);
        }
    }
}
