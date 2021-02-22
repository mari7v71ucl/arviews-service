using System.Collections.Generic;
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
        IARConfigService _configService;
        IWorkspaceService _workspaceService;
        ARConfigsController _controller;

        public ARConfigsControllerTests()
        {
            _configService = new MockARConfigService();
            _workspaceService = new MockWorkspaceService();

            IMapper _mapper = new MapperConfiguration(c =>
                c.AddProfile<MappingProfile>()).CreateMapper();

            _controller = new ARConfigsController(_configService, _workspaceService, (_mapper));
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

        [Theory]
        [InlineData("forbiddenviewid")]
        public void Test_Get_IdAccessForbidden_ReturnsForbidden(string invalidViewId)
        {
            // Act 
            var forbidenReturnCode = _controller.Get(invalidViewId);

            // Assert
            Assert.IsType<StatusCodeResult>(forbidenReturnCode.Result);
            Assert.Equal((int)((StatusCodeResult)forbidenReturnCode.Result).StatusCode, 403);
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
            Assert.IsType<OkObjectResult>(createdResponse.Result);

            var item = createdResponse.Result as OkObjectResult;
            Assert.IsType<ARConfigDto>(item.Value);

            var configItem = item.Value as ARConfigDto;
            Assert.Equal(completeConfig.Properties.Count, configItem.Properties.Count);
            Assert.Equal(completeConfig.Properties["resolution"], configItem.Properties["resolution"]);
            Assert.NotNull(configItem.CreatedTimestamp);
        }

        [Theory]
        [InlineData("forbiddenviewid")]
        public void Test_Post_IdAccessForbidden_ReturnsForbidden(string invalidViewId)
        {
            // Arrange
            var forbiddenAccessConfig = new ARConfig()
            {
                ViewId = "forbiddenviewid",
                Properties = new Dictionary<string, string>()
                {
                    { "height", "0.1" },
                    { "width", "0.1" },
                    { "verticalOffset", "0" },
                }
            };

            // Act 
            var forbidenReturnCode = _controller.Post(forbiddenAccessConfig);

            // Assert
            Assert.IsType<StatusCodeResult>(forbidenReturnCode.Result);
            Assert.Equal(403, ((StatusCodeResult)forbidenReturnCode.Result).StatusCode);
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
