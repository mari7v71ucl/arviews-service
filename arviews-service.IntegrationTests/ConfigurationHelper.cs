using arviews_service.API;
using arviews_service.API.Models;
using Microsoft.Extensions.Configuration;

namespace arviews_service.IntegrationTests
{
    public class ConfigurationHelper
    {
        public static ArViewsServiceDatabaseSettings GetDatabaseSettings()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddUserSecrets<DatabaseTests>()
                .Build();

            var settings = new ArViewsServiceDatabaseSettings()
            {
                ArConfigsCollectionName = configuration["ArViewsServiceDatabaseSettings:ArConfigsCollectionName"],
                WorkspacesCollectionName = configuration["ArViewsServiceDatabaseSettings:WorkspacesCollectionName"],
                ConnectionString = configuration["ArViewsServiceDatabaseSettings:ConnectionString"],
                DatabaseName = configuration["ArViewsServiceDatabaseSettings:DatabaseName"]
            };

            return settings;
        }
    }
}
