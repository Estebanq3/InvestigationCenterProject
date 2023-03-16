using ResearchRepository.Domain.ResearchGroups.Repositories;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.ResearchGroups.DTOs;
using ResearchRepository.Domain.ResearchAreas.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Application.ResearchGroups.Implementations
{
    public class ResearchCenterService : IResearchCenterService
    {
        private readonly IResearchCenterRepository _researchCenterRepository;

        public ResearchCenterService(IResearchCenterRepository researchCenterRepository)
        {
            _researchCenterRepository = researchCenterRepository;
        }
        public async Task CreateGroupAsync(ResearchGroup group)
        {
            ResearchCenter center = group.Center!;
            center.AddGroupToCenter(group);

            await _researchCenterRepository.SaveGroupAsync(group);
        }

        public async Task DeleteGroupAsync(ResearchGroup group)
        {
            ResearchCenter center = group.Center!;
            if (group != null)
            {
                center.RemoveGroupFromCenter(group);
            }
            await _researchCenterRepository.DeleteGroupAsync(group);
        }

        public async Task<IEnumerable<ResearchCenterDTO>> GetAllAsync()
        {
            return await _researchCenterRepository.GetAllAsync();
        }

        public async Task<IList<ResearchCenter>> GetAllCenters(){
            return await _researchCenterRepository.GetAllCenters();
        }
        public async Task<IList<ResearchGroup>> GetAllGroupsFromCenter(int centerId)
        {
            return await _researchCenterRepository.GetAllGroupsFromCenter(centerId);
        }
            public async Task<ResearchCenter?> GetByIdAsync(int id)
        {
            return await _researchCenterRepository.GetByIdAsync(id);
        }

        public async Task<int> GetGroupsByTermCountAsync(int idCenter, string term)
        {
            return await _researchCenterRepository.GetGroupsByTermCount(idCenter, term);
        }

        public async Task<IEnumerable<ResearchGroup>?> GetGroupsByTermPagedAsync(int idCenter, int currentPage, int size, string term)
        {
            return await _researchCenterRepository.GetGroupsByTermPaged(idCenter, currentPage, size, term);
        }

        public async Task<int> GetGroupsCountAsync(int idCenter)
        {
            return await _researchCenterRepository.GetGroupsCount(idCenter);
        }

        public async Task<IEnumerable<ResearchGroup>?> GetGroupsPagedAsync(int idCenter, int currentPage, int size)
        {
            return await _researchCenterRepository.GetGroupsPaged(idCenter, currentPage, size);
        }

        public async Task<IEnumerable<ResearchGroup>?> GetGroupsByAreaListAndTermPaged(int centerId, int currentPage, int size, HashSet<ResearchArea> area, string term)
        {
            return await _researchCenterRepository.GetGroupsByAreaListAndTermPaged(centerId, currentPage, size, area, term);
        }

        public async Task<int> GetGroupsByAreaListAndTermCount(int centerId, HashSet<ResearchArea> area, string term)
        {
            return await _researchCenterRepository.GetGroupsByAreaListAndTermCount(centerId, area, term);
        }

        public async Task<IEnumerable<ResearchGroup>?> GetGroupsByAreaListPaged(int centerId, int currentPage, int size, HashSet<ResearchArea> area)
        {
            return await _researchCenterRepository.GetGroupsByAreaListPaged(centerId, currentPage, size, area);
        }

        public async Task<int> GetGroupsByAreaListCount(int centerId, HashSet<ResearchArea> area)
        {
            return await _researchCenterRepository.GetGroupsByAreaListCount(centerId, area);
        }

        public async Task<IEnumerable<ResearchGroup>?> GetActiveGroupsPaged(int centerId, int currentPage, int size) {
            return await _researchCenterRepository.GetActiveGroupsPaged(centerId, currentPage, size);
        }
        public async Task<int> GetActiveGroupsCountAsync(int idCenter) {
            return await _researchCenterRepository.GetActiveGroupsCount(idCenter);
        }

        public async Task<IEnumerable<ResearchGroup>?> GetAllGroupsByTermPagedAsync(int idCenter, int currentPage, int size, string term)
        {
            return await _researchCenterRepository.GetAllGroupsByTermPaged(idCenter, currentPage, size, term);
        }

        public async Task<int> GetAllGroupsByTermCountAsync(int idCenter, string term)
        {
            return await _researchCenterRepository.GetAllGroupsByTermCount(idCenter, term);
        }

        public async Task<IList<ResearchGroup>> GetAllGroups() {
            return await _researchCenterRepository.GetAllGroups();
        }
    }
}
