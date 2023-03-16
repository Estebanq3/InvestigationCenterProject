using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResearchRepository.Domain.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ResearchRepository.Domain.Authorization.Repositories;
using System.Security.Claims;
using ResearchRepository.Domain.Authorization;
using ResearchRepository.Domain.People.Entities;


namespace ResearchRepository.Infrastructure.Authorization.Repositories
{
    internal class AuthorizationRepository : IAuthorizationRepository
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<IdentityUser> userManager;
        public ClaimsList claimsList;

        public AuthorizationRepository(RoleManager<IdentityRole> roleM, UserManager<IdentityUser> userM) {
            roleManager = roleM;
            userManager = userM;
            claimsList = new ClaimsList();
        }

        public List<string> getAllClaims()
        {
            return claimsList.Claims;
        }

        public async Task configureRoles() {
            string[] roleNames = { "Administrador", "Administrador de Centro", "Administrador de Grupo", "Colaborador de Centro", "Colaborador de Grupo" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 2
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            await SeedClaimsForSuperAdministrator();
        }
        public async Task assignRole(string email, string role) {
            var roleExist = await roleManager.RoleExistsAsync(role);
            var user = await userManager.FindByEmailAsync(email);
            if (roleExist && user != null)
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
        public async Task removeRole(string email, string role) {
            var roleExist = await roleManager.RoleExistsAsync(role);
            var user = await userManager.FindByEmailAsync(email);
            if (roleExist && user != null)
            {
                await userManager.RemoveFromRoleAsync(user, role);
            }
        }
        public async Task switchRoleState(string email, string role, bool isPart)
        {
            if (isPart)
            {
                await assignRole(email, role);
            }
            else
            {
                await removeRole(email, role);
            }
        }

        public async Task<IList<IdentityUser>> getUsers() {
            List<IdentityUser> usersList = new List<IdentityUser>();

            var user = userManager.Users.Select(x => new IdentityUser{
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                PasswordHash = "*****"
            });

            foreach (var item in user){
                usersList.Add(item);
            }

            return usersList;
        }
        public async Task<bool> isPartOfRole(string email, string role) {
            var user = await userManager.FindByEmailAsync(email);
            return await userManager.IsInRoleAsync(user, role);
        }

        public async Task<IList<string>> getRoles(){
            List<string> roles = new List<string>();
            roles = roleManager.Roles.Select(x => x.Name).ToList();
            return roles;
        }

        public async Task<IList<string>> getUserRoles(string email) {
            IList<string> list = new List<string>();
            var user = await userManager.FindByEmailAsync(email);
            if (user!=null) {
                list = await userManager.GetRolesAsync(user);
            }
            return list;
        }
        
        public async Task<IList<IdentityUser>> getUsersBySearch(string searched) {
            IList<IdentityUser> allUsers = await getUsers();
            IList<IdentityUser> usersMatched = new List<IdentityUser>();
            foreach(var i in allUsers) {
                if (i.Email.Contains(searched)) {
                    usersMatched.Add(i);
                }
            }
            return usersMatched;
        }


        public async Task SeedClaimsForSuperAdministrator()
        {
            var adminRole = await roleManager.FindByNameAsync("Administrador");
            var allClaims = await roleManager.GetClaimsAsync(adminRole);
            string permission = claimsList.EditRoles;
            string permission2 = claimsList.EditUsers;
            if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
            {
                await roleManager.AddClaimAsync(adminRole, new Claim("Permission", permission));
            }
            if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission2))
            {
                await roleManager.AddClaimAsync(adminRole, new Claim("Permission", permission2));
            }
            //await AddPermissionClaim(adminRole);
        }

        public async Task AddPermissionClaim(IdentityRole role)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = getAllClaims();
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }

        public async Task<List<bool>> getboolRoleClaims(string roleName) {
            List<bool> ClaimBooleans = new List<bool>();
            var role = await roleManager.FindByNameAsync(roleName);
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = getAllClaims();
            foreach (var permission in allPermissions)
            {
                if (allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    ClaimBooleans.Add(true);
                }
                else {
                    ClaimBooleans.Add(false);
                }
            }
            return ClaimBooleans;
        }

        public async Task Update(string roleName,List<string> selectedClaims)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            var claims = await roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await roleManager.RemoveClaimAsync(role, claim);
            }
            foreach (var claim in selectedClaims)
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", claim));
            }
        }


 
    }

}
