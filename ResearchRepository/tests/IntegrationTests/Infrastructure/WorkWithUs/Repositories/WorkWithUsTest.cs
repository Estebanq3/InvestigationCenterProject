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
using ResearchRepository.Domain.WorkWithUs.Entities;
using ResearchRepository.Domain.WorkWithUs.Repositories;
using FluentAssertions;
using WebApplication_ResearchRepository;
using IntegrationTests.Infrastructure.WorkWithUs;

namespace IntegrationTests.Infrastructure.WorkWithUs.Repositories
{
    //ID: ST-PA-3.4, ST-PA-3.5, ST-PA-3.6
    //Tareas: Crear entidades, mapeos y servicios
    //Participantes: Andrea Alvarado y Sebastián Montero
    public class WorkWithUsTestClass :
    IClassFixture<WorkWithUsFactory<Startup>>
    {
        private readonly WorkWithUsFactory<Startup> _factory;
        public WorkWithUsTestClass(WorkWithUsFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetInfo()
        {
            string email = "correo@ucr.ac.cr";
            var repository = _factory.Server.Services.GetRequiredService<IWorkWithUsRepository>();
            var info = await repository.GetAsyncInfo();
            info.Email.Equals(email);
        }

    }
}
