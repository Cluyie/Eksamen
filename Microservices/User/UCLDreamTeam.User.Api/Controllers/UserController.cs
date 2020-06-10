using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLDreamTeam.User.Application.Interfaces;
using UCLDreamTeam.User.Domain.Models;

namespace UCLDreamTeam.User.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ApiResponse<IUser>> GetProfile()
        {
            try
            {
                var userName = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                if (userName != null)
                {
                    var userProfile = _userService.GetUserFromUserNameAsync(userName).Result;

                    if (userProfile != null)
                    {
                        return new ApiResponse<IUser>(ApiResponseCode.OK, userProfile);
                    }
                }

                return new ApiResponse<IUser>(ApiResponseCode.NotFound);
            }
            catch (Exception e)
            {
                return new ApiResponse<IUser>(ApiResponseCode.InternalServerError) {Exception = e};
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ApiResponse<IUser>> GetById(Guid id)
        {
            if (id == Guid.Empty) return new ApiResponse<IUser>(ApiResponseCode.BadRequest);
            try
            {
                var user = await _userService.GetUserFromIdAsync(id);
                if (user != null)
                    return new ApiResponse<IUser>(ApiResponseCode.OK, user);
                return new ApiResponse<IUser>(ApiResponseCode.NotFound, user);
            }
            catch (Exception e)
            {
                return new ApiResponse<IUser>(ApiResponseCode.InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ApiResponse<IUser>> RegisterProfile([FromBody] Domain.Models.User user, [FromQuery] string roleName = null)
        {
            if (user == null || !ModelState.IsValid)
                return new ApiResponse<IUser>(ApiResponseCode.BadRequest); //return BadRequest();
            try
            {
                roleName ??= "User";
                var role = new Role { RoleName = roleName };
                await _userService.RegisterAsync(user, role);
                return new ApiResponse<IUser>(ApiResponseCode.OK, user); //Ok(user);
            }
            catch (Exception e)
            {
                return new ApiResponse<IUser>(ApiResponseCode.InternalServerError) {Exception = e}; //StatusCode(503, e.Message);
            }
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ApiResponse<IUser>> UpdateProfile([FromBody] Domain.Models.User user, [FromQuery] string roleName = null)
        {
            if (user == null || await _userService.GetUserFromIdAsync(user.Id) == null || !ModelState.IsValid)
                return new ApiResponse<IUser>(ApiResponseCode.BadRequest); //return BadRequest();
            try
            {
                roleName ??= "User";
                var role = new Role { RoleName = roleName };
                await _userService.Update(user, role);
                return new ApiResponse<IUser>(ApiResponseCode.OK, user); //Ok(user);
            }
            catch (Exception e)
            {
                return new ApiResponse<IUser>(ApiResponseCode.InternalServerError) {Exception = e}; //StatusCode(503, e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ApiResponse<IUser>> DeleteById(Guid id)
        {
            if (id == Guid.Empty) return new ApiResponse<IUser>(ApiResponseCode.BadRequest); //return BadRequest();
            try
            {
                await _userService.DeleteUserFromIdAsync(id);
                return new ApiResponse<IUser>(ApiResponseCode.OK); //Ok(null);
            }
            catch (Exception e)
            {
                return new ApiResponse<IUser>(ApiResponseCode.InternalServerError) {Exception = e}; //StatusCode(503, e.Message);
            }
        }
    }
}
