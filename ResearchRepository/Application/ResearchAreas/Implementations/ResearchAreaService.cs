using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.ResearchAreas.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.ResearchAreas.Repositories;
using ResearchRepository.Domain.Core.ValueObjects;

namespace ResearchRepository.Application.ResearchAreas.Implementations
{
    
    public class ResearchAreaService : IResearchAreaService
    {
        private readonly IResearchAreaRepository _researchAreaRepository;

        public ResearchAreaService(IResearchAreaRepository researchAreaRepository)
        {
            _researchAreaRepository = researchAreaRepository;
        }

        public async Task<IEnumerable<ResearchArea>> GetResearchAreaAsync()
        {
            return await _researchAreaRepository.GetAllAsync();
        }
        public async Task<ResearchArea?> GetResearchAreaByIdAsync(int id)
        {
            return await _researchAreaRepository.GetByIdAsync(id);
        }
        public async Task AddGroupToSubAreaAsync(ResearchArea researchArea, ResearchGroup researchGroup)
        {
            researchGroup.AddAreaToGroup(researchArea);
            await _researchAreaRepository.SaveAsync(researchArea);
        }
        public async Task RemoveGroupFromSubAreaAsync(ResearchArea researchArea, ResearchGroup researchGroup)
        {
            researchGroup.RemoveAreaFromGroup(researchArea);
            await _researchAreaRepository.SaveAsync(researchArea);
        }

        public async Task DeleteResearchArea(ResearchArea researchArea)
        {

            if(researchArea.ResearchAreas != null && researchArea.ResearchAreas.Count() > 0)
            {
                int size = researchArea.ResearchGroups.Count();

                for (int i = 0; i < size; ++i)
                {
                    researchArea.ResearchGroups.ElementAt(0).RemoveAreaFromGroup(researchArea);
                }
                /*
                foreach (var p in researchArea.ResearchAreas)
                {
                    p.RemoveSubArea(researchArea);
                }
                */
            }
            else
            {
                int j = 0;
                int size = researchArea.ResearchSubAreas.Count();

                for(int i = 0; i < size; ++i)
                {
                    if(researchArea.ResearchSubAreas.ElementAt(j).ResearchAreas.Count() == 1)
                    {
                        await DeleteResearchArea(researchArea.ResearchSubAreas.ElementAt(j));
                    }
                    else
                    {
                        j++;
                    }
                }
            }

            await _researchAreaRepository.DeleteResearchArea(researchArea);
        }

        public async Task AddResearchArea(ResearchArea researchArea)
        {
            var result = RequiredString.TryCreate(researchArea.Name.ToString(), 100);

            if (result.IsSuccess)
                await _researchAreaRepository.SaveAsync(researchArea);
            else
                throw new Exception("Nombre de área no válido.");
        }

        public async Task<IEnumerable<ResearchArea>> GetSubAreaAsync(int id)
        {
            return await _researchAreaRepository.GetSubAreaAsync(id);
        }

        public async Task<IList<ResearchAreaProject>> GetAssociatedAreas(int id)
        {
            return await _researchAreaRepository.GetAssociatedAreas(id);
        }
        public async Task<ResearchAreaProject> GetResearchAreaThesisAssociation(int projectId, int areaId)
        {
            return await _researchAreaRepository.GetResearchAreaProjectAssociation(projectId, areaId);
        }

        public async Task DeleteAssociatedArea(int projectId, int areaId)
        {
            await _researchAreaRepository.DeleteAssociatedArea(projectId, areaId);
        }

        public async Task AddAssociatedArea(ResearchAreaProject association)
        {
            await _researchAreaRepository.AddAssociatedArea(association);
        }

}
}
