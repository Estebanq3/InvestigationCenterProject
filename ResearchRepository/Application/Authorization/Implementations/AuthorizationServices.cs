using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.Authorization.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ResearchRepository.Application.Authorization.Implementations
{
    public class AuthorizationServices : IAuthorizationServices
    {
        private readonly IAuthorizationRepository _authorizationRepository;
        public AuthorizationServices(IAuthorizationRepository authorization) {
            _authorizationRepository = authorization;
        }
        public async Task configureRoles() {
            await _authorizationRepository.configureRoles();
        }
        public async Task assignRole(string email, string role){
            await _authorizationRepository.assignRole(email,role);
        }
        public async Task removeRole(string email, string role){
            await _authorizationRepository.removeRole(email, role);
        }
        
        public async Task<bool> isPartOfRole(string email, string role) {
            return await _authorizationRepository.isPartOfRole(email, role);
        }
        public async Task switchRoleState(string email, string role, bool isPart) {
            await _authorizationRepository.switchRoleState(email, role, isPart);
        }
        public async Task<IList<IdentityUser>> getUsers()
        {
            return await _authorizationRepository.getUsers();
        }
        public async Task<IList<string>> getRoles() {
            return await _authorizationRepository.getRoles();
        }

        public async Task<IList<string>> getUserRoles(string email) {
            return await _authorizationRepository.getUserRoles(email);
        }

        public async Task<IList<IdentityUser>> getUsersBySearch(string searched) {
            return await _authorizationRepository.getUsersBySearch(searched);
        }
        public async Task SeedClaimsForSuperAdministrator() {
            await _authorizationRepository.SeedClaimsForSuperAdministrator();
        }

        public async Task AddPermissionClaim(IdentityRole role) {
            await _authorizationRepository.AddPermissionClaim(role);
        }
        public List<string> getAllClaims() {
            return _authorizationRepository.getAllClaims();
        }
        public async Task<List<bool>> getboolRoleClaims(string roleName) {
            return await _authorizationRepository.getboolRoleClaims(roleName);
        }

        public async Task Update(string roleName, List<string> selectedClaims) {
            await _authorizationRepository.Update(roleName, selectedClaims);
        }

    }
}
