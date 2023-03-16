using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.People.Repositories;


namespace ResearchRepository.Application.People.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _AccountRepository;

        public AccountService(IAccountRepository _accountRepo)
        {
            _AccountRepository = _accountRepo;
        }

        public async Task<Person> SearchPersonByEmail(string email) {
            var a = _AccountRepository;
            return await _AccountRepository.SearchPersonByEmail(email);
        }

    }
}
