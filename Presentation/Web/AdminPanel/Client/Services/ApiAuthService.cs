using AdminPanel.Client.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using AdminPanel.Client.Models;

namespace AdminPanel.Client.Services
{
    public class ApiAuthService : IAuthService
    {
        private readonly CustomAuthStateProvider _authStateProvider;
        private readonly AuthCredentialsKeeper _credentialsKeeper;
        private readonly ApiClient _client;
        private User User { get; set; }

        public ApiAuthService(AuthenticationStateProvider authStateProvider,
            AuthCredentialsKeeper credentialsKeeper, ApiClient client)
        {
            _authStateProvider = (CustomAuthStateProvider)authStateProvider;
            _credentialsKeeper = credentialsKeeper;
            _client = client;
        }

        public async Task<User> GetCurrentUser()
        {
            if(User != null && User.UserName.Equals(_credentialsKeeper.Username))
            {
                return User;
            }
            User = ((await _client.GetAsync<User>("User")).Value);
            return User;
        }

        public async Task<bool> Login(LoginDTO loginDTO)
        {
            ApiResponseDTO<string> response = await _client.PostAsync<string>("Auth/Login", loginDTO);
            Console.WriteLine(response.ToString());
            if(response.Code != 200)
            {
                return false;
            }
            else
            {
                _credentialsKeeper.SetCredentials(loginDTO.UsernameOrEmail, response.Value);
                _authStateProvider.Refresh();
                return true;
            }
        }

        public void Logout()
        {
            User = null;
            _credentialsKeeper.ClearCredentials();
            _authStateProvider.Refresh();
        }
    }
}
