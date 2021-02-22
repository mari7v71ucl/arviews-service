using System;
using System.Collections.Generic;
using System.Linq;
using arviews_service.API.Models;

namespace arviews_service.API.Services
{
    public class MockARConfigService : IARConfigService
    {
        public List<ARConfig> configs;

        public MockARConfigService()
        {
            configs = new List<ARConfig>()
                {
                    new ARConfig()
                    {
                        Id = "507f1f77bcf86cd799439011",
                        ViewId = "validviewid",
                        CreatedTimestamp = DateTime.Now,
                        Properties = new Dictionary<string, string>()
                        {
                            {"height", "0.2"},
                            {"width", "0.2"},
                            {"verticalOffset", "0.15"},
                            {"horizontalOffset", "0.08"},
                            {"perpendicularOffset", "-0.1"}
                        }
                    },
                    new ARConfig()
                    {
                        Id = "508b1f77bcf86cd7994750376",
                        ViewId = "validviewid",
                        CreatedTimestamp = DateTime.Now - TimeSpan.FromMinutes(1),
                        Properties = new Dictionary<string, string>()
                        {
                            {"height", "0.3"},
                            {"width", "0.4"},
                            {"verticalOffset", "0"},
                            {"horizontalOffset", "0.16"},
                            {"perpendicularOffset", "-0.2"}
                        }
                    },
                    new ARConfig()
                    {
                        Id = "375df77bcf86cd7994750724",
                        ViewId = "forbiddenviewid",
                        CreatedTimestamp = DateTime.Now - TimeSpan.FromMinutes(2),
                        Properties = new Dictionary<string, string>()
                        {
                            {"height", "0.1"},
                            {"width", "0.1"},
                            {"verticalOffset", "0"},
                            {"horizontalOffset", "0.2"},
                            {"perpendicularOffset", "0"}
                        }
                    }
                };
        }

        public List<ARConfig> GetByViewId(string viewId, int count)
        {
            if (count == 0)
            {
                return configs.Where(c => c.ViewId == viewId).ToList();
            }
            else if (count > 0)
            {
               return configs.Where(c => c.ViewId == viewId).Take(count).ToList();
            }

            return new List<ARConfig>();
        }

        public ARConfig Create(ARConfig config)
        {
            config.Id = "375df77bcf86cj49564750724";
            config.CreatedTimestamp = DateTime.Now.ToUniversalTime();

            return config;
        }

        public void DeleteByViewId(string wId)
        {
            configs.RemoveAll(x => x.ViewId == wId);
        }
    }
}
