using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arviews_service.API.Dtos
{
    public class WorkspaceDto
    {
        public string Id { get; set; }
        public string WorkspaceId { get; set; }
        public bool IsClientAccessForbidden { get; set; }
        // can we include number of views?
    }
}
