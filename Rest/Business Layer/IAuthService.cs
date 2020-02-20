using Business_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer
{
    public interface IAuthService
    {
        ApiResponse<string> Login(LoginDTO loginDTO);
        ApiResponse<string> Register(RegisterDTO registerDTO);
    }
}
