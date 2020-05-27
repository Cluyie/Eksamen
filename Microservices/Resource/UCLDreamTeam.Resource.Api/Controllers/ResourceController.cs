using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UCLDreamTeam.Resource.Api.BusinessLayer;
using UCLDreamTeam.Resource.Domain.Interfaces;
using UCLDreamTeam.Resource.Domain.Models;

namespace UCLDreamTeam.Resource.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _ResourceService;

        public ResourceController(IResourceService ResourceService)
        {
            _ResourceService = ResourceService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Domain.Models.Resource> CreateResource([FromBody] Domain.Models.Resource Resource)
        {
            return _ResourceService.Create(Resource);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<List<Domain.Models.Resource>> GetResources()
        {
            return _ResourceService.Get();
        }

        [HttpGet("guid={guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Domain.Models.Resource> GetResourceById([FromRoute] Guid guid)
        {
            return _ResourceService.Get(guid);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse<Domain.Models.Resource>> UpdateResource([FromBody] Domain.Models.Resource Resource)
        {
            return await _ResourceService.Update(Resource);
        }

        [HttpDelete("guid={guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse<Domain.Models.Resource>> DeleteResource([FromRoute] Guid guid)
        {
            return await _ResourceService.Delete(guid);
        }
    }
}