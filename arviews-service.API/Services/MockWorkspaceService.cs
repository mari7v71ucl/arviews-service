using System.Collections.Generic;
using System.Linq;
using arviews_service.API.Models;

namespace arviews_service.API.Services
{
    public class MockWorkspaceService : IWorkspaceService
    {
        List<Workspace> workspaces = new List<Workspace>()
        {
            new Workspace()
            {
                Id = "6LNfhisUAuU",
                WorkspaceId = "workspace001",
                ArViews = new List<string>()
                {
                    "view001",
                    "view002",
                    "view003"
                }
            },
            new Workspace()
            {
                Id = "HC3aUmnK3nG",
                WorkspaceId = "workspace002",
                ArViews = new List<string>()
                {
                    "view004",
                    "view005",
                    "view006"
                }
            },
            new Workspace()
            {
                Id = "22DDU2Nl5Ad",
                WorkspaceId = "workspace003",
                ArViews = new List<string>()
                {
                    "view007",
                    "view008",
                    "view009"
                }
            },
            new Workspace()
            {
                Id = "pcBwQhs75TP",
                WorkspaceId = "workspace004",
                ArViews = new List<string>()
                {
                    "forbiddenviewid"
                },
                IsClientAccessForbidden = true
            }
        };


        public List<Workspace> Get() => workspaces;
        
        public Workspace GetByWorkspaceId(string wId) => workspaces.FirstOrDefault(w => w.WorkspaceId == wId);
        public Workspace GetById(string id) => workspaces.FirstOrDefault(w => w.Id == id);

        public Workspace Create(Workspace w)
        {
            w.Id = "MbkJ9u";
            return w;
        }

        public void Remove(string id)
        {
            var itemToRemove = workspaces.FirstOrDefault(i => i.Id == id);
            workspaces.Remove(itemToRemove);
        }

        public bool AccessAllowed(string viewId)
        {
            var workspace = workspaces.FirstOrDefault(w => w.ArViews.Contains(viewId));
            if (workspace == null || !workspace.IsClientAccessForbidden)
            {
                return true;
            }

            return false;
        }
    }
}
