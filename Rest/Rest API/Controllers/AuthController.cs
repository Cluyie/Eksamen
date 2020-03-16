using Business_Layer;
using Business_Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<string> Login([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null || !ModelState.IsValid)
                return new ApiResponse<string>(ApiResponseCode.BadRequest, "");

            return _userService.Login(loginDTO);
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<string> Register([FromBody] RegisterDTO registerDTO)
        {
            if (registerDTO == null)
                return new ApiResponse<string>(ApiResponseCode.BadRequest, "");

            return _userService.Register(registerDTO);

        }
    }
}