using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Interfaces;
using UCLDreamTeam.User.Application.Interfaces;
using UCLDreamTeam.User.Domain.Models;

namespace UCLDreamTeam.User.Api.Controller
{
  
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
    public async Task<ApiResponse<Domain.Models.User>> GetProfile()
    {
        var response = new ApiResponse<Domain.Models.User>(ApiResponseCode.NoContent, null);

        var userName = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

        var userProfile = _userService.GetUserFromUserNameAsync(userName).Result;

        if (userProfile != null)
        {
            response = new ApiResponse<Domain.Models.User>(ApiResponseCode.OK, userProfile);
        }
        return response;
    }

    [HttpGet("guid={guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ApiResponse<Domain.Models.User>> GetProfileFromGuid([FromRoute] Guid guid)
    {
        var response = new ApiResponse<Domain.Models.User>(ApiResponseCode.NoContent, null);

        var userProfile = _userService.GetUserFromIdAsync(guid).Result;

        if (userProfile != null)
        {
            response = new ApiResponse<Domain.Models.User>(ApiResponseCode.OK, userProfile);
        }
        return response;
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    public async Task<ApiResponse<Domain.Models.User>> UpdateProfile([FromBody] Domain.Models.User user)
    {
      if (user == null || !ModelState.IsValid)
        return new ApiResponse<Domain.Models.User>(ApiResponseCode.BadRequest, user);

      return await _userService.Update(user);
    }
  }
}