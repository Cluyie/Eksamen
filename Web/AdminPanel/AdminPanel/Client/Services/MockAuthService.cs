using AdminPanel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Client.Services
{
    public class MockAuthService : IAuthService
    {
        bool loggedIn = false;

        public bool IsLoggedIn()
        {
            return loggedIn;
        }

        public void Logout()
        {
            loggedIn = false;
        }

        public async Task<bool> Login(LoginDTO loginDTO)
        {
            loggedIn = true;

            return true;
        }
    }
}
