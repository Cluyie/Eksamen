using AdminPanel.Client.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using AdminPanel.Client.Models;

namespace AdminPanel.Client.Services
{
    public class ApiAuthService : IAuthService
    {
        private readonly CustomAuthStateProvider _authStateProvider;
        private readonly AuthCredentialsKeeper _credentialsKeeper;
        private readonly ApiClient _client;

        public User CurrentUser { get; private set; }

        public ApiAuthService(AuthenticationStateProvider authStateProvider,
            AuthCredentialsKeeper credentialsKeeper, ApiClient client)
        {
            _authStateProvider = (CustomAuthStateProvider)authStateProvider;
            _credentialsKeeper = credentialsKeeper;
            _client = client;
            
        }

        private async Task<User> GetUser()
        {
            ApiResponseDTO<User> response = await _client.GetAsync<User>("User");
            return response.Value;
        }

        public async Task<bool> Login(LoginDTO loginDTO)
        {
            ApiResponseDTO<string> response = await _client.PostAsync<string>("Auth/Login", loginDTO);

            if(response.Code != 200)
            {
                return false;
            }
            else
            {
                _credentialsKeeper.SetCredentials(loginDTO.UsernameOrEmail, response.Value);
                _authStateProvider.Refresh();
                CurrentUser = await GetUser();
                return true;
            }
        }

        public void Logout()
        {
            _credentialsKeeper.ClearCredentials();
            _authStateProvider.Refresh();
        }
    }
}
