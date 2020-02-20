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
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }

        [HttpPost("[Register]")]
        public IActionResult Register([FromBody] RegisterDTO registerDTO)
        {
            throw new NotImplementedException();
        }
    }
}
