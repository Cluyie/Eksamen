using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UCLDreamTeam.Auth.Api.Infrastructure.Services;
using UCLDreamTeam.Auth.Api.Models;
using UCLDreamTeam.Auth.Api.Models.DTO;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Auth.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ApiResponse<string>> LoginAsync([FromBody] LoginDTO login)
        {
            if (login == null) return new ApiResponse<string>(ApiResponseCode.BadRequest, "");

            try
            {
                var userToken = await _authService.AuthenticateAsync(login);

                if (userToken != null)
                {
                    return new ApiResponse<string>(ApiResponseCode.OK, "Bearer" + " " + userToken);
                }

                return new ApiResponse<string>(ApiResponseCode.Unauthorized, "");
            }
            catch (Exception e)
            {
                return new ApiResponse<string>(ApiResponseCode.InternalServerError, e.ToString());
            }
        }
    }
}