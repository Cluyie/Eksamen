using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Shared;

namespace AdminPanel.Client.Services
{
    public interface IAuthService
    {
        string GetToken();

        Task<bool> Login(LoginDTO loginDTO);

        void Logout();
    }
}
