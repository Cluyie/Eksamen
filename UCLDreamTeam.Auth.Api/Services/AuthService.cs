using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Interfaces;
using UCLDreamTeam.Auth.Api.Infrastructure;
using UCLDreamTeam.Auth.Api.Models;

namespace UCLDreamTeam.Auth.Api.Services
{
    public class AuthService
    {
        private readonly AuthRepository _authRepository;
        private readonly IConfiguration _config;

        public AuthService(AuthRepository authRepository, IConfiguration config)
        {
            _authRepository = authRepository;
            _config = config;
        }


        /// <summary>
        ///     Authenticate the specified user
        /// </summary>
        /// <param name="user"></param>
        public async Task<string> Authenticate(ILoginDTO user)
        {
            var userIn = await _authRepository.GetUserFromUserNameOrEmailAsync(user.UsernameOrEmail);

            if (userIn != null)
            {
                var success = await HashMatch(user.Password, userIn.PasswordHash);

                if (success)
                {
                    return GenerateJSONWebToken(userIn);
                }
            }

            return string.Empty;
        }

        public async Task<bool> HashMatch(string password, string hash)
        {
            return true;
        }

        private string GenerateJSONWebToken(AuthUser authUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, authUser.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Email, authUser.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, authUser.RoleId.ToString())
            };

            //claims.AddRange(role(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
