using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.People;
using ResearchRepository.Domain.People.Entities;
using ResearchRepository.Domain.People.Repositories;
using FluentAssertions;
using WebApplication_ResearchRepository;
using IntegrationTests.Infrastructure.People;

namespace IntegrationTests.Infrastructure.People.Repositories
{
    public class SubscriptionRepositoryIntegrationTestClass :
    IClassFixture<SubscriptionFactory<Startup>>
    {
        private readonly SubscriptionFactory<Startup> _factory;
        public
        SubscriptionRepositoryIntegrationTestClass(SubscriptionFactory<Startup>
        factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task InsertSubscription()
        {
            // arrange
            Subscriptions sub = new Subscriptions(1, "GREIVIN.SANCHEZGARITA@ucr.ac.cr");
            var repository = _factory.Server.Services.GetRequiredService<ISubscriptionsRepository>();
            //act
            await repository.AddSubscription("GREIVIN.SANCHEZGARITA@ucr.ac.cr",1);
            var list = await repository.FindSubscription("GREIVIN.SANCHEZGARITA@ucr.ac.cr", 1);
            //assert
            list[0].GroupID.Equals(sub.GroupID);
            list[0].UserEmail.Equals(sub.UserEmail);
        }

        [Fact]
        public async Task DeleteSubscription()
        {
            // arrange
            Subscriptions sub = new Subscriptions(1, "GREIVIN.SANCHEZGARITA@ucr.ac.cr");
            var repository = _factory.Server.Services.GetRequiredService<ISubscriptionsRepository>();
            //act
            await repository.AddSubscription("GREIVIN.SANCHEZGARITA@ucr.ac.cr", 1);
            await repository.DeleteSubscription("GREIVIN.SANCHEZGARITA@ucr.ac.cr", 1);
            var list = await repository.FindSubscription("GREIVIN.SANCHEZGARITA@ucr.ac.cr", 1);
            var count = list.Count();
            //assert
            count.Equals(0);
        }



    }
}
