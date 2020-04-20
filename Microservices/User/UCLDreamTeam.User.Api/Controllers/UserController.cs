using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<IUser>> GetProfile()
        {
            try
            {
                var userName = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

                var userProfile = _userService.GetUserFromUserNameAsync(userName).Result;

                if (userProfile != null)
                {
                    //return Ok(userProfile);
                    return new ApiResponse<IUser>(ApiResponseCode.OK, userProfile);
                }

                //return NotFound(userProfile);
                return new ApiResponse<IUser>(ApiResponseCode.NotFound, null);
            }
            catch (Exception ex)
            {
                //TODO Fill in exceptions
                //if (ex.GetType() == typeof(InvalidOperationException))
                //{

                //}
                //return StatusCode(503, ex.Message);
                return new ApiResponse<IUser>(ApiResponseCode.ServiceUnavailable, null);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<IUser>> GetById(Guid id)
        {
            if (id == Guid.Empty) return new ApiResponse<IUser>(ApiResponseCode.BadRequest, null);//return BadRequest();
            try
            {
                var user = await _userService.GetUserFromIdAsync(id);
                if (user != null)
                    //return Ok(user);
                    return new ApiResponse<IUser>(ApiResponseCode.OK, user);
                //return NotFound(user);
                return new ApiResponse<IUser>(ApiResponseCode.NotFound, null);
            }
            catch (Exception ex)
            {
                //TODO Fill in exceptions
                //if (ex.GetType() == typeof(InvalidOperationException))
                //{

                //}
                //return StatusCode(503, ex.Message);
                return new ApiResponse<IUser>(ApiResponseCode.ServiceUnavailable, null);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        public async Task<ApiResponse<IUser>> UpdateProfile([FromBody] Domain.Models.User user)
        {
            if (user == null || !ModelState.IsValid) return new ApiResponse<IUser>(ApiResponseCode.BadRequest, null); //return BadRequest();
            try
            {
                if (await _userService.GetUserFromIdAsync(user.Id) != null)
                    await _userService.Update(user);
                else
                    await _userService.RegisterAsync(user);

                //return Ok(user);
                return new ApiResponse<IUser>(ApiResponseCode.OK, user);
            }
            catch (Exception ex)
            {
                //TODO Fill in exceptions
                //if (ex.GetType() == typeof(InvalidOperationException))
                //{

                //}
                //return StatusCode(503, ex.Message);
                return new ApiResponse<IUser>(ApiResponseCode.ServiceUnavailable, null);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse<IUser>> DeleteById(Guid id)
        {
            if (id == Guid.Empty) return new ApiResponse<IUser>(ApiResponseCode.BadRequest, null);//return BadRequest();
            try
            {
                await _userService.DeleteUserFromIdAsync(id);
                //return Ok(id);
                return new ApiResponse<IUser>(ApiResponseCode.OK, null);
            }
            catch (Exception ex)
            {
                //TODO Fill in exceptions
                //if (ex.GetType() == typeof(InvalidOperationException))
                //{

                //}
                //return StatusCode(503, ex.Message);
                return new ApiResponse<IUser>(ApiResponseCode.ServiceUnavailable, null);
            }
        }
    }
}