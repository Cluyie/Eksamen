using Business_Layer;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Business_Layer.Models;
using Data_Access_Layer.Models;
using Rest_API.Controllers.ControllerMethods;

namespace Rest_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        ResourceService _resourceService;
        AuthService _authService;
        GenericMethods _genericMethods;

        private User _user;

        public ResourceController(ResourceService resourceService, AuthService authService, GenericMethods genericMethods)
        {
            _resourceService = resourceService;
            _authService = authService;
            _genericMethods = genericMethods;

            _user = _authService.GetUser();
        }
       
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Resource> CreateResource([FromBody] Resource resource)
        {
            return _genericMethods.ValidateAndPerfom(resource, _resourceService.Create);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<List<Resource>> GetResources()
        {
            return _genericMethods.ValidateAndPerfom(_resourceService.Get);
        }

        [HttpGet("guid={guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Resource> GetResourceById([FromRoute]Guid guid)
        {
            return _genericMethods.ValidateAndPerfom(guid, _resourceService.Get);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Resource> UpdateResource([FromBody] Resource resource)
        {
            return _genericMethods.ValidateAndPerfom(resource, _resourceService.Update);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Resource> DeleteResource(Resource resource)
        {
            return _genericMethods.ValidateAndPerfom(resource, _resourceService.Delete);
        }
    }
}
