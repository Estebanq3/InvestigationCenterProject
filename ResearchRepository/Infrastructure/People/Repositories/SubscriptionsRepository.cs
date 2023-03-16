
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.People.Repositories;
using ResearchRepository.Infrastructure.People;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResearchRepository.Infrastructure.Authentication;
using ResearchRepository.Application.Authentication;
using Microsoft.AspNetCore.Identity.UI.Services;
using ResearchRepository.Domain.Core.ValueObjects;

namespace ResearchRepository.Infrastructure.People.Repositories
{
    internal class SubscriptionsRepository : ISubscriptionsRepository
    {
        private readonly SubscriptionsDbContext _dbContext;
        private IEmailSender sender;
        public IUnitOfWork UnitOfWork => _dbContext;
        public SubscriptionsRepository(SubscriptionsDbContext unitOfWork, IEmailSender send)
        {
            _dbContext = unitOfWork;
            sender = send;

        }
        public async Task AddSubscription(string email, int groupId)
        {
            Subscriptions subscription = new Subscriptions();
            subscription.UserEmail = email;
            subscription.GroupID = groupId;
            _dbContext.Add(subscription);
            await _dbContext.SaveEntitiesAsync();
        }
        public async Task DeleteSubscription(string email, int groupId)
        {
            var _subscription = (from s in _dbContext.Subscriptions
                                 where s.UserEmail == email && s.GroupID == groupId
                                 select s).ToList();
            _dbContext.Subscriptions.RemoveRange(_subscription);
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task<IList<Subscriptions>> FindSubscription(string email, int groupId)
        {
            return await _dbContext.Subscriptions.Where(g => g.GroupID == groupId && g.UserEmail == email).ToListAsync();
        }

        public async Task NotifySubscriptors(int groupID,string groupName,string subject, string NewsLink)
        {
            IList<Subscriptions>  subs =  await _dbContext.Subscriptions.Where(g => g.GroupID == groupID).ToListAsync();
            StandarizedEmail email = new StandarizedEmail(1);
            email.setSubscriptionEmailContent(groupName, NewsLink);
            foreach(var sub in subs)
            {

                await sender.SendEmailAsync(sub.UserEmail, subject, email.getContent());
            }
            
        }
    }
}
