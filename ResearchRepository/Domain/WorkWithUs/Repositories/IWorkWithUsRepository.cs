using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResearchRepository.Domain.WorkWithUs.Entities;
using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.Authentication.ValueObjects;
//using ResearchRepository.Domain.People.DTOs;

namespace ResearchRepository.Domain.WorkWithUs.Repositories
{
    public interface IWorkWithUsRepository
    {
        /// <summary>
        /// Gets information of work with us.
        /// </summary>
        /// Author: Carlos mora
        /// StoryID: ST-PA-2.1
        /// <returns>Single information</returns>
        Task <WorkInfo> GetAsyncInfo();        
    }
}