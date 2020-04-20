using System.Collections.Generic;
using System;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;

namespace AdminPanel.Client.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly AuthCredentialsKeeper _credentialsKeeper;

        public CustomAuthStateProvider(AuthCredentialsKeeper credentialsKeeper)
        {
            _credentialsKeeper = credentialsKeeper;
        }

        /// <summary>
        ///     Create the initial authenticationstate when loading the site for the first time
        /// </summary>
        /// <returns></returns>
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Username is null, so return an empty claimsprinipal, so we are NOT authenticated
            if (!_credentialsKeeper.HasCredentials())
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));

            IEnumerable < Claim > claims = ParseClaimsFromJwt(_credentialsKeeper.Token);

            //Add username to the claims
            claims.Append(new Claim(ClaimTypes.Name, _credentialsKeeper.Username));

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "ApiWebAuth"));
            return Task.FromResult(new AuthenticationState(user));
        }

        /// <summary>
        ///     Re-calculate the users authentication state
        /// </summary>
        /// <param name="username"></param>
        public void Refresh()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }
            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}