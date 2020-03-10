using AdminPanel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Client.Services
{
    public class MockAuthService : IAuthService
    {
        private AppState _state;

        private string _token = null;

        public MockAuthService(AppState state)
        {
            _state = state;
        }

        public string GetToken()
        {
            return _token;
        }

        public void Logout()
        {
            _state.Authenticated = false;
            _token = null;
        }

        public async Task<bool> Login(LoginDTO loginDTO)
        {
            _state.Authenticated = true;

            return true;
        }
    }
}
