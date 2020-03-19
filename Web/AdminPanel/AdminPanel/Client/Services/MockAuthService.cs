using AdminPanel.Client.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace AdminPanel.Client.Services
{
    public class MockAuthService : IAuthService
    {
        private string _token = null;

        private CustomAuthStateProvider _authStateProvider;

        public MockAuthService(AuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = (CustomAuthStateProvider)authStateProvider;
        }

        public string GetToken()
        {
            return _token;
        }

        public void Logout()
        {
            _authStateProvider.Logout();

            _token = null;
        }

        public async Task<bool> Login(LoginDTO loginDTO)
        {
            _authStateProvider.Login(loginDTO.Username);

            return true;
        }
    }
}
