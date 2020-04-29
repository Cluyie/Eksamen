using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace AdminPanel.Client.Services
{
    public class AuthCredentialsKeeper
    {
        const string INDEX_TOKEN = "AdminPanel-Token";
        const string INDEX_USERNAME = "AdminPanel-Username";

        ISyncLocalStorageService _localStorage;

        public string Username { get; private set; } = null;

        public string Token { get; private set; } = null;

        public AuthCredentialsKeeper(ISyncLocalStorageService localStorage)
        {
            _localStorage = localStorage;

            if (_localStorage.ContainKey(INDEX_USERNAME) && _localStorage.ContainKey(INDEX_TOKEN))
            {
                Username = _localStorage.GetItem<string>(INDEX_USERNAME);
                Token = _localStorage.GetItem<string>(INDEX_TOKEN);
            }
        }

        public void SetCredentials(string username, string token)
        {
            Username = username;
            Token = token;

            _localStorage.SetItem(INDEX_USERNAME, Username);
            _localStorage.SetItem(INDEX_TOKEN, Token);
        }

        public void ClearCredentials()
        {
            Username = null;
            Token = null;
            _localStorage.Clear();
        }

        public bool HasCredentials()
        {
            return (Username != null && Token != null);
        }
    }
}
