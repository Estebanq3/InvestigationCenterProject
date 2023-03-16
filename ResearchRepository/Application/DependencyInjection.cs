using ResearchRepository.Application.ResearchAreas;
using ResearchRepository.Application.ResearchAreas.Implementations;
using ResearchRepository.Application.ResearchGroups;
using ResearchRepository.Application.ResearchGroups.Implementations;
using ResearchRepository.Application.Core.Utils;
using Microsoft.Extensions.DependencyInjection;
using ResearchRepository.Application.PublicationContext;
using ResearchRepository.Application.PublicationContext.Implementation;
using ResearchRepository.Application.StatisticsContext;
using ResearchRepository.Application.Authorization;
using ResearchRepository.Application.Authorization.Implementations;
using ResearchRepository.Application.StatisticsContext.Implementation;
using ResearchRepository.Application.InvestigationProjects;
using ResearchRepository.Application.InvestigationProjects.Implementations;
using ResearchRepository.Application.Theses;
using ResearchRepository.Application.Theses.Implementations;
using ResearchRepository.Application.People;
using ResearchRepository.Application.People.Implementations;
using ResearchRepository.Application.WorkWithUs;
using ResearchRepository.Application.WorkWithUs.Implementations;
using ResearchRepository.Application.Authentication.Implementations;
using ResearchRepository.Application.Authentication;
using ResearchRepository.Application.Authentication.Implementations;
using ResearchRepository.Application.Contacts;
using ResearchRepository.Application.Contacts.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Application.ResearchNews;
using ResearchRepository.Application.ResearchNews.Implementations;
using ResearchRepository.Application.Core.Utils.Implementatios;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<IResearchAreaService, ResearchAreaService>();
            services.AddTransient<IResearchCenterService, ResearchCenterService>();
            services.AddTransient<IInvestigationProjectService, InvestigationProjectService>();
            services.AddTransient<IThesisService, ThesisService>();
            services.AddTransient<IResearchGroupService, ResearchGroupService>();
            services.AddTransient<IResearcherService, ResearcherService>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IAcademicProfileService, AcademicProfileService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IPublicationService, PublicationService>();
            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IAuthorizationServices, AuthorizationServices>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IPermissionsService, PermissionsService>();
            services.AddTransient<IContactsService, ContactsService>();
            services.AddTransient<IWorkWithUsService, WorkWithUsService>();

            services.AddScoped<IMenuState, MenuState>();
            services.AddScoped<IWebConfigService, WebConfigService>();
            services.AddTransient<IContactEmailService, ContactEmailService>();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddTransient<ISubscriptionsService, SubscriptionsService>();
            return services;
        }
    }
}
