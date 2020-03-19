using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System;

namespace AdminPanel.Client.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private string _username = null;

        /// <summary>
        /// Create the initial authenticationstate when loading the site for the first time
        /// </summary>
        /// <returns></returns>
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Username is null, so return an empty claimsprinipal, so we are NOT authenticated
            if(_username == null)
            {
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, _username),
            }, "ApiWebAuth");

            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }

        /// <summary>
        /// Authenticate the user from the given username
        /// </summary>
        /// <param name="username"></param>
        public void Login(string username)
        {
            _username = username;

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        /// <summary>
        /// Log the user out
        /// </summary>
        public void Logout()
        {
            _username = null;

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}