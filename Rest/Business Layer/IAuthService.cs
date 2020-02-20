using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer
{
    interface IAuthService
    {
        IActionResult Login([FromBody] User user);
    }
}
