using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using arviews_service.API.Models;

namespace arviews_service.API.Services
{
    public class MockARConfigService : IARConfigService
    {
        public List<ARConfig> GetByViewId(string viewId, int count)
        {
            var configs = new List<ARConfig>();

            if (viewId == "validviewid")
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
                    }
                };
            }

            return configs;
        }

        public ARConfig Create(ARConfig config)
        {
            config.CreatedTimestamp = DateTime.Now.ToUniversalTime();

            return config;
        }
    }
}
