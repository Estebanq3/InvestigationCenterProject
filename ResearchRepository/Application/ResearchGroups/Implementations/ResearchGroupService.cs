using ResearchRepository.Domain.ResearchGroups.Repositories;
using ResearchRepository.Domain.ResearchGroups.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.ResearchGroups.DTOs;

namespace ResearchRepository.Application.ResearchGroups.Implementations
{
    public class ResearchGroupService : IResearchGroupService
    {
        private readonly IResearchGroupRepository _researchGroupRepository;

        public ResearchGroupService(IResearchGroupRepository researchGroupRepository)
        {
            _researchGroupRepository = researchGroupRepository;
        }

        public async Task<ResearchGroup> GetById(int id)
        {
            return await _researchGroupRepository.GetById(id);
        }
        public async Task<int> GetCountAsync()
        {
            return await _researchGroupRepository.GetCountAsync();
        }

        public async Task ChangeStateGroup(int idGroup, bool state)
        {
            var group = await this.GetById(idGroup);

            if(group != null)
            {
                group.ChangeStateGroup(state);
                await _researchGroupRepository.SaveAsync(group);
            }
        }
    }
}
