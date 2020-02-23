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
                return new ApiResponse<string>(ApiResponseCode.BadRequest, "", "Intet data modtaget");

            try
            {
                //Call BL Login method
            }
            catch (Exception)
            {
                return new ApiResponse<string>(ApiResponseCode.NotAuthenticated, "", "Det var ikke muligt at authenticate dette login");
            }

            return new ApiResponse<string>(ApiResponseCode.BadRequest, "TestToken");
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<string> Register([FromBody] RegisterDTO registerDTO)
        {
            if (registerDTO == null)
                return new ApiResponse<string>(ApiResponseCode.BadRequest, "", "Intet data modtaget");

            try
            {
                //Call BL Register method
            }
            catch (Exception)
            {
                return new ApiResponse<string>(ApiResponseCode.InternalServerError, "", "Det var ikke muligt at oprette dette login");
            }

            return new ApiResponse<string>(ApiResponseCode.Created, "TestToken");
        }
    }
}