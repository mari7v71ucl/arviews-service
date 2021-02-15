using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using arviews_service.API.Controllers;
using arviews_service.API.Dtos;
using arviews_service.API.Infrastructure;
using arviews_service.API.Models;
using arviews_service.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace arviews_service.Tests
{
    public class ARConfigsControllerTests
    {
        IARConfigService _service;
        ARConfigsController _controller;

        public ARConfigsControllerTests()
        {
            _service = new MockARConfigService();

            IMapper _mapper = new MapperConfiguration(c =>
                c.AddProfile<MappingProfile>()).CreateMapper();

            _controller = new ARConfigsController(_service, (_mapper));
        }

        [Theory]
        [InlineData("validviewid")]
        public void Test_IdExists_ReturnsOk(string validViewId)
        {
            // Act
            var okResult = _controller.Get(validViewId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);

            var item = okResult.Result as OkObjectResult;
            Assert.IsType<ARConfigHistory>(item.Value);

            var configHistoryItem = item.Value as ARConfigHistory;
            Assert.Equal("validviewid", configHistoryItem.ViewId);
            Assert.Equal(2, configHistoryItem.Configs.Count);
            Assert.Equal("0.2", configHistoryItem.Configs[0].Properties["height"]);
        }

        [Theory]
        [InlineData("invalidviewid")]
        public void Test_IdNotExists_ReturnsNotFound(string invalidViewId)
        {
            // Act 
            var notFoundResult = _controller.Get(invalidViewId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult.Result);
        }

        [Fact]
        public void Test_AddARConfig_Valid()
        {
            // Arrange
            var completeConfig = new ARConfig()
            {
                ViewId = "WyrQ2WzBeM8",
                Properties = new Dictionary<string, string>()
                {
                    { "height", "0.3" },
                    { "width", "1.1" },
                    { "verticalOffset", "0.5" },
                    { "horizontalOffset", "0" },
                    { "perpendicularOffset", "-0.2" },
                    { "resolution", "1300" }
                }
            };

            // Act 
            var createdResponse = _controller.Post(completeConfig);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse.Result);

            var item = createdResponse.Result as CreatedAtActionResult;
            Assert.IsType<ARConfigDto>(item.Value);

            var configItem = item.Value as ARConfigDto;
            Assert.Equal(completeConfig.Properties.Count, configItem.Properties.Count);
            Assert.Equal(completeConfig.Properties["resolution"], configItem.Properties["resolution"]);
            Assert.NotNull(configItem.CreatedTimestamp);
        }

        [Fact]
        public void Test_AddARConfig_Invalid()
        {
            // Arrange
            var incompleteConfig = new ARConfig()
            {
                Properties = new Dictionary<string, string>()
                {
                    { "height", "0.1" },
                    { "width", "0.8" },
                    { "verticalOffset", "0" },
                }
            };

            // Act 
            _controller.ModelState.AddModelError("ViewId", "ViewId is a required field");
            var badResponse = _controller.Post(incompleteConfig);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }


    }
}
