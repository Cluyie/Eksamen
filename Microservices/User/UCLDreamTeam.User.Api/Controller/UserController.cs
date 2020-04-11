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
        public async Task<ActionResult<Domain.Models.User>> Get()
        {
            try
            {
                var userName = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                var userProfile = await _userService.GetUserFromUserNameAsync(userName);
                if (userProfile == null) return BadRequest();
                return Ok(userProfile);
            }
            catch (Exception e)
            {
                return StatusCode(503, e.Message);
            }
        }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Domain.Models.User>> GetById([FromRoute] Guid id)
        {
            if (id == Guid.Empty) return BadRequest();
            try
            {
                var userProfile = await _userService.GetUserFromIdAsync(id);
                if (userProfile == null) return NotFound();
                return Ok(userProfile);
            }
            catch (Exception e)
            {
                return StatusCode(503, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] Domain.Models.User user)
        {
            if (user == null) return BadRequest();
            try
            {
                await _userService.RegisterAsync(user);
                return Ok(user);
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
        public async Task<IActionResult> Update([FromBody] Domain.Models.User user)
        {
            if (user == null || !ModelState.IsValid) return BadRequest();
            try
            {
                await _userService.Update(user);
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(503, e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
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