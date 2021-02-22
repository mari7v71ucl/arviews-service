using System.Collections.Generic;
using arviews_service.API.Models;

namespace arviews_service.API.Services
{
    public interface IWorkspaceService
    {
        public List<Workspace> Get();
        public Workspace GetByWorkspaceId(string id);
        public Workspace GetById(string id);
        public Workspace Create(Workspace w);
        public void Remove(string id);
        public bool AccessAllowed(string viewId);
    }
}
