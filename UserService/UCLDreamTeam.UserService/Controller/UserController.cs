using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Interfaces;
using UCLDreamTeam.UserServiceApi.Domain.Models;
using UCLDreamTeam.UserServiceApi.Domain.Services;
using UCLDreamTeam.UserServiceApi.Domain.Services.Interfaces;

namespace UCLDreamTeam.UserServiceApi.Controller
{
  [Authorize]
  [Route("[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ApiResponse<User>> GetProfile()
    {
        var response = new ApiResponse<User>(ApiResponseCode.NoContent, null);

        var userName = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

        var userProfile = _userService.GetUserFromUserNameAsync(userName).Result;

        if (userProfile != null)
        {
            response = new ApiResponse<User>(ApiResponseCode.OK, userProfile);
        }
        return response;
    }

    [HttpGet("guid={guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ApiResponse<User>> GetProfileFromGuid([FromRoute] Guid guid)
    {
        var response = new ApiResponse<User>(ApiResponseCode.NoContent, null);

        var userProfile = _userService.GetUserFromIdAsync(guid).Result;

        if (userProfile != null)
        {
            response = new ApiResponse<User>(ApiResponseCode.OK, userProfile);
        }
        return response;
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    public async Task<ApiResponse<User>> UpdateProfile([FromBody] User user)
    {
      if (user == null || !ModelState.IsValid)
        return new ApiResponse<User>(ApiResponseCode.BadRequest, user);

      return await _userService.Update(user);
    }
  }
}