using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resource.Domain.Models;
using ResourceMicroService.BusinessLayer;

namespace ResourceMicroService.Controllers
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
        public ApiResponse<Resource<AvaiableTime>> CreateResource([FromBody] Resource<AvaiableTime> resource)
        {
            return _resourceService.Create(resource);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<List<Resource<AvaiableTime>>> GetResources()
        {
            return _resourceService.Get();
        }

        [HttpGet("guid={guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Resource<AvaiableTime>> GetResourceById([FromRoute] Guid guid)
        {
            return _resourceService.Get(guid);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse<Resource<AvaiableTime>>> UpdateResource([FromBody] Resource<AvaiableTime> resource)
        {
            return await _resourceService.Update(resource);
        }

        [HttpDelete("guid={guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse<Resource<AvaiableTime>>> DeleteResource([FromRoute] Guid guid)
        {
            return await _resourceService.Delete(guid);
        }
    }
}