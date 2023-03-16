using FluentAssertions;
using ResearchRepository.Domain.Core.Exceptions;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.ResearchGroups.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.Domain.ResearchGroups.Entities
{
    public class ResearchCenterTest
    {
        //global 
        private readonly RequiredString NameC = RequiredString.TryCreate("Name").Success();
        private readonly long Id = 1;
        private readonly string _descrip = "Descrip";
        private readonly string _image = "Image.png";
        private readonly string _abrvv = "CITIC";

        [Fact]
        public void CreateCenterWithoutIdAndGets()
        {
            //act
            var group = new ResearchCenter(NameC, _descrip, _image, _abrvv);

            //assert
            group.Name.Should().Be(NameC);
            group.Description.Should().Be(_descrip);
            group.ImageName.Should().Be(_image);
            group.Abbreviation.Should().Be(_abrvv);

        }

        [Fact]
        public void AddGroupToCenterWorks()
        {

            //arrange
            var center = new ResearchCenter(NameC, _descrip, _image, _abrvv);
            var group = new ResearchGroup(NameC, _descrip, _image, null, center );

            //act
            center.AddGroupToCenter(group);

            //assert
            center.ResearchGroups.Should().Contain(group);

        }

        [Fact]
        public void RemoveGroupFromCenterWorks()
        {

            //arrange
            var center = new ResearchCenter(NameC, _descrip, _image, _abrvv);
            var group = new ResearchGroup(NameC, _descrip, _image, null, center);
            center.AddGroupToCenter(group);

            //act
            center.RemoveGroupFromCenter(group);

            //assert
            center.ResearchGroups.Should().NotContain(group);

        }

    }
}
