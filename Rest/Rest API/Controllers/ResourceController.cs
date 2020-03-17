﻿using Business_Layer;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Business_Layer.Models;
using Data_Access_Layer.Models;

namespace Rest_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        ResourceService _resourceService;

        public ResourceController(ResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Resource> CreateResource([FromBody] Resource resource)
        {
            if (resource == null || !ModelState.IsValid)
                return new ApiResponse<Resource>(ApiResponseCode.BadRequest, resource);

            return _resourceService.Create(resource);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<List<Resource>> GetResources()
        {
            return _resourceService.Get();
        }

        [HttpGet("guid={guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Resource> GetResourceById([FromRoute]Guid guid)
        {
            if (guid == null)
                return new ApiResponse<Resource>(ApiResponseCode.BadRequest, null);

            return _resourceService.Get(guid);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Resource> UpdateResource([FromBody] Resource resource)
        {
            if (resource == null || !ModelState.IsValid)
                return new ApiResponse<Resource>(ApiResponseCode.BadRequest, resource);

            return _resourceService.Update(resource);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<string> DeleteResource(Resource resource)
        {
            if (resource == null || !ModelState.IsValid)
                return new ApiResponse<string>(ApiResponseCode.BadRequest, "");

            return _resourceService.Delete(resource);
        }
    }
}
