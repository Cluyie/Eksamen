using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UCLDreamTeam.Auth.Api.Services;
using Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace UCLDreamTeam.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _config;
        private readonly UserService _userService;

        public AuthController(UserService userService, IConfiguration config, AuthService authService)
        {
            _userService = userService;
            _authService = authService;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ApiResponse<string>> LoginAsync([FromBody] ILoginDTO login)
        {
            var response = new ApiResponse<string>(ApiResponseCode.UnAuthenticated, "");
            var user = await _authService.Authenticate(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = new ApiResponse<string>(ApiResponseCode.OK, "Bearer" + " " + tokenString);
            }

            return response;
        }

        private string GenerateJSONWebToken(IUser userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var roles = _userService.GetUserRolesAsync(userInfo).Result;

            claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
            //}
        }
}