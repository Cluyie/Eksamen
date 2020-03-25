using Business_Layer;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Business_Layer.Models;
using Data_Access_Layer.Models;
using Rest_API.Controllers.ControllerMethods;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Rest_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        ResourceService _resourceService;
        RequestValidator _requestValidator;

        public ResourceController(ResourceService resourceService, RequestValidator genericMethods)
        {
            _resourceService = resourceService;
            _requestValidator = genericMethods;
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Resource> CreateResource([FromBody] Resource resource)
        {
            return _requestValidator.ValidateAndPerfom(resource, _resourceService.Create, Response);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<List<Resource>> GetResources()
        {
            return _requestValidator.ValidateAndPerfom(_resourceService.Get, Response);
        }

        [HttpGet("guid={guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Resource> GetResourceById([FromRoute]Guid guid)
        {
            return _requestValidator.ValidateAndPerfom(guid, _resourceService.Get, Response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Resource> UpdateResource([FromBody] Resource resource)
        {
            return _requestValidator.ValidateAndPerfom(resource, _resourceService.Update, Response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("guid={guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Resource> DeleteResource([FromRoute]Guid guid)
        {
            return _requestValidator.ValidateAndPerfom(guid, _resourceService.Delete, Response);
        }
    }
}
