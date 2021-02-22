using System.Collections.Generic;
using System.Threading.Tasks;
using arviews_service.API.Models;
using arviews_service.API.Services;
using DnsClient.Protocol;
using Xunit;

namespace arviews_service.IntegrationTests
{
    public class DatabaseTests
    {
        [Fact]
        public async Task Test_CreateThenDeleteWorkspace()
        {
            // Arrange
            IWorkspaceService workspaceService = new WorkspaceService(ConfigurationHelper.GetDatabaseSettings());

            var workspaceItem = new Workspace()
            {
                WorkspaceId = "workspace123",
                ArViews = new List<string>()
                {
                    "view101", "view102", "view103"
                },
                IsClientAccessForbidden = false
            };

            // Act
            workspaceItem = workspaceService.Create(workspaceItem);

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(workspaceItem.Id));

            // Act
            var workspaceItemResult = workspaceService.GetById(workspaceItem.Id);

            // Assert
            Assert.NotNull(workspaceItemResult);
            Assert.Equal("workspace123", workspaceItemResult.WorkspaceId);
            Assert.Equal(3, workspaceItemResult.ArViews.Count);
            Assert.Contains("view103", workspaceItemResult.ArViews);
            Assert.False(workspaceItemResult.IsClientAccessForbidden);

            // Act
            workspaceService.Remove(workspaceItem.Id);
            workspaceItemResult = workspaceService.GetById(workspaceItem.Id);
            Assert.Null(workspaceItemResult);
        }

        [Fact]
        public async Task Test_CreateThenDeleteArConfigs()
        {
            // Arrange
            IARConfigService arConfigService = new ARConfigService(ConfigurationHelper.GetDatabaseSettings());

            var configItem = new ARConfig()
            {
                ViewId = "view100",
                Properties = new Dictionary<string, string>()
                {
                    {"height", "0.1"},
                    {"width", "0.2"}
                }
            };

            // Act
            configItem = arConfigService.Create(configItem);

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(configItem.Id));

            // Act
            var configListResult = arConfigService.GetByViewId("view100");

            // Assert
            Assert.NotNull(configListResult);
            Assert.NotEqual(0, configListResult.Count);
            Assert.Equal("view100", configListResult[0].ViewId);
            Assert.Equal(2, configListResult[0].Properties.Count);
            Assert.Equal("0.2", configListResult[0].Properties["width"]);

           // Act
           arConfigService.DeleteByViewId("view100");
           configListResult = arConfigService.GetByViewId("view100");

           // Assert
           Assert.Equal(0, configListResult.Count);
        }
    }
}
