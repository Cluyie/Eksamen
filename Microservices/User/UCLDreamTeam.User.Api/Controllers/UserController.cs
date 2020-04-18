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
        public async Task<ActionResult<IUser>> GetProfile()
        {
            try
            {
                var userName = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

                var userProfile = _userService.GetUserFromUserNameAsync(userName).Result;

                if (userProfile != null)
                {
                    return Ok(userProfile);
                }

                return NotFound(userProfile);
            }
            catch (Exception e)
            {
                return StatusCode(503, e.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IUser>> GetById(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();
            try
            {
                var user = await _userService.GetUserFromIdAsync(id);
                if (user != null)
                    return Ok(user);
                return NotFound(user);
            }
            catch (Exception e)
            {
                return StatusCode(503, e.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        public async Task<IActionResult> UpdateProfile([FromBody] Domain.Models.User user)
        {
            if (user == null || !ModelState.IsValid)
                return BadRequest();
            try
            {
                if (await _userService.GetUserFromIdAsync(user.Id) != null)
                    await _userService.Update(user);
                else
                    await _userService.RegisterAsync(user);

                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(503, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();
            try
            {
                await _userService.DeleteUserFromIdAsync(id);
                return Ok(id);
            }
            catch (Exception e)
            {
                return StatusCode(503, e.Message);
            }
        }
    }
}