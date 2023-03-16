using Microsoft.EntityFrameworkCore;
using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.Contacts.Entities;
using ResearchRepository.Domain.Contacts.Repositories;
using ResearchRepository.Infrastructure.ResearchGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ResearchRepository.Infrastructure.Contacts.Repositories
{
    internal class ContactsRepository : IContactsRepository
    {
        private readonly ResearchGroupsDbContext _dbContext;
        private readonly Regex _sqlInjectionValidator;

        public IUnitOfWork UnitOfWork => _dbContext;

        public ContactsRepository(ResearchGroupsDbContext context)
        {
            _dbContext = context;
            _sqlInjectionValidator = new Regex(@"(?i)\b(ALTER|CREATE|DELETE|DROP|EXEC(UTE){0,1}|INSERT( +INTO){0,1}|MERGE|SELECT|UPDATE|UNION( +ALL){0,1})\b");
        }

        public async Task SaveContactsAsync(Contact contact)
        {
            var contactToInsert = await _dbContext.Contacts.FirstOrDefaultAsync(n => n.Id == contact.Id);
            if (contactToInsert != null)
            {
                _dbContext.Entry(contactToInsert).CurrentValues.SetValues(contact);
            }
            else
            {
                _dbContext.Attach(contact).State = EntityState.Added;
                _dbContext.Contacts.Add(contact);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Contact?> GetContactsByIdAsync(long id)
        {
            return await _dbContext.Contacts.Include(n => n.Group).FirstOrDefaultAsync(n => n.Id == id);
        }



        public async Task<int> GetContactsCountAsync()
        {
            return await _dbContext.Contacts.CountAsync();
        }


        public async Task DeleteContactsAsync(Contact contact)
        {
            _dbContext.Remove(contact);
            await _dbContext.SaveEntitiesAsync();
        }

        public async Task<int> GetContactsByTermCount(ResearchGroup group, string term)
        {
            Match match = _sqlInjectionValidator.Match(term);
            if (!match.Success)
            {
                var data = await _dbContext.Contacts.Where(n => n.Group == group).OrderByDescending(n => n.StartDate).ToListAsync();

                return await Task.FromResult(data.Where(n => n.Name.ToString().ToLower().Contains(term.ToLower())).Count());
            }
            else
            {
                throw new Exception("Failed to parse against SQL injection validator.");
            }
        }


        public async Task<int> GetContactsByGroupCount(ResearchGroup group)
        {
            return await _dbContext.Contacts.Where(n => n.Group == group).CountAsync();
        }

        public async Task<IEnumerable<Contact>?> GetContactsByGroupPaged(ResearchGroup group, int currentPage, int size)
        {
            return await _dbContext.Contacts.OrderByDescending(n => n.StartDate).Where(n => n.Group == group).Skip((currentPage - 1) * size).Take(size).ToListAsync();
        }
        public async Task<IEnumerable<Contact>?> GetContactsByTermPaged(ResearchGroup group, int currentPage, int size, string term) {

            Match match = _sqlInjectionValidator.Match(term);
            if (!match.Success)
            {
                var data = await _dbContext.Contacts
                .Where(t => t.Group.Id == group.Id)
                .OrderByDescending(n => n.StartDate)
                .ToListAsync();

                //Local filter
                //Slower than doing all in SQL
                return await Task.FromResult(data.Where(t => t.Name.ToString().ToLower().Contains(term.ToLower()))
                    .Skip((currentPage - 1) * size).Take(size));
            }
            else
            {
                throw new Exception("Failed to parse against SQL injection validator.");
            }

        }

        public async Task<Contact?> GetMainContact(ResearchGroup group)
        {
            return await _dbContext.Contacts.Include(n => n.Group).FirstOrDefaultAsync(c => c.MainContact == true && c.Group.Id == group.Id);
        }
    }
}

