using Business_Layer.Models;
using Data_Access_Layer.Context;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Business_Layer
{
/// <summary>
/// Is responsible for authenticating requests, generating and validating
/// tokens.
/// </summary>
    public class AuthService
    {
        private SignInManager<User> _signInManager;
        private UserService _userService;

        public AuthService(SignInManager<User> signInManager, UserService userService)
        {
            _signInManager = signInManager;
            _userService = userService;
        }

        /// <summary>
        /// Authenticate the specified user
        /// </summary>
        /// <param name="user"></param>
        public async Task<User> Authenticate(LoginDTO user)
        {
            User userIn = _userService.GetUserFromUserNameAsync(user.UsernameOrEmail).Result ?? _userService.GetUserFromEmailAsync(user.UsernameOrEmail).Result;

            User userOut = null;

            if (userIn != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(userIn, user.Password, false);

                if (result.Succeeded)
                {
                    userOut = userIn;
                    return userOut;
                }
            }
            return userOut;
        }

        /// <summary>
        /// Get the currently authenticated user, or null if the request
        /// is not authenticated. Always sets the password hash to null,
        /// might need to change this in the future
        /// </summary>
        /// <returns></returns>
    }
}
