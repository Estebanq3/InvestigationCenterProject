using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.Contacts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Application.Contacts
{
    public interface IContactsService
    {
        /// <summary>
        /// saves the contact object.
        /// </summary>
        /// Author: Dean Vargas
        /// StoryID: ST-MM-1.4
        /// <param name="contact">The contact object</param>
        /// <returns></returns>
        Task SaveContactsAsync(Contact contact);

        /// <summary>
        /// Get the contact given its ID.
        /// </summary>
        /// Author: Dean Vargas
        /// StoryID: ST-MM-1.6
        /// <param name="id">ID of the contact</param>
        /// <returns> contact given the ID</returns>
        Task<Contact?> GetContactsByIdAsync(long id);

        /// <summary>
        /// Get the count of total contacts in the database.
        /// </summary>
        /// Author: Dean Vargas
        /// StoryID: ST-MM-1.4
        /// <returns>The count of total contacts in the database</returns>
        Task<int> GetContactsCountAsync();

        /// <summary>
        /// Save the provided contact object.
        /// </summary>
        /// Author: Dean Vargas
        /// StoryID: ST-MM-1.4
        /// <param name="contact">The object of contact to be saved.</param>
        Task DeleteContactsAsync(Contact contact);

        /// <summary>
        /// Get the number of contacts given the ResearchGroup and a search term
        /// </summary>
        /// Author: Dean Vargas
        /// StoryID: ST-MM-1.4
        /// <param name="term">Search term</param>
        /// <param name="group">Group where the contacts will retrieved</param>
        Task<int> GetContactsByTermCount(ResearchGroup group, string term);

        /// <summary>
        /// Get the number of contacts given the ResearchGroup
        /// </summary>
        /// Author: Dean Vargas
        /// StoryID: ST-MM-1.4
        /// <param name="group">Group where the contacts will retrieved</param>
        Task<int> GetContactsByGroupCount(ResearchGroup group);

        /// <summary>
        /// Get the contacts of a given the ResearchGroup, number of elements to 
        /// retrieve and the current page.
        /// </summary>
        /// Author: Dean Vargas
        /// StoryID: ST-MM-1.4
        /// <param name="group">Group where the contacts will retrieved</param>
        /// <param name="currentPage">Current page of the search</param>
        /// <param name="size">Number of elements to retrieve</param>
        Task<IEnumerable<Contact>?> GetContactsByGroupPaged(ResearchGroup group, int currentPage, int size);

        /// <summary>
        /// Get the contacts of a given the ResearchGroup, number of elements to 
        /// retrieve, term and the current page.
        /// </summary>
        /// Author: Dean Vargas
        /// StoryID: ST-MM-1.4
        /// <param name="group">Group where the contacts will retrieved</param>
        /// <param name="term">Search term</param>
        /// <param name="currentPage">Current page of the search</param>
        /// <param name="size">Number of elements to retrieve</param>
        Task<IEnumerable<Contact>?> GetContactsByTermPaged(ResearchGroup group, int currentPage, int size, string term);

        /// <summary>
        /// Gets the main contact given a group.
        /// </summary>
        /// Author: Dean Vargas
        /// StoryID: ST-MM-1.4
        /// <param name="group">Group where the contacts will retrieved</param>
        Task<Contact?> GetMainContact(ResearchGroup group);

        /// <summary>
        /// Toggles the state of being main contact given a group id.
        /// </summary>
        /// Author: Dean Vargas
        /// StoryID: ST-MM-1.4
        /// <param name="idGroup">Group id where the state will change</param>
        Task ChangeMainContactState(int idGroup);

        /// <summary>
        /// Send email to main contact of the group with info from contact form
        /// </summary>
        /// Author: Roberto Méndez
        /// StoryID: ST-MM-1.4
        /// <param name="contactList"></param>
        /// <param name="userEmail"></param>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        /// <param name="organization"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendContactEmailAsync(IEnumerable<Contact> contactList, string userEmail, string name, string subject, string organization, string message);
    }
}
