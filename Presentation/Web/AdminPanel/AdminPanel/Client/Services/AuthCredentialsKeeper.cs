using Blazored.LocalStorage;

namespace AdminPanel.Client.Services
{
    public class AuthCredentialsKeeper
    {
        private const string INDEX_TOKEN = "AdminPanel-Token";
        private const string INDEX_USERNAME = "AdminPanel-Username";

        private readonly ISyncLocalStorageService _localStorage;

        public AuthCredentialsKeeper(ISyncLocalStorageService localStorage)
        {
            _localStorage = localStorage;

            Username = _localStorage.GetItem<string>(INDEX_USERNAME);
            Token = _localStorage.GetItem<string>(INDEX_TOKEN);
        }

        public string Username { get; private set; }

        public string Token { get; private set; }

        public void SetCredentials(string username, string token)
        {
            Username = username;
            Token = token;

            _localStorage.SetItem(INDEX_USERNAME, Username);
            _localStorage.SetItem(INDEX_TOKEN, Token);
        }

        public bool HasCredentials()
        {
            return Username != null && Token != null;
        }
    }
}