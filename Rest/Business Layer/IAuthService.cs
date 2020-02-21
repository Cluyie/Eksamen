using Business_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer
{
    public interface IAuthService
    {
        IActionResult Login([FromBody] LoginDTO loginDTO);
        IActionResult Register([FromBody] RegisterDTO registerDTO);
    }
}
