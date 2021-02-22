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
    public class ARConfigsController : ControllerBase
    {
        private readonly IARConfigService _configService;
        private readonly IWorkspaceService _workspaceService;
        private readonly IMapper _mapper;

        public ARConfigsController(IARConfigService cService, IWorkspaceService wService, IMapper mapper)
        {
            _configService = cService;
            _workspaceService = wService;
            _mapper = mapper;
        }

        [HttpGet("{viewId}/{limit:int}")]
        public async Task<IActionResult> Get(string viewId, int limit = 0)
        {
            bool accessAllowed = _workspaceService.AccessAllowed(viewId);
            if (!accessAllowed)
            {
                return StatusCode(403);
            }

            var configs = _configService.GetByViewId(viewId, limit);

            if (configs == null || configs.Count == 0)
            {
                return NotFound(viewId);
            }

            var resultItem = new ARConfigHistory()
            {
                ViewId = viewId, Configs = _mapper.Map<List<ARConfig>, List<ARConfigDto>>(configs) 
            };

            return Ok(resultItem);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ARConfig config)
        {
            bool accessAllowed = _workspaceService.AccessAllowed(config.ViewId);
            if (!accessAllowed)
            {
                return StatusCode(403);
            }

            _configService.Create(config);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_mapper.Map<ARConfigDto>(config));
        }
    }
}
