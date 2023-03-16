using Moq;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System;
using ResearchRepository.Application.Core.Utils;

namespace UnitTests.Application.Core
{
    public class MenuStateTests
    {
        [Fact]
        public void SetDiplayGroupMenuChangesValues()
        {
            //arrange
            var menuService = new MenuState();

            //act
            menuService.SetDisplayGroupMenu(true);

            //assert            
            menuService.GetDisplayGroupMenu().Should().BeTrue();
            menuService.getDisplayBack().Should().BeFalse();
        }

        [Fact]
        public void SetDiplayBackChangesValues()
        {
            //arrange
            var menuService = new MenuState();

            //act
            menuService.SetDisplayBack(true, "msj");

            //assert            
            menuService.GetDisplayGroupMenu().Should().BeFalse();
            menuService.getDisplayBack().Should().BeTrue();
            menuService.getDisplayBackMsg().Should().Be("msj");
        }


        [Fact]
        public void GetIdGroupReturnID()
        {
            //arrange
            var menuService = new MenuState();

            //act
            menuService.SetDisplayGroupMenu(true, 1);

            //assert            
            menuService.GetDisplayGroupMenu().Should().BeTrue();
            menuService.getDisplayBack().Should().BeFalse();
            menuService.GetIdGroup().Should().Be(1);
        }

        [Fact]
        public void SetOnChangeTest()
        {
            //arrange
            var menuService = new MenuState();
            Action? message = null;

            //act
            menuService.SetOnChange(message);
        }

        [Fact]
        public void UnsetOnChangeTest()
        {
            //arrange
            var menuService = new MenuState();
            Action? message = null;

            //act
            menuService.UnsetOnChange(message);
        }
    }
}
