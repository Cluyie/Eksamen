using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase, IUserService
    {
        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, [FromBody] User user)
        {
            throw new NotImplementedException();
        }
    }
}
