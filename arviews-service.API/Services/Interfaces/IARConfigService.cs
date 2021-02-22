using System.Collections.Generic;
using arviews_service.API.Models;

namespace arviews_service.API.Services
{
    public interface IARConfigService
    {
        public List<ARConfig> GetByViewId(string viewId, int count = 0);

        public ARConfig Create(ARConfig config);
        public void DeleteByViewId(string wId);
    }
}
