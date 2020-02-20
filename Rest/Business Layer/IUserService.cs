using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer
{
    interface IUserService
    {
        IActionResult UpdateUser(Guid id, [FromBody] User user);
    }
}
