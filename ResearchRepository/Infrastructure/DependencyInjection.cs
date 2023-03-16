using ResearchRepository.Domain.Core.Repositories;
using ResearchRepository.Domain.People.Repositories;
using ResearchRepository.Infrastructure.People;
using ResearchRepository.Infrastructure.People.Repositories;
using ResearchRepository.Infrastructure.ResearchAreas.Repositories;
using ResearchRepository.Domain.ResearchAreas.Repositories;
using ResearchRepository.Domain.ResearchGroups.Repositories;
using ResearchRepository.Infrastructure.ResearchGroups;
using ResearchRepository.Infrastructure.ResearchGroups.Repositories;
using ResearchRepository.Infrastructure.ResearchAreas;
using ResearchRepository.Domain.Theses.Repositories;
using ResearchRepository.Infrastructure.Theses;
using ResearchRepository.Infrastructure.Theses.Repositories;

using ResearchRepository.Domain.WorkWithUs.Repositories;
using ResearchRepository.Infrastructure.WorkWithUs;
using ResearchRepository.Infrastructure.WorkWithUs.Repositories;

using ResearchRepository.Domain.Contacts.Repositories;
using ResearchRepository.Infrastructure.Contacts;
using ResearchRepository.Infrastructure.Contacts.Repositories;

using ResearchRepository.Domain.InvestigationProjects.Repositories;
using ResearchRepository.Infrastructure.InvestigationProjects;
using ResearchRepository.Infrastructure.InvestigationProjects.Repositories;

using ResearchRepository.Domain.PublicationContext;
using ResearchRepository.Domain.StatisticsContext;
using ResearchRepository.Infrastructure.PublicationContext;
using ResearchRepository.Infrastructure.PublicationContext.Repositories;
using ResearchRepository.Infrastructure.StatisticsContext;
using ResearchRepository.Infrastructure.StatisticsContext.Repositories;
using Infrastructure.PublicationContext;
using ResearchRepository.Domain.Repositories;

using ResearchRepository.Domain.Authorization.Repositories;
using ResearchRepository.Infrastructure.Authorization.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.ResearchNews.Repositories;
using ResearchRepository.Infrastructure.ResearchNews.Repositories;
using Microsoft.AspNetCore.Identity;
using ResearchRepository.Domain.PublicationContext.Repositories;
using ResearchRepository.Infrastructure.Authentication;

using ResearchRepository.Domain.Authentication.Repositories;
using ResearchRepository.Infrastructure.Authentication.Repositories;
using ResearchRepository.Infrastructure.Authorization;

using Microsoft.AspNetCore.Identity.UI.Services;

namespace ResearchRepository.Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Infrastructure Repositories Dependecy injection
        /// Sets which Repository to use and adds the Database Context
        /// Author: Nelson Alvarez
        /// StoryID: ST-MM-3.2
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, string connectionString, string authenticationConnectionString)
        {
            services.AddDbContext<ResearchGroupsDbContext>(options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging());
            services.AddScoped<IResearchCenterRepository, ResearchCenterRepository>();
            services.AddScoped<IResearchAreaRepository, ResearchAreaRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();

            services.AddScoped<IContactsRepository, ContactsRepository>();

            services.AddDbContext<InvestigationProjectsDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IInvestigationProjectsRepository, InvestigationProjectsRepository>();

            services.AddDbContext<WorkWithUsDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IWorkWithUsRepository, WorkWithUsRepository>();

            services.AddDbContext<ThesesDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IThesesRepository, ThesesRepository>();

            services.AddScoped<IResearchGroupRepository, ResearchGroupRepository>();
            services.AddDbContext<PeopleDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IAcademicProfileRepository, AcademicProfileRepository>();

            services.AddDbContext<AccountInfoDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddDbContext<People.SubscriptionsDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<ISubscriptionsRepository,People.Repositories.SubscriptionsRepository>();

            services.AddScoped<IPublicationRepository, PublicationRepository>();
            services.AddDbContext<PublicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IStatisticsRepository, StatisticsRepository>();
            services.AddDbContext<StatisticsDbContext>(options => options.UseSqlServer(connectionString));

            services.AddDbContext<AccountsDbContext>(options => options.UseSqlServer(authenticationConnectionString), ServiceLifetime.Transient);
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<AccountsDbContext>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();

            services.AddDbContext<PermissionsContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IPermissionsRepository, PermissionsRepository>();

            services.AddTransient<IEmailSender, EmailSender>();
            return services;
        }
    }
}
