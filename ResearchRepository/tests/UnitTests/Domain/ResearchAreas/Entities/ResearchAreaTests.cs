using FluentAssertions;
using ResearchRepository.Domain.Core.Exceptions;
using ResearchRepository.Domain.Core.Helpers;
using ResearchRepository.Domain.Core.ValueObjects;
using ResearchRepository.Domain.ResearchAreas.Entities;
using ResearchRepository.Domain.ResearchGroups.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.Domain.ResearchAreas.Entities
{
    public class ResearchAreaTests
    {
        private static readonly RequiredString Name = RequiredString.TryCreate("Nombre").Success();
        private static readonly RequiredString SubAreaName = RequiredString.TryCreate("Nombre").Success();
        private static readonly String? Description = "Esto es una descripcion";
        private static readonly string? Image = "/img/imagen.png";


        private IEnumerable<ResearchGroup> GetGroups()
        {
            for (var i = 1; i <= 5; ++i)
            {
                var center = new ResearchCenter(Name, Description, Image, "Citic");
                yield return new ResearchGroup(Name, Description, Image, DateTime.Now, center);
            }
        }

        [Fact]
        public void NewAreaWithoutGroup()
        {
            // act
            var area = new ResearchArea(Name, Description);
            // assert
            area.ResearchGroups.Should().BeEmpty();
        }

        [Fact]
        public void NewAreaWithIdWithoutGroup()
        {
            // act
            var area = new ResearchArea(1, Name, Description);
            // assert
            area.ResearchGroups.Should().BeEmpty();
        }

        [Fact]
        public void AddOneAreaShouldNotThrow()
        {
            // arrange
            var area = new ResearchArea(Name, Description);
            // act
            var group = GetGroups().First();
            // assert
            area.Invoking(t => t.AddResearchGroup(group)).Should().NotThrow();
        }

        [Fact]
        public void AddSameGroupShouldThrow()
        {
            // arrange
            var area = new ResearchArea(Name, Description);
            // act
            var group = GetGroups().First();
            area.AddResearchGroup(group);
            // assert
            area.Invoking(t => t.AddResearchGroup(group)).Should().Throw<DomainException>().WithMessage("Research group is already in the area");
        }

        [Fact]
        public void DeleteAreaShouldNotThrow()
        {
            // arrange
            var area = new ResearchArea(Name, Description);
            // act
            var group = GetGroups().First();
            area.AddResearchGroup(group);
            // assert
            area.Invoking(t => t.RemoveResearchGroup(group)).Should().NotThrow();
        }

        [Fact]
        public void AddSubAreaShouldNotThrow()
        {
            // arrange
            var area = new ResearchArea(Name, Description);

            // act
            var subrea = new ResearchArea(SubAreaName, Description);

            // assert
            area.Invoking(t => t.AddSubArea(subrea)).Should().NotThrow();
        }

        [Fact]
        public void AddSameSubAreaShouldThrow()
        {
            // arrange
            var area = new ResearchArea(Name, Description);

            // act
            var subarea = new ResearchArea(Name, Description);
            area.AddSubArea(subarea);
            // assert
            area.Invoking(t => t.AddSubArea(subarea)).Should().Throw<DomainException>().WithMessage("Research sub-area is already added");
        }

        [Fact]
        public void RemoveSubAreaShouldNotThrow()
        {
            // arrange
            var area = new ResearchArea(Name, Description);

            // act
            var subarea = new ResearchArea(Name, Description);
            area.AddSubArea(subarea);
            // assert
            area.Invoking(t => t.RemoveSubArea(subarea)).Should().NotThrow();
        }

        [Fact]
        public void RemoveNotASubAreaShouldThrow()
        {
            // arrange
            var area = new ResearchArea(Name, Description);
            var subarea = new ResearchArea(Name, Description);

            // assert
            area.Invoking(t => t.RemoveSubArea(subarea)).Should().Throw<DomainException>().WithMessage("Not a research sub-area");
        }

        [Fact]
        public void AddParentAreaShouldNotThrow()
        {
            // arrange
            var area = new ResearchArea(Name, Description);
            var subarea = new ResearchArea(Name, Description);

            subarea.Invoking(t => t.AddParentArea(area)).Should().NotThrow();
        }


        [Fact]
        public void RemoveParentAreaShouldNotThrow()
        {
            // arrange
            var area = new ResearchArea(Name, Description);

            // act
            var subarea = new ResearchArea(Name, Description);
            subarea.AddParentArea(area);

            subarea.Invoking(t => t.RemoveParentArea(area)).Should().NotThrow();
        }

        [Fact]
        public void RemoveNotParentAreaShouldThrow()
        {
            // arrange
            var area = new ResearchArea(Name, Description);

            // act
            var subarea = new ResearchArea(Name, Description);
            subarea.AddParentArea(area);

            area.Invoking(t => t.RemoveParentArea(subarea)).Should().Throw<DomainException>().WithMessage("It's not a parent of the subarea");
        }

        [Fact]
        public void CompareToShouldwork()
        {
            // arrange
            var area = new ResearchArea(Name, Description);

            // act
            var subarea = new ResearchArea(Name, Description);

            // assert
            area.CompareTo(subarea).Should().Be(0);
        }


        [Fact]
        public void GetSubAreasCountReturnsCount()
        {
            // arrange
            var area = new ResearchArea(Name, Description);
            var subarea = new ResearchArea(Name, Description);

            // act
            area.AddSubArea(subarea);

            // assert
            area.GetSubAreasCount().Should().Be(1);
        }

        [Fact]
        public void UpdateNameChangesName()
        {
            // arrange
            var area = new ResearchArea(Name, Description);
            var newName  = RequiredString.TryCreate("Nombre2").Success();

            // act
            area.UpdateName("Nombre2");

            // assert
            area.Name.Should().Be(newName);
        }

        [Fact]
        public void UpdateNameInvalidShouldThrow()
        {
            // arrange
            var area = new ResearchArea(Name, Description);

            // assert
            area.Invoking(t => t.UpdateName("")).Should().Throw<DomainException>().WithMessage("Not a valid name");            
        }

        [Fact]
        public void ToStringReturnsName()
        {
            // arrange
            var area = new ResearchArea(Name, Description);

            // act
            var name = area.ToString();

            // assert
            name.Should().Be("Nombre");
        }

        [Fact]
        public void GetHashCodeReturnsHashCode()
        {
            // arrange
            var area = new ResearchArea(1, Name, Description);
            var expected = HashCode.Combine(1, Name);

            // act
            var code = area.GetHashCode();

            // assert
            code.Should().Be(expected);
        }

        [Fact]
        public void AddParentAreasWork()
        {
            // arrange
            var area = new ResearchArea(1, Name, Description);
            var subareas = new List<ResearchArea>()
            {
                new ResearchArea(2, Name, Description),
                new ResearchArea(3, Name, Description)
            };

            // act
            area.AddParentAreas(subareas);

            // assert
            area.ResearchAreas.Should().Contain(subareas);
        }
    }
}
