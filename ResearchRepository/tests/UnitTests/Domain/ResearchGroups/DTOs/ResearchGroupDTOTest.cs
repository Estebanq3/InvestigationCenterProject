using FluentAssertions;
using ResearchRepository.Domain.Core.Exceptions;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.ResearchGroups.DTOs;
using ResearchRepository.Domain.ResearchGroups.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.Domain.ResearchGroups.DTOs
{
    public class ResearchGroupDTOTest
    {
        //global 
        private readonly RequiredString Name = RequiredString.TryCreate("Name").Success();
        private readonly long Id = 1;

        [Fact]
        public void ContructorWorks()
        {
            //act
            var group = new ResearchGroupDTO(Id, Name);

            //assert

            group.Id.Should().Be(Id);
            group.Name.Should().Be(Name);

        }

        [Fact]
        public void EmptyContructorWorks()
        {
            //act
            var group = new ResearchGroupDTO(Id, null);

            //assert

            group.Id.Should().Be(Id);
            group.Name.Should().Be(null);

        }


    }
}
