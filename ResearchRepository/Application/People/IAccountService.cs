using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.People.Repositories;
using ResearchRepository.Domain.Core.Repositories;

namespace ResearchRepository.Application.People
{
    public interface IAccountService
    {
        /// <summary>
        /// Search a specific Person in the database based on their email. 
        /// </summary>
        /// Author: Greivin Sánchez 
        /// StoryID: ST-PA-4.6
        /// <returns>A entity of Person</returns>
        Task<Person> SearchPersonByEmail(string email);

    }
}
