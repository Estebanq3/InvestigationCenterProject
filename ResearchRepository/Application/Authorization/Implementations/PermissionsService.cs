using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.Authorization.Repositories;
using ResearchRepository.Domain.Authorization;

namespace ResearchRepository.Application.Authorization.Implementations
{
    internal class PermissionsService : IPermissionsService
    {
        private readonly IPermissionsRepository _permissionsRepository;
        public ClaimsList _claimsList { get; }
        public PermissionsService(IPermissionsRepository permissions)
        {
            _permissionsRepository = permissions;
            _claimsList = new ClaimsList();
        }

        public async Task<bool> AuthorizeClaim(string claim)
        {
            return await _permissionsRepository.AuthorizeClaim(claim);
        }

        public async Task<bool> AuthorizeGroup(int groupId)
        {
            return await _permissionsRepository.AuthorizeGroup(groupId);
        }


        public async Task<IList<bool>> AdministratorOfGroupBoolList(int groupCount, string email) {
            return await _permissionsRepository.AdministratorOfGroupBoolList(groupCount, email);
        }


        public async Task<IList<bool>> CollaboratorOfGroupBoolList(int groupCount, string email) {
            return await _permissionsRepository.CollaboratorOfGroupBoolList(groupCount, email);
        }


        public async Task changeAdmiGroup(string email, int groupId, bool assign) {
            await _permissionsRepository.changeAdmiGroup(email, groupId, assign);
        }

        public async Task changeColabGroup(string email, int groupId, bool assign) {
            await _permissionsRepository.changeColabGroup(email, groupId, assign);
        }

    }
}
