using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.People.Repositories;

namespace ResearchRepository.Infrastructure.People.Repositories
{
    internal class AccountRepository:IAccountRepository
    {

        private readonly AccountInfoDbContext _dbContext;

        public IUnitOfWork UnitOfWork => _dbContext;

        public AccountRepository(AccountInfoDbContext people)
        {
            _dbContext = people;
        }

        public async Task<Person> SearchPersonByEmail(string email) {
            Person person =  await _dbContext.Person.FindAsync(email);
            if (person != null)
            {
                person.AcademicProfile = await _dbContext.AcademicProfile.FindAsync(email);
            }
            return person;
        }



    }
}
