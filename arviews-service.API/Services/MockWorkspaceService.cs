using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                    "view001",
                    "view002",
                    "view003"
                }
            },
            new Workspace()
            {
                Id = "22DDU2Nl5Ad",
                WorkspaceId = "workspace003",
                ArViews = new List<string>()
                {
                    "view001",
                    "view002",
                    "view003"
                }
            }
        };


        public List<Workspace> Get() => workspaces;
        
        public Workspace GetByWorkspaceId(string wId) => workspaces.FirstOrDefault(w => w.WorkspaceId == wId);

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
    }
}
