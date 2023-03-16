
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ResearchRepository.Domain.Authorization.Repositories;
using ResearchRepository.Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Infrastructure.People;
using ResearchRepository.Domain.Authorization.Entities;

namespace ResearchRepository.Infrastructure.Authorization.Repositories
{
    internal class PermissionsRepository : IPermissionsRepository
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly PermissionsContext _dbContext;
        public IUnitOfWork UnitOfWork => _dbContext;
        private readonly IServiceProvider _sp;


        public PermissionsRepository(IHttpContextAccessor httpContextAccessor, PermissionsContext unitOfWork, IServiceProvider sp) {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = unitOfWork;
            _sp = sp;
        }

        public async Task<IList<bool>> AdministratorOfGroupBoolList(int groupCount, string email)
        {
            IList<bool> boolList = new List<bool>();
            IList<AccountIsAdministratorOfGroup> admi = await _dbContext.AccountIsAdministratorOfGroup.Where(e => e.Email == email).ToListAsync();
            for (int i = 1; i <= groupCount; i++) {
                if (admi.FirstOrDefault(e => e.GroupId == i) != null)
                {
                    boolList.Add(true);
                }
                else {
                    boolList.Add(false);
                }
            }
            return boolList;
        }

        public async Task<IList<bool>> CollaboratorOfGroupBoolList(int groupCount, string email)
        {
            IList<bool> boolList = new List<bool>();
            IList<AccountIsCollaboratorOfGroup> colab = await _dbContext.AccountIsCollaboratorOfGroup.Where(e => e.Email == email).ToListAsync();
            for (int i = 1; i <= groupCount; i++)
            {
                if (colab.FirstOrDefault(e => e.GroupId == i) != null)
                {
                    boolList.Add(true);
                }
                else
                {
                    boolList.Add(false);
                }
            }
            return boolList;
        }

        public async Task changeAdmiGroup(string email, int groupId, bool assign) {
            bool found = false;
            AccountIsAdministratorOfGroup relationship = await _dbContext.AccountIsAdministratorOfGroup.FirstOrDefaultAsync(e => e.Email == email && e.GroupId == groupId);
            if (relationship != null) {
                found = true;
            }
            if (assign && !found)
            {
                AccountIsAdministratorOfGroup newRelationship = new AccountIsAdministratorOfGroup();
                newRelationship.Email = email;
                newRelationship.GroupId = groupId;
                await _dbContext.AccountIsAdministratorOfGroup.AddAsync(newRelationship);
                _dbContext.SaveChanges();
            }
            else if(!assign && found)
            {
                _dbContext.AccountIsAdministratorOfGroup.Remove(relationship);
                _dbContext.SaveChanges();   
            } 
        }


        public async Task changeColabGroup(string email, int groupId, bool assign)
        {
            bool found = false;
            AccountIsCollaboratorOfGroup relationship = await _dbContext.AccountIsCollaboratorOfGroup.FirstOrDefaultAsync(e => e.Email == email && e.GroupId == groupId);
            if (relationship != null)
            {
                found = true;
            }
            if (assign && !found)
            {
                AccountIsCollaboratorOfGroup newRelationship = new AccountIsCollaboratorOfGroup();
                newRelationship.Email = email;
                newRelationship.GroupId = groupId;
                await _dbContext.AccountIsCollaboratorOfGroup.AddAsync(newRelationship);
                _dbContext.SaveChanges();
            }
            else if (!assign && found)
            {
                _dbContext.AccountIsCollaboratorOfGroup.Remove(relationship);
                _dbContext.SaveChanges();
            }

        }


        private async Task<bool> AuthorizeClaimForAllUserRoles(string claim) {
            var scope = _sp.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var user = _httpContextAccessor.HttpContext?.User;
            if (user != null && user.Identity != null && user.Identity.Name != null)
            {
                var currentUser = await userManager.FindByEmailAsync(user.Identity.Name);
                IList<string> roleNames = await userManager.GetRolesAsync(currentUser);
                IList<Claim> claims;
                IdentityRole currentRole;
                foreach (var roleName in roleNames)
                {
                    currentRole = await roleManager.FindByNameAsync(roleName);
                    claims = await roleManager.GetClaimsAsync(currentRole);
                    if (claims.Any(a => a.Type == "Permission" && a.Value == claim))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

       /* private async Task<bool> AuthorizeClaimForCurrentRole(string claim) {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user != null && user.Identity != null && user.Identity.Name != null)
            {
                string roleName = "Administrador";//getCurrentRoleName
                IList<Claim> claims;
                IdentityRole currentRole = await roleManager.FindByNameAsync(roleName);
                claims = await roleManager.GetClaimsAsync(currentRole);
                if (claims.Any(a => a.Type == "Permission" && a.Value == claim))
                {
                    return true;
                }
            }
            return false;
         }*/

        public async Task<bool> AuthorizeClaim(string claim)
        {
            return await AuthorizeClaimForAllUserRoles(claim);
            //return await AuthorizeClaimForCurrentRole(claim);
        }

        public async Task<bool> AuthorizeGroup(int groupId)
        {
            var scope = _sp.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var user = _httpContextAccessor.HttpContext?.User;
            if (user != null && user.Identity != null && user.Identity.Name != null)
            {
                var currentUser = await userManager.FindByEmailAsync(user.Identity.Name);
                IList<string> roleNames = await userManager.GetRolesAsync(currentUser);
                if (roleNames.Contains("Administrador"))
                {
                    return true;
                }
                string userEmail = user.Identity.Name;
                AccountIsAdministratorOfGroup admi = await _dbContext.AccountIsAdministratorOfGroup.FirstOrDefaultAsync(e => e.Email == userEmail && e.GroupId == groupId);
                AccountIsCollaboratorOfGroup colab = await _dbContext.AccountIsCollaboratorOfGroup.FirstOrDefaultAsync(e => e.Email == userEmail && e.GroupId == groupId);
                if (admi != null || colab != null) {
                    return true;
                }

            }
             return false;
        }
    }
}
