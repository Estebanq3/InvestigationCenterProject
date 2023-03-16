using FluentAssertions;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.ResearchGroups.Entities;
using ResearchRepository.Presentation.ResearchGroups.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UITests.Presentation.ResearchGroups.Models
{
    public class GroupModelTests
    {
        private readonly int id = 1;
        private readonly string Name = "Nombre";
        private readonly string Desc = "Desc";
        private readonly string Image = "image.png";
        private readonly DateTime Date = DateTime.Now;

        [Fact]
        public void CreateGroupModelWithoutIdAndGets()
        {
            //arrange
            ResearchCenter _center = new ResearchCenter(RequiredString.TryCreate(Name, 200).Success(), Desc, null, null);

            //act
            var group = new GroupModel(Name, Desc, Image, Date, _center);

            //assert
            group.id.Should().BeNull();
            group.Name.Should().Be(Name);
            group.ImageName.Should().Be(Image);
            group.Description.Should().Be(Desc);
            group.CreationDate.Should().Be(Date);
            group.Center.Should().Be(_center);
            group.AdminEmail.Should().BeNull();
            group.Active.Should().BeFalse();
        }

        [Fact]
        public void CreateGroupModelEmpty()
        {
            //arrange

            //act
            var group = new GroupModel();

            //assert
            group.id.Should().BeNull();
            group.Name.Should().BeNull();
            group.ImageName.Should().BeNull();
            group.Description.Should().BeNull();
            group.CreationDate.Should().Be(default);
            group.Center.Should().BeNull();
            group.AdminEmail.Should().BeNull();
            group.Active.Should().BeFalse();
        }

        [Fact]
        public void CreateGroupModelEmptyWithSets()
        {
            //arrange
            ResearchCenter _center = new ResearchCenter(RequiredString.TryCreate(Name, 200).Success(), Desc, null, null);
            var group = new GroupModel();

            //act
            group.id = id;
            group.Name = Name;
            group.Description = Desc;
            group.ImageName = Image;
            group.CreationDate = Date;
            group.Center = _center;
            group.AdminEmail = "admin";
            group.Active = true;

            //assert
            group.id.Should().Be(1);
            group.Name.Should().Be(Name);
            group.ImageName.Should().Be(Image);
            group.Description.Should().Be(Desc);
            group.CreationDate.Should().Be(Date);
            group.Center.Should().Be(_center);
            group.AdminEmail.Should().Be("admin");
            group.Active.Should().BeTrue();
        }
    }
}
