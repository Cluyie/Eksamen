using System.Threading.Tasks;
using AdminPanel.Client.DTOs;
using Microsoft.AspNetCore.Components.Authorization;

namespace AdminPanel.Client.Services
{
    public class MockAuthService : IAuthService
    {
        private readonly CustomAuthStateProvider _authStateProvider;
        private readonly AuthCredentialsKeeper _credentialsKeeper;

        public MockAuthService(AuthenticationStateProvider authStateProvider,
            AuthCredentialsKeeper credentialsKeeper)
        {
            _authStateProvider = (CustomAuthStateProvider) authStateProvider;
            _credentialsKeeper = credentialsKeeper;
        }

        public void Logout()
        {
            _credentialsKeeper.SetCredentials(null, null);
            _authStateProvider.Refresh();
        }

        public async Task<bool> Login(LoginDTO loginDTO)
        {
            _credentialsKeeper.SetCredentials(loginDTO.UsernameOrEmail, "DummyToken");
            _authStateProvider.Refresh();

            return true;
        }
    }
}