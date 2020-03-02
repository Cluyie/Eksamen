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

    public UserController(UserService userService, AuthService authService)
    {
      _userService = userService;
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