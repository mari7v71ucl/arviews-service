using System.Collections.Generic;
using System.Threading.Tasks;
using arviews_service.API.Dtos;
using arviews_service.API.Models;
using arviews_service.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace arviews_service.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkspacesController : ControllerBase
    {
        private readonly IWorkspaceService _workspaceService;
        private readonly IMapper _mapper;

        public WorkspacesController(IWorkspaceService service, IMapper mapper)
        {
            _workspaceService = service;
            _mapper = mapper;
        }

        [HttpGet("{includeViews:bool}")]
        public async Task<IActionResult> Get(bool includeViews = false)
        {
            var workspaces = _workspaceService.Get();

            if (includeViews)
                return Ok(workspaces);
            
            return Ok(_mapper.Map<List<Workspace>, List<WorkspaceDto>>(workspaces));
        }

        [HttpGet("{wId}")]
        public async Task<IActionResult> GetByWId(string wId)
        {
            var workspace = _workspaceService.GetByWorkspaceId(wId);

            if (workspace == null)
            {
                return NotFound(wId);
            }

            return Ok(workspace);
        }


        [HttpPost]
        public async Task<IActionResult> Post(Workspace workspace)
        {
            workspace = _workspaceService.Create(workspace);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return CreatedAtAction("GetByWId", new {wId = workspace.WorkspaceId}, workspace);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteByWId(string wId)
        {
            var existingItem = _workspaceService.GetByWorkspaceId(wId);

            if (existingItem == null)
            {
                return NotFound();
            }

            var id = existingItem.Id;
            _workspaceService.Remove(id);
            return Ok();
        }
    }
}
