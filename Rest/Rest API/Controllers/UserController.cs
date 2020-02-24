using Data_Access_Layer;
using Microsoft.AspNetCore.Mvc;
using Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Layer.Models;
using Microsoft.AspNetCore.Http;

namespace Rest_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        public ApiResponse<UserData> UpdateUser(Guid id, [FromBody] UserData userData)
        {
            if (userData == null)
                return new ApiResponse<UserData>(ApiResponseCode.BadRequest, userData);

            try
            {
                //Call DAL user update method
            }
            catch (Exception)
            {
                return new ApiResponse<UserData>(ApiResponseCode.NotModified, userData);
            }

            return new ApiResponse<UserData>(ApiResponseCode.OK, userData);
        }
    }
}