using System;
using System.Collections.Generic;
using arviews_service.API.Models;
using MongoDB.Driver;

namespace arviews_service.API.Services
{
    public class ARConfigService : IARConfigService
    {
        private readonly IMongoCollection<ARConfig> _arConfigs;

        public ARConfigService(IArViewsServiceDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _arConfigs = database.GetCollection<ARConfig>(settings.ArConfigsCollectionName);
        }

        public List<ARConfig> GetByViewId(string viewId, int count = 0)
        {
            List<ARConfig> configs = new List<ARConfig>();

            if (count > 0)
            {
                configs = _arConfigs.Find(c => c.ViewId == viewId)
                    .SortByDescending(c => c.CreatedTimestamp).Limit(count).ToList();
            }
            else if (count == 0)
            {
                configs = _arConfigs.Find(c => c.ViewId == viewId)
                    .SortByDescending(c => c.CreatedTimestamp).ToList();
            }

            return configs;
        }

        public ARConfig Create(ARConfig config)
        {
            config.CreatedTimestamp = DateTime.Now.ToUniversalTime();
            _arConfigs.InsertOneAsync(config);
            return config;
        }

        public void DeleteByViewId(string wId)
        {
            _arConfigs.DeleteMany(c => c.ViewId == wId);
        }
    }
}
