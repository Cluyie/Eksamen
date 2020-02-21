using Business_Layer;
using Business_Layer.Models;
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
        [HttpPost("[Login]")]
        public ApiResponse<string> Login([FromBody] LoginDTO loginDTO)
        {
            return new ApiResponse<string>(ApiResponseCode.Success, "TestToken");
        }

        [HttpPost("[Register]")]
        public ApiResponse<string> Register([FromBody] RegisterDTO registerDTO)
        {
            return new ApiResponse<string>(ApiResponseCode.Success, "TestToken");
        }
    }
}
