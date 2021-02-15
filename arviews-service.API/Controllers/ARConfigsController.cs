using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        private readonly IMapper _mapper;

        public ARConfigsController(IARConfigService service, IMapper mapper)
        {
            _configService = service;
            _mapper = mapper;
        }

        [HttpGet("{viewId}/{limit:int}")]
        public async Task<IActionResult> Get(string viewId, int limit = 0)
        {
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
            _configService.Create(config);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return CreatedAtAction("Get", new {ViewId = config.ViewId}, _mapper.Map<ARConfigDto>(config));
        }
    }
}
