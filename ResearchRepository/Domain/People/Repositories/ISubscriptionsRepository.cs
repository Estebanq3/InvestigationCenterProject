using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.People.Entities;

namespace ResearchRepository.Domain.People.Repositories
{
    public interface ISubscriptionsRepository
    {
        /// <summary>
        /// Add new subscription to database
        /// </summary>
        /// Author: Greivin Sánchez / Carlos Mora
        /// StoryID: ST-PA-2.7
        /// <returns: A task </returns>
        Task AddSubscription(string email, int grupo);


        /// <summary>
        /// Delete a subscription from database 
        /// </summary>
        /// Author: Greivin Sánchez  / Carlos Mora
        /// StoryID: ST-PA-2.7
        /// <returns>A Task</returns>
        Task DeleteSubscription(string email, int grupo);

        /// <summary>
        /// Find a specific subscription
        /// </summary>
        /// Author: Greivin Sánchez  / Carlos Mora
        /// StoryID: ST-PA-2.7
        /// <returns>A list of subscriptions</returns>
        Task<IList<Subscriptions>> FindSubscription(string email, int groupId);

        /// <summary>
        /// Send email to subscriptors of a specific group.
        /// </summary>
        /// Author: Greivin Sánchez  / Carlos Mora
        /// StoryID: ST-PA-2.8
        /// <returns>Notify subscriptions</returns>
        Task NotifySubscriptors(int groupID, string groupName, string subject, string NewsLink);

    }
}
