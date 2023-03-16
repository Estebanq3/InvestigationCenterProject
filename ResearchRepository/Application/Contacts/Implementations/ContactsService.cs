using ResearchRepository.Domain.Contacts.Entities;
using ResearchRepository.Domain.Contacts.Repositories;
using ResearchRepository.Domain.ResearchGroups.Entities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ResearchRepository.Application.Contacts.Implementations
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IContactEmailService _contactSender;
        private readonly Regex _sqlInjectionValidator;
        public ContactsService(IContactsRepository contactsRepository, IContactEmailService contactSender)
        {
            _contactsRepository = contactsRepository;
            _contactSender = contactSender;
            _sqlInjectionValidator = new Regex(@"(?i)\b(ALTER|CREATE|DELETE|DROP|EXEC(UTE){0,1}|INSERT( +INTO){0,1}|MERGE|SELECT|UPDATE|UNION( +ALL){0,1})\b");
        }

        public async Task SaveContactsAsync(Contact contact)
        {
            ResearchGroup group = contact.Group!;
            group.AddContactToGroup(contact);
            await _contactsRepository.SaveContactsAsync(contact);
        }

        public async Task<Contact?> GetContactsByIdAsync(long id)
        {
            return await _contactsRepository.GetContactsByIdAsync(id);
        }

        public async Task<int> GetContactsCountAsync()
        {
            return await _contactsRepository.GetContactsCountAsync();
        }


        public async Task DeleteContactsAsync(Contact contact)
        {
            ResearchGroup? group = contact.Group;
            if (group != null)
            {
                group.RemoveContactFromGroup(contact);
            }
            await _contactsRepository.DeleteContactsAsync(contact);
        }


        public async Task<int> GetContactsByTermCount(ResearchGroup group, string term)
        {
            Match match = _sqlInjectionValidator.Match(term);
            if (!match.Success)
            {
                return await _contactsRepository.GetContactsByTermCount(group, term);
            }
            else
            {
                throw new Exception("Failed to parse against SQL injection validator.");
            }
            
        }

        public async Task<int> GetContactsByGroupCount(ResearchGroup group)
        {
            return await _contactsRepository.GetContactsByGroupCount(group);
        }

        public async Task<IEnumerable<Contact>?> GetContactsByGroupPaged(ResearchGroup group, int currentPage, int size)
        {
            return await _contactsRepository.GetContactsByGroupPaged(group, currentPage, size);
        }

        public async Task<IEnumerable<Contact>?> GetContactsByTermPaged(ResearchGroup group, int currentPage, int size, string term)
        {
            Match match = _sqlInjectionValidator.Match(term);
            if (!match.Success)
            {
                return await _contactsRepository.GetContactsByTermPaged(group, currentPage, size, term);
            }
            else
            {
                throw new Exception("Failed to parse against SQL injection validator.");
            }
            
        }

        public async Task ChangeMainContactState(int contactId)
        {
            var contact = await this.GetContactsByIdAsync(contactId);

            if (contact != null)
            {
                contact.ChangeStateMainContact(!contact.MainContact);
            }
        }

        public async Task<Contact?> GetMainContact(ResearchGroup group)
        {
            return await _contactsRepository.GetMainContact(group);
        }

        public async Task SendContactEmailAsync(IEnumerable<Contact> contactList, string userEmail, string name, string subject, string organization, string message)
        {
            var messageToSend = "";
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(userEmail) && !string.IsNullOrEmpty(subject) && !string.IsNullOrEmpty(message))
            {
                messageToSend = "<b>De:</b> " + name + "<br><b>Correo: </b>" + userEmail + "<br><b>Asunto:</b> " + subject;
                if (!string.IsNullOrEmpty(organization))
                    messageToSend += "<br><b>Organización que representa:</b> " + organization;
                messageToSend += "<br><b>Mensaje:</b><br>" + message;
            }
            List<string> contactEmails = new List<string>();
            if (contactList != null)
            {
                foreach (var contact in contactList)
                {
                    contactEmails.Add(contact.Email.ToString());
                }

                await _contactSender.SendEmailAsync(contactEmails, userEmail, "Formulario de contacto - " + subject, messageToSend);
            }
        }
    }
}

