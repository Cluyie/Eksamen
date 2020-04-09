using System.Threading.Tasks;
using AdminPanel.Client.DTOs;
using Microsoft.AspNetCore.Components.Authorization;

namespace AdminPanel.Client.Services
{
    public class ApiAuthService : IAuthService
    {
        private readonly CustomAuthStateProvider _authStateProvider;
        private readonly ApiClient _client;
        private readonly AuthCredentialsKeeper _credentialsKeeper;

        public ApiAuthService(AuthenticationStateProvider authStateProvider,
            AuthCredentialsKeeper credentialsKeeper, ApiClient client)
        {
            _authStateProvider = (CustomAuthStateProvider) authStateProvider;
            _credentialsKeeper = credentialsKeeper;
            _client = client;
        }

        public async Task<bool> Login(LoginDTO loginDTO)
        {
            var response = await _client.PostAsync<string>("Auth/Login", loginDTO);

            if (response.Code != 200)
            {
                return false;
            }

            _credentialsKeeper.SetCredentials(loginDTO.UsernameOrEmail, response.Value);
            _authStateProvider.Refresh();
            return true;
        }

        public void Logout()
        {
            _credentialsKeeper.SetCredentials(null, null);
            _authStateProvider.Refresh();
        }
    }
}