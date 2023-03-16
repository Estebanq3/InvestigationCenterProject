using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using ResearchRepository.Application.Core.Utils.Implementatios;
using FluentAssertions;
using System.Collections.Generic;
using System;

namespace UnitTests.Application.Core
{
    public class WebConfigServiceTests
    {
        private Mock<IConfiguration> config = new();
        private Mock<IConfigurationSection> configSection = new();
        private readonly Dictionary<string, string> inMemorySettings = new()
        {
            {"WebParams:prueba", "string" },
            {"WebParams:pruebaNum", "2021" },
            {"WebParams:pruebaNumFloat", "20.21" },
            {"WebParams:pruebaNumNegative", "-20" }

        };

        [Fact]
        public void GetStringValueEmptyReturnsEmptyString()
        {
            //arrange
            configSection.Setup(x => x.Value).Returns(string.Empty);
            config.Setup(x => x.GetSection("WebParams")).Returns(configSection.Object);
            var webConfigService = new WebConfigService(config.Object);

            //act
            var result = webConfigService.GetStringValue(string.Empty);

            //assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetStringValueNullReturnsEmptyString()
        {
            //arrange
            configSection.Setup(x => x.Value).Returns(string.Empty);
            config.Setup(x => x.GetSection("WebParams")).Returns(configSection.Object);
            var webConfigService = new WebConfigService(config.Object);

            //act
            var result = webConfigService.GetStringValue(null);

            //assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetStringValueReturnsString()
        {
            //arrange
            var expected = "string";
            IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            var webConfigService = new WebConfigService(config);

            //act
            var result = webConfigService.GetStringValue("prueba");

            //assert
            result.Should().Be(expected);
        }

        [Fact]
        public void GetStringNumValueReturnsString()
        {
            //arrange
            var expected = "2021";
            IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            var webConfigService = new WebConfigService(config);

            //act
            var result = webConfigService.GetStringValue("pruebaNum");

            //assert
            result.Should().Be(expected);
        }

        [Fact]
        public void GetIntValueEmptyReturnsZero()
        {
            //arrange
            configSection.Setup(x => x.Value).Returns(string.Empty);
            config.Setup(x => x.GetSection("WebParams")).Returns(configSection.Object);
            var webConfigService = new WebConfigService(config.Object);

            //act
            var result = webConfigService.GetIntValue(string.Empty);

            //assert
            result.Should().Be(0);
        }

        [Fact]
        public void GetIntValueNullReturnsZero()
        {
            //arrange
            configSection.Setup(x => x.Value).Returns(string.Empty);
            config.Setup(x => x.GetSection("WebParams")).Returns(configSection.Object);
            var webConfigService = new WebConfigService(config.Object);

            //act
            var result = webConfigService.GetIntValue(null);

            //assert
            result.Should().Be(0);
        }

        [Fact]
        public void GetIntValueInvalidReturnsZero()
        {
            //arrange
            var expected = 0;
            IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            var webConfigService = new WebConfigService(config);

            //act
            var result = webConfigService.GetIntValue("prueba");

            //assert
            result.Should().Be(expected);
        }

        [Fact]
        public void GetIntValueValidReturnsInt()
        {
            //arrange
            var expected = 2021;
            IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            var webConfigService = new WebConfigService(config);

            //act
            var result = webConfigService.GetIntValue("pruebaNum");

            //assert
            result.Should().Be(expected);
        }

        [Fact]
        public void GetIntValueFloatReturnsZero()
        {
            //arrange
            var expected = 0;
            IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            var webConfigService = new WebConfigService(config);

            //act
            var result = webConfigService.GetIntValue("pruebaNumFloat");

            //assert
            result.Should().Be(expected);
        }

        [Fact]
        public void GetIntValueNegativeReturnsNegativeInt()
        {
            //arrange
            var expected = -20;
            IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            var webConfigService = new WebConfigService(config);

            //act
            var result = webConfigService.GetIntValue("pruebaNumNegative");

            //assert
            result.Should().Be(expected);
        }

        [Fact]
        public void ValueExistValidReturnsTrue()
        {
            //arrange
            IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            var webConfigService = new WebConfigService(config);

            //act
            var result = webConfigService.ValueExists("pruebaNum");

            //assert
            result.Should().BeTrue();
        }

        [Fact]
        public void ValueExistInvalidReturnsFalse()
        {
            //arrange
            IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            var webConfigService = new WebConfigService(config);

            //act
            var result = webConfigService.ValueExists("");

            //assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ValueExistNullReturnsFalse()
        {
            //arrange
            IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            var webConfigService = new WebConfigService(config);

            //act
            var result = webConfigService.ValueExists(null);

            //assert
            result.Should().BeFalse();
        }

        [Fact]        
        public void InvalidSectionThrowsError()
        {
            configSection.Setup(x => x.Value).Returns("Nothing");
            config.Setup(x => x.GetSection("InvalidSection")).Returns(configSection.Object);
            try
            {
                var webConfigService = new WebConfigService(config.Object);
            }
            catch (ArgumentException ex)
            {
                Assert.Equal("Section: 'WebParams' doesn't exist in appsettings.json", ex.Message);
            }
        }

    }
}
