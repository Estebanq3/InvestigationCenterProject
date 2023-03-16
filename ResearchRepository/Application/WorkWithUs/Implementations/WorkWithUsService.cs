using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResearchRepository.Domain.WorkWithUs.Entities;
using ResearchRepository.Domain.WorkWithUs.Repositories;
using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.Authentication.ValueObjects;


//using ResearchRepository.Domain.People.DTOs;

namespace ResearchRepository.Application.WorkWithUs.Implementations
{
    public class WorkWithUsService : IWorkWithUsService
    {
        private readonly IWorkWithUsRepository _workWithUSRepository;

        public WorkWithUsService(IWorkWithUsRepository workWithUsRepository)
        {
            _workWithUSRepository = workWithUsRepository;
        }
        public async Task <WorkInfo> GetAsyncInfo() //GetAsyncPersona
        {
            return await _workWithUSRepository.GetAsyncInfo();
        }
        

    }
}
