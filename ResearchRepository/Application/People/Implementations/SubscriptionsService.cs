using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.People.Repositories;
using ResearchRepository.Domain.People.Entities;


namespace ResearchRepository.Application.People.Implementations
{
    public class SubscriptionsService:ISubscriptionsService
    {
        private readonly ISubscriptionsRepository _subsRepository;

        public SubscriptionsService(ISubscriptionsRepository repository)
        {
            _subsRepository = repository;
        }
        public async Task AddSubscription(string email, int grupo)
        {
            await _subsRepository.AddSubscription(email,grupo);
        }

        public async Task DeleteSubscription(string email, int group) {
            await _subsRepository.DeleteSubscription(email,group);
        }

        public async Task<IList<Subscriptions>> FindSubscription(string email, int groupId) {
            return await _subsRepository.FindSubscription(email, groupId);
        }

        public async Task NotifySubscriptors(int groupID, string groupName, string subject, string NewsLink)
        {
            await _subsRepository.NotifySubscriptors(groupID,groupName,subject, NewsLink);
        }
    }
}
