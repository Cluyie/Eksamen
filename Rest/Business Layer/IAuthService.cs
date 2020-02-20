using Business_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer
{
    public interface IAuthService
    {
        void Login(LoginDTO loginDTO);
        void Register(RegisterDTO registerDTO);
    }
}
