using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.ResearchAreas.Repositories;
using ResearchRepository.Domain.ResearchAreas.Entities;
using ResearchRepository.Infrastructure.ResearchGroups;
using ResearchRepository.Domain.Core.Repositories;

using Microsoft.EntityFrameworkCore;
using ResearchRepository.Domain.Core.ValueObjects;

namespace ResearchRepository.Infrastructure.ResearchAreas.Repositories
{
    internal class ResearchAreaRepository : IResearchAreaRepository
    {
        private readonly ResearchGroupsDbContext _dbContext;

        public IUnitOfWork UnitOfWork => _dbContext;

        public ResearchAreaRepository(ResearchGroupsDbContext unitOfWork)
        {
            _dbContext = unitOfWork;
        }

        public async Task SaveAsync(ResearchArea researchArea)
        {
            var result = RequiredString.TryCreate(researchArea.Name.ToString(), 100);

            if (result.IsSuccess)
            {
                _dbContext.Update(researchArea);

                await _dbContext.SaveEntitiesAsync();
            }
            else
                throw new Exception("Nombre de área no válido.");
        }
        public async Task<IEnumerable<ResearchArea>> GetAllAsync()
        {
            var data = await _dbContext.Areas
                .OrderBy(a => a.Name)
                .Include(a => a.ResearchAreas)
                .Include(a => a.ResearchSubAreas.OrderBy(s => s.Name))
                .ThenInclude(s => s.ResearchGroups)
                .ToListAsync();

            // filter research subareas
            return await Task.FromResult(data.Where(a => a.ResearchAreas.Count() == 0));
        }
        public async Task<ResearchArea?> GetByIdAsync(int id)
        {
            return await _dbContext.Areas.FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task DeleteResearchArea(ResearchArea researchArea)
        {
            _dbContext.Remove(researchArea);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<ResearchArea>> GetSubAreaAsync(int id)
        {
            return await _dbContext.Areas.Include(s => s.ResearchAreas.Where(a => a.Id == id)).ToListAsync();

        }

        public async Task<IList<ResearchAreaProject>> GetAssociatedAreas(int id)
        {
            IList<ResearchAreaProject> areas = await _dbContext.ResearchAreaProject.Where(r => r.ProjectsId == id).ToListAsync();

            return areas;
        }
        public async Task<ResearchAreaProject> GetResearchAreaProjectAssociation(int projectId, int areaId)
        {
            return (await _dbContext.ResearchAreaProject
                .Where(r => r.ResearchAreasId == areaId && r.ProjectsId == projectId)
                .ToListAsync()).First();
        }

        public async Task DeleteAssociatedArea(int projectId, int areaId)
        {
            var association = await GetResearchAreaProjectAssociation(projectId, areaId);
                if (association != null){
                    _dbContext.ResearchAreaProject.Remove(association);
                    _dbContext.SaveChanges();
                }
        }

        public async Task AddAssociatedArea(ResearchAreaProject association)
        {
            _dbContext.ResearchAreaProject.Add(association);
            await _dbContext.SaveEntitiesAsync();
        }

}
}
