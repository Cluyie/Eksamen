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
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<string> Login([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null)
                return new ApiResponse<string>(ApiResponseCode.BadRequest, "");

            try
            {
                //Call BL Login method
            }
            catch (Exception)
            {
                return new ApiResponse<string>(ApiResponseCode.UnAuthenticated, "");
            }

            return new ApiResponse<string>(ApiResponseCode.OK, "TestToken");
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<string> Register([FromBody] RegisterDTO registerDTO)
        {
            if (registerDTO == null)
                return new ApiResponse<string>(ApiResponseCode.BadRequest, "");

            try
            {
                //Call BL Register method
            }
            catch (Exception)
            {
                return new ApiResponse<string>(ApiResponseCode.InternalServerError, "");
            }

            return new ApiResponse<string>(ApiResponseCode.Created, "TestToken");
        }
    }
}