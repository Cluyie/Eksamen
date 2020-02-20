using Data_Access_Layer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer
{
    public interface IUserService
    {
        IActionResult UpdateUser(Guid id, [FromBody] UserData userData);
    }
}
