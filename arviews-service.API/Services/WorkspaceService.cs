using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using arviews_service.API.Models;
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

        public Workspace GetByWorkspaceId(string id) =>
            _workspaces.Find<Workspace>(w => w.WorkspaceId == id).FirstOrDefault();

        public Workspace Create(Workspace w)
        {
            _workspaces.InsertOne(w);
            return w;
        }

        public void Remove(string id) =>
            _workspaces.DeleteOne(w => w.Id == id);
    }
}
