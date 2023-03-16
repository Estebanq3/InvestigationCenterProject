using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.WorkWithUs.Entities;
//using ResearchRepository.Domain.People.DTOs;
using ResearchRepository.Domain.WorkWithUs.Repositories;

using ResearchRepository.Domain.Authentication.ValueObjects;


namespace ResearchRepository.Infrastructure.WorkWithUs.Repositories
{
    internal class WorkWithUsRepository : IWorkWithUsRepository
    {
        private readonly WorkWithUsDbContext _dbContext;
        public IUnitOfWork UnitOfWork => _dbContext;

        public WorkWithUsRepository(WorkWithUsDbContext unitOfWork)
        {
            _dbContext = unitOfWork;
        }

        public async Task<WorkInfo> GetAsyncInfo()
        {
            return await _dbContext.WorkInfo.FirstAsync();
        }

    }
}

