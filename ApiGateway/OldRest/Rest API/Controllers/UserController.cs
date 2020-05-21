using System;
using System.Security.Claims;
using Business_Layer;
using Business_Layer.Models;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Rest_API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ApiResponse<User> GetProfile()
        {
            var response = new ApiResponse<User>(ApiResponseCode.NoContent, null);

            var userName = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            var userProfile = _userService.GetUserFromUserNameAsync(userName).Result;

            if (userProfile != null) response = new ApiResponse<User>(ApiResponseCode.OK, userProfile);
            return response;
        }

        [HttpGet("guid={guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ApiResponse<User> GetProfileFromGuid([FromRoute] Guid guid)
        {
            var response = new ApiResponse<User>(ApiResponseCode.NoContent, null);

            var userProfile = _userService.GetUserFromIdAsync(guid).Result;

            if (userProfile != null) response = new ApiResponse<User>(ApiResponseCode.OK, userProfile);
            return response;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        public ApiResponse<User> UpdateProfile([FromBody] User user)
        {
            if (user == null || !ModelState.IsValid)
                return new ApiResponse<User>(ApiResponseCode.BadRequest, user);

            return _userService.Update(user);
        }
    }
}