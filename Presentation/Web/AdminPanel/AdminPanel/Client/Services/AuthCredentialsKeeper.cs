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

        string _username = null;
        string _token = null;

        public string Username
        {
            get => _username;
        }

        public string Token
        {
            get => _token;
        }

        public AuthCredentialsKeeper(ISyncLocalStorageService localStorage)
        {
            _localStorage = localStorage;

            _username = _localStorage.GetItem<string>(INDEX_USERNAME);
            _token = _localStorage.GetItem<string>(INDEX_TOKEN);
        }

        public void SetCredentials(string username, string token)
        {
            _username = username;
            _token = token;

            _localStorage.SetItem(INDEX_USERNAME, _username);
            _localStorage.SetItem(INDEX_TOKEN, _token);
        }

        public bool HasCredentials()
        {
            return (_username != null && _token != null);
        }
    }
}
