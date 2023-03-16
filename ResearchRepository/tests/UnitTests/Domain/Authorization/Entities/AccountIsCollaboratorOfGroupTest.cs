using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ResearchRepository.Domain.Authorization.Entities;
using FluentAssertions;

namespace UnitTests.Domain.Authorization.Entities
{
    public class AccountIsCollaboratorOfGroupTest
    {
        private static string Email = "email@ucr.ac.cr";
        private static int GroupId = 1;


        [Fact]
        public void EmailEmptyReturEmpty()
        {
            //arrange
            var input = string.Empty;

            //act
            var result = new AccountIsCollaboratorOfGroup(input, GroupId);

            //assert
            result.Email.Should().Be(string.Empty);
        }

        [Fact]
        public void EmailNullReturnNull()
        {
            //arrange
            string? input = null;

            //act
            var result = new AccountIsCollaboratorOfGroup(input, GroupId);

            //assert
            result.Email.Should().Be(null);
        }

        [Fact]
        public void ConstructorGroupIdReturnsGroupId()
        {


            //act
            var result = new AccountIsCollaboratorOfGroup(Email, GroupId);

            //assert
            result.GroupId.Should().Be(GroupId);
        }
    }
}
