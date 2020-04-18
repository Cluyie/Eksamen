using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UCLDreamTeam.Resource.Api.BusinessLayer;
using UCLDreamTeam.Resource.Api.Models;

namespace UCLDreamTeam.Resource.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly ResourceService _resourceService;

        public ResourceController(ResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Domain.Models.Resource> CreateResource([FromBody] Domain.Models.Resource resource)
        {
            return _resourceService.Create(resource);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<List<Domain.Models.Resource>> GetResources()
        {
            return _resourceService.Get();
        }

        [HttpGet("guid={guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Domain.Models.Resource> GetResourceById([FromRoute] Guid guid)
        {
            return _resourceService.Get(guid);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse<Domain.Models.Resource>> UpdateResource([FromBody] Domain.Models.Resource resource)
        {
            return await _resourceService.Update(resource);
        }

        [HttpDelete("guid={guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse<Domain.Models.Resource>> DeleteResource([FromRoute] Guid guid)
        {
            return await _resourceService.Delete(guid);
        }
    }
}