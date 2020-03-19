using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Client.DTOs;

namespace AdminPanel.Client.Services
{
    public interface IAuthService
    {
        Task<bool> Login(LoginDTO loginDTO);

        void Logout();
    }
}
