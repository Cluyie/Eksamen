using Business_Layer;
using Business_Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Identity;

namespace Rest_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        UserService _userService;
        AuthService _authService;

        private IConfiguration _config;

        public AuthController(UserService userService, IConfiguration config, AuthService authService)
        {
            _userService = userService;
            _authService = authService;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<string> Register([FromBody] User userToRegister)
        {
            if (userToRegister == null)
                return new ApiResponse<string>(ApiResponseCode.BadRequest, "");

            return _userService.RegisterAsync(userToRegister).Result;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody]LoginDTO login)
        {
            IActionResult response = Unauthorized();
            User user = _authService.Authenticate(login).Result;

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
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
        }
    }
}