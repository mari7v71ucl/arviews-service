using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using arviews_service.API.Models;

namespace arviews_service.API.Services
{
    public interface IARConfigService
    {
        public List<ARConfig> GetByViewId(string viewId, int count);

        public ARConfig Create(ARConfig config);
    }
}
