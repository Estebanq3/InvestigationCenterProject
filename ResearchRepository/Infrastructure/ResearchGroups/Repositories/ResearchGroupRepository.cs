using ResearchRepository.Domain.ResearchGroups.Repositories;
using ResearchRepository.Domain.ResearchGroups.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using ResearchRepository.Domain.ResearchGroups.DTOs;

namespace ResearchRepository.Infrastructure.ResearchGroups.Repositories
{
    internal class ResearchGroupRepository : IResearchGroupRepository
    {

        private readonly ResearchGroupsDbContext context;
        public IUnitOfWork UnitOfWork => context;

        public ResearchGroupRepository(ResearchGroupsDbContext unitOfWork)
        {
            context = unitOfWork;
        }

        public async Task SaveAsync(ResearchGroup group)
        {
            context.Update(group);
            await context.SaveEntitiesAsync();
        }

        public async Task<ResearchGroup> GetById(int id)
        {
            return await context.Groups.Include(g=> g.Contacts).Include(g => g.ResearchAreas).Include(g => g.News).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await context.Groups.CountAsync();
        }
    }
}
