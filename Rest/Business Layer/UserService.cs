using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Data_Access_Layer;
using Data_Access_Layer.Context;
using Data_Access_Layer.Models;
using Business_Layer.Models;

namespace Business_Layer
{
    public class UserService
    {
        IdentityContext _identityContext;
        AuthService _authService;

        public UserService(IdentityContext identityContext, AuthService authService)
        {
            _identityContext = identityContext;
            _authService = authService;
        }

        public ApiResponse<string> Register(RegisterDTO registerDTO)
        {
            // The email is already in use
            if (GetFromEmail(registerDTO.Email) != null)
            {
                return new ApiResponse<string>(ApiResponseCode.EmailAlreadyTaken, "");
            }

            // Username is already in use
            if(GetFromUsername(registerDTO.Username) != null)
            {
                return new ApiResponse<string>(ApiResponseCode.UsernameAlreadyTaken, "");
            }

            // TODO: Create a real user in identity with hashed password
            User user = new User
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email,
                PasswordHash = ""
            };

            _identityContext.Users.Add(user);
            _identityContext.SaveChanges();

            return new ApiResponse<string>(ApiResponseCode.OK, _authService.GetAuthToken(user));
        }

        public ApiResponse<User> Update(Guid id, User user)
        {
            // Can only update an existing user
            if(GetFromID(id) == null)
            {
                return new ApiResponse<User>(ApiResponseCode.BadRequest, null);
            }

            /* TODO: Uncomment this when authentication is working
            // User can only update self
            if(_authService.GetUser().Id != id.ToString())
            {
                return new ApiResponse<UserData>(ApiResponseCode.UnAuthenticated, null);
            }
            */

            // Update the user
            _identityContext.Attach(user).State = EntityState.Modified;
            _identityContext.SaveChanges();

            return new ApiResponse<User>(ApiResponseCode.OK, user);
        }

        public ApiResponse<string> Login(LoginDTO credentials)
        {
            // TODO: Make proper login functionality. For now it always authenticates
            // if the username matches a user

            User user = GetFromEmail(credentials.UsernameOrEmail);

            // Didn't find a user with that email, try to find by username
            if(user == null)
            {
                user = GetFromUsername(credentials.UsernameOrEmail);
            }

            // Didn't find a user by either email or username, so login fails
            if(user == null)
            {
                return new ApiResponse<string>(ApiResponseCode.BadRequest, "");
            }

            // TODO: Send a proper token instead of just the username
            return new ApiResponse<string>(ApiResponseCode.OK, _authService.GetAuthToken(user));
        }

        // ----- Internal methods -----

        User GetFromID(Guid id)
        {
            return _identityContext.Users.FirstOrDefault(user => user.Id == id.ToString());
        }

        User GetFromEmail(string email)
        {
            return _identityContext.Users.FirstOrDefault(user => user.Email.ToLower() == email.ToLower());
        }

        User GetFromUsername(string username)
        {
            return _identityContext.Users.FirstOrDefault(user => user.UserName.ToLower() == username.ToString());
        }

        User GetUserFromToken(string token)
        {
            // TODO: Validate token against token in the database instead of username
            return _identityContext.Users.FirstOrDefault(user => user.UserName == token);
        }
    }
}
