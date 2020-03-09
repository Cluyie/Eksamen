using Data_Access_Layer;
using Microsoft.AspNetCore.Mvc;
using Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Layer.Models;
using Microsoft.AspNetCore.Http;
using Data_Access_Layer.Models;
using Rest_API.Middleware;

namespace Rest_API.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private UserService _userService;
    private AuthService _authService;

    public UserController(UserService userService, AuthService authService)
    {
      _userService = userService;
      _authService = authService;
    }

    [HttpGet("GetProfile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ApiResponse<User> GetProfile()
    {
      var user = _authService.GetUser();
      if (user == null)
        return new ApiResponse<User>(ApiResponseCode.BadRequest, user);
      return new ApiResponse<User>(ApiResponseCode.OK, user);
    } 

    [HttpPut("UpdateProfile")]
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