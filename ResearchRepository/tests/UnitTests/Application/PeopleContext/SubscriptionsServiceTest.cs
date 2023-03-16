using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.People.Repositories;
using ResearchRepository.Application.People.Implementations;
using Moq;
using Xunit;
using FluentAssertions;


namespace UnitTests.Application.PeopleContext
{
    public class SubscriptionsServiceTest
    {

        private static string _userEmail = "gruposInvestigUCR@ucr.ac.cr";
        private static int _groupID= 1;
        private static IList<Subscriptions> CreateSubscriptionsList()
        {
            var list = new List<Subscriptions>();
            for (int i=1; i < 10; i++)
            {
                var sub = new Subscriptions();
                sub.GroupID = i;
                sub.UserEmail = _userEmail;
                list.Add(sub);
            }
            return list;
        }


        [Fact]
        public async Task AddSubscriptionTest()      {
            var sub = new Subscriptions();
            sub.GroupID = _groupID;
            sub.UserEmail = _userEmail;

            var mock = new Mock<ISubscriptionsRepository>();
            var SubService = new SubscriptionsService(mock.Object);
            
            mock.Setup(r => r.AddSubscription(_userEmail,_groupID));
    
            await SubService.AddSubscription(_userEmail, _groupID);
            sub.Should().Be(sub);
            mock.Verify(repo => repo.AddSubscription(_userEmail, _groupID), Times.Once);
        }
        
        [Fact]
        public async Task DeleteSubscriptionTest()
        {
            var sub = new Subscriptions();
            Person person = new Person("email", "firstName", "firstLastName", "SecondLastName", "Country");
            int groupId = 1;

            sub.UserEmail = person.Email;
            sub.GroupID = groupId;

            var mockSubsRepository = new Mock<ISubscriptionsRepository>();
            var subscriptionService = new SubscriptionsService(mockSubsRepository.Object);
            await subscriptionService.AddSubscription(person.Email, groupId);
            await subscriptionService.DeleteSubscription(person.Email, groupId);

            person.Subscriptions.Should().BeNull();

            mockSubsRepository.Verify(repo => repo.DeleteSubscription(person.Email, groupId), Times.Once);
        }

        [Fact]
        public async Task FindSubscriptionTest()
        {
            var List = CreateSubscriptionsList();//10
            var subList = new List<Subscriptions>();
            var sub = new Subscriptions();
            sub.UserEmail = _userEmail;
            sub.GroupID = _groupID;
            

            subList.Add(sub);//1

            var mock = new Mock<ISubscriptionsRepository>();
            mock.Setup(r => r.FindSubscription(_userEmail, _groupID));
            var SubService = new SubscriptionsService(mock.Object);
            await SubService.AddSubscription(_userEmail, _groupID);
            var results= await SubService.FindSubscription(_userEmail, _groupID);
            //ASSERT
            List.Should().NotHaveSameCount(subList);
        }
    }
}
