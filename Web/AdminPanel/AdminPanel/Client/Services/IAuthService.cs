using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Shared;

namespace AdminPanel.Client.Services
{
    interface IAuthService
    {
        Task<bool> Login(LoginDTO loginDTO);

        void Logout();

        bool IsLoggedIn();
    }
}
