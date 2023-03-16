using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using ResearchRepository.Domain.WorkWithUs.Entities;
using ResearchRepository.Domain.Authentication.ValueObjects;
using ResearchRepository.Domain.Core.Repositories;

//using ResearchRepository.Domain.People.DTOs;

namespace ResearchRepository.Application.WorkWithUs
{
    public interface IWorkWithUsService
    {
        /// <summary>
        /// Get info of work with us
        /// </summary>
        /// Author: Carlos Mora
        /// StoryID: ST-PA-2.1
        /// <returns>Info of work with us</returns>
        Task <WorkInfo> GetAsyncInfo();

        
    }

}
