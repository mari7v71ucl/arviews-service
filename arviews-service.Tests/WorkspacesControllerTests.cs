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
    public class WorkspacesControllerTests
    {
        private IWorkspaceService _service;
        private WorkspacesController _controller;

        public WorkspacesControllerTests()
        {
            _service = new MockWorkspaceService();

            IMapper _mapper = new MapperConfiguration(c =>
                c.AddProfile<MappingProfile>()).CreateMapper();

            _controller = new WorkspacesController(_service, _mapper);
        }

        [Fact]
        public void Test_WithoutContents_ReturnsOk()
        {
            // Act
            var okResult = _controller.Get();

            Assert.IsType<OkObjectResult>(okResult.Result);

            var item = okResult.Result as OkObjectResult;
            Assert.IsType<List<WorkspaceDto>>(item.Value);

            var workspaces = item.Value as List<WorkspaceDto>;
            Assert.Equal(4, workspaces.Count);
            Assert.Equal("workspace003", workspaces[2].WorkspaceId);
        }

        [Fact]
        public void Test_WithContents_ReturnsOk()
        {
            // Act
            var okResult = _controller.Get(true);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);

            var item = okResult.Result as OkObjectResult;
            Assert.IsType<List<Workspace>>(item.Value);

            var workspaces = item.Value as List<Workspace>;
            Assert.Equal(4, workspaces.Count);
            Assert.Equal("workspace003", workspaces[2].WorkspaceId);
            Assert.Equal("view009", workspaces[2].ArViews[2]);
        }

        [Theory]
        [InlineData("workspace001")]
        public void Test_Get_IdExists_ReturnsOk(string workspaceId)
        {
            // Act 
            var okResult = _controller.GetByWId(workspaceId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            var item = okResult.Result as OkObjectResult;
            Assert.IsType<Workspace>(item.Value);

            var workspace = item.Value as Workspace;
            Assert.Equal(workspaceId, workspace.WorkspaceId);
            Assert.Equal(3, workspace.ArViews.Count);
            Assert.Equal("view003", workspace.ArViews[2]);
        }

        [Theory]
        [InlineData("nonexistent")]
        public void Test_Get_IdNotExists_ReturnsNotFound(string workspaceId)
        {
            // Act
            var notFoundResult = _controller.GetByWId(workspaceId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult.Result);
        }

        [Fact]
        public void Test_CreateWorkspace_Valid()
        {
            // Arrange
            var validWorkspace = new Workspace()
            {
                WorkspaceId = "workspace999",
                ArViews = new List<string>()
                {
                    "view111", "view112", "view113"
                }
            };

            // Act
            var createdResponse = _controller.Post(validWorkspace);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse.Result);

            var item = createdResponse.Result as CreatedAtActionResult;
            Assert.IsType<Workspace>(item.Value);

            var workspaceItem = item.Value as Workspace;
            Assert.Equal("workspace999", workspaceItem.WorkspaceId);
            Assert.Equal(3, workspaceItem.ArViews.Count);
            Assert.Equal("view113", workspaceItem.ArViews[2]);
        }

        [Fact]
        public void Test_CreateWorkspace_Invalid()
        {
            // Arrange
            var invalidWorkspace = new Workspace()
            {
                ArViews = new List<string>()
                {
                    "view111", "view112", "view113"
                }
            };

            // Act
            _controller.ModelState.AddModelError("WorkspaceId", "WorkspaceId is a required field");
            var badResponse = _controller.Post(invalidWorkspace);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }

        [Theory]
        [InlineData("workspace001", "nonexistent")]
        public void Test_DeleteWorkspace(string validWId, string invalidWId)
        {
            // Act
            var notFoundResult = _controller.DeleteByWId(invalidWId);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
            Assert.Equal(4, _service.Get().Count);

            // Act
            var okResult = _controller.DeleteByWId(validWId);

            // Assert
            Assert.IsType<OkResult>(okResult.Result);
            Assert.Equal(3, _service.Get().Count);
        }
    }
}
