using System.Collections.Generic;
using arviews_service.API.Models;
using MongoDB.Bson.IO;
using MongoDB.Driver;

namespace arviews_service.API.Services
{
    public class WorkspaceService : IWorkspaceService
    {
        private readonly IMongoCollection<Workspace> _workspaces;

        public WorkspaceService(IArViewsServiceDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _workspaces = database.GetCollection<Workspace>(settings.WorkspacesCollectionName);
        }


        public List<Workspace> Get() =>
            _workspaces.Find(w => true).ToList();

        public Workspace GetById(string id) =>
            _workspaces.Find<Workspace>(w => w.Id == id).FirstOrDefault();

        public Workspace GetByWorkspaceId(string id) =>
            _workspaces.Find<Workspace>(w => w.WorkspaceId == id).FirstOrDefault();

        public Workspace Create(Workspace w)
        {
            _workspaces.InsertOne(w);
            return w;
        }

        public void Remove(string id) =>
            _workspaces.DeleteOne(w => w.Id == id);

        public bool AccessAllowed(string viewId)
        {
            var workspace = _workspaces.Find<Workspace>(w => w.ArViews.Contains(viewId)).FirstOrDefault();
            if (workspace == null || !workspace.IsClientAccessForbidden)
            {
                return true;
            }
                
            return false;
        }
    }
}
