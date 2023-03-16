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
    public class PersonServiceTest
    {

        private static string Email = "email@ucr.ac.cr";

        private static string FirstName = "Nombre";

        private static string FirstLastName = "Apellido1";

        private static string SecondLastName = "Apellido2";

        private static string Country = "Pais";

        private static int GroupId = 1;


        private static IList<Person> CreatePersonList()
        {
            List<Person> list = new List<Person>();
            for (int i = 1; i< 3; i++)
            {
                Person entity = new Person(Email, FirstName, FirstLastName, SecondLastName, Country);
                list.Add(entity);
            }
            return list;
        }

        private static IList<Student> CreateStudentList()
        {
            List<Student> list = new List<Student>();
            for (int i = 1; i < 3; i++)
            {
                Student entity = new Student(Email, FirstName, FirstLastName, SecondLastName, Country);
                list.Add(entity);
            }
            return list;
        }

        private static IList<Investigator> CreateInvestigatorList()
        {
            List<Investigator> list = new List<Investigator>();
            for (int i = 1; i < 3; i++)
            {
                Investigator entity = new Investigator(Email, FirstName, FirstLastName, SecondLastName, Country);
                list.Add(entity);
            }
            return list;
        }
        private static IList<InvestigatorManagesGroup> CreateInvestigatorManagesGroupList()
        {
            List<InvestigatorManagesGroup> list = new List<InvestigatorManagesGroup>();
            for (int i = 1; i < 3; i++)
            {
                InvestigatorManagesGroup entity = new InvestigatorManagesGroup(Email, GroupId);
                list.Add(entity);
            }
            return list;
        }


        [Fact]
        public async Task GetAsyncPersonTest()
        {
            var list = CreatePersonList();
            var mock = new Mock<IPersonRepository>();
            var personService = new PersonService(mock.Object);
            mock.Setup(r => r.GetAsyncPerson()).ReturnsAsync(list);
            
            //Act
            var persons = await personService.GetAsyncPerson();

            // assert
            persons.Should().BeEquivalentTo(list);

        }

        [Fact]
        public async Task GetAsyncStudentTest()
        {
            var list = CreateStudentList();
            var mock = new Mock<IPersonRepository>();
            var personService = new PersonService(mock.Object);
            mock.Setup(r => r.GetAsyncStudent()).ReturnsAsync(list);

            //Act
            var students = await personService.GetAsyncStudent();

            // assert
            students.Should().BeEquivalentTo(list);

        }

        [Fact]
        public async Task GetAsyncInvestigatorTest()
        {
            var list = CreateInvestigatorList();
            var mock = new Mock<IPersonRepository>();
            var personService = new PersonService(mock.Object);
            mock.Setup(r => r.GetAsyncInvestigator()).ReturnsAsync(list);

            //Act
            var investigators = await personService.GetAsyncInvestigator();

            // assert
            investigators.Should().BeEquivalentTo(list);

        }
        
        [Fact]
        public async Task GetAsyncInvestigatorManagesGroupTest()
        {
            var list = CreateInvestigatorManagesGroupList();
            var mock = new Mock<IPersonRepository>();
            var personService = new PersonService(mock.Object);
            mock.Setup(r => r.GetAsyncInvestigatorManagesGroup()).ReturnsAsync(list);

            //Act
            var investigatorsManages = await personService.GetAsyncInvestigatorManagesGroup();

            // assert
            investigatorsManages.Should().BeEquivalentTo(list);

        }




    }

}

