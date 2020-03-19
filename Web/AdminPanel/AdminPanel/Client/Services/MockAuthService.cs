﻿using AdminPanel.Client.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace AdminPanel.Client.Services
{
    public class MockAuthService : IAuthService
    {
        private CustomAuthStateProvider _authStateProvider;
        private AuthCredentialsKeeper _credentialsKeeper;

        public MockAuthService(AuthenticationStateProvider authStateProvider,
            AuthCredentialsKeeper credentialsKeeper)
        {
            _authStateProvider = (CustomAuthStateProvider)authStateProvider;
            _credentialsKeeper = credentialsKeeper;
        }

        public void Logout()
        {
            _credentialsKeeper.SetCredentials(null, null);
            _authStateProvider.Refresh();
        }

        public async Task<bool> Login(LoginDTO loginDTO)
        {
            _credentialsKeeper.SetCredentials(loginDTO.Username, "DummyToken");
            _authStateProvider.Refresh();

            return true;
        }
    }
}
