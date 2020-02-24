using Data_Access_Layer;
using Microsoft.AspNetCore.Mvc;
using Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Layer.Models;
using Data_Access_Layer.Models;

namespace Rest_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPut("{id}")]
        public ApiResponse<User> UpdateUser(Guid id, [FromBody] User userData)
        {
            return new ApiResponse<User>(ApiResponseCode.Success, userData);
        }
    }
}
