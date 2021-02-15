using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using arviews_service.API.Models;

namespace arviews_service.API.Services
{
    public interface IWorkspaceService
    {
        public List<Workspace> Get();
        public Workspace GetByWorkspaceId(string id);
        public Workspace Create(Workspace w);
        public void Remove(string id);
    }
}
