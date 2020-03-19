using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System;

namespace AdminPanel.Client.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        AuthCredentialsKeeper _credentialsKeeper;

        public CustomAuthStateProvider(AuthCredentialsKeeper credentialsKeeper)
        {
            _credentialsKeeper = credentialsKeeper;
        }

        /// <summary>
        /// Create the initial authenticationstate when loading the site for the first time
        /// </summary>
        /// <returns></returns>
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Username is null, so return an empty claimsprinipal, so we are NOT authenticated
            if(!_credentialsKeeper.HasCredentials())
            {
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, _credentialsKeeper.Username),
            }, "ApiWebAuth");

            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }

        /// <summary>
        /// Re-calculate the users authentication state
        /// </summary>
        /// <param name="username"></param>
        public void Refresh()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}