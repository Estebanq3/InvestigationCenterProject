using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResearchRepository.Domain.WorkWithUs.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Domain.WorkWithUs.Repositories;
using ResearchRepository.Application.WorkWithUs.Implementations;
using Moq;
using Xunit;
using FluentAssertions;


namespace UnitTests.Application.WorkWithUs
{
    public class WorkWithUsServiceTest
    {
        private string Name = "Trabaje Con Nosotros";
        [Fact]
        public async Task GetAsyncInfoTest()
        {


            var mock = new Mock<IWorkWithUsRepository>();
            var workService = new WorkWithUsService(mock.Object);

            WorkInfo work = new WorkInfo { Name = "Trabaje Con Nosotros" };

            mock.Setup(r => r.GetAsyncInfo()).ReturnsAsync(work);

            var work2 = await workService.GetAsyncInfo();

            mock.Verify(repo => repo.GetAsyncInfo(), Times.Once);
            work.Name.Should().Be(Name);
        }




    }

}

