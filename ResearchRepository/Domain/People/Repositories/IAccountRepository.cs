using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.People.Entities;

namespace ResearchRepository.Domain.People.Repositories
{
    public interface IAccountRepository
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
