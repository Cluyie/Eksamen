using Business_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Data_Access_Layer;

namespace Business_Layer
{
    /// <summary>
    /// Is responsible for authenticating requests, generating and validating
    /// tokens.
    /// </summary>
    public class AuthService
    {
        private UserData _user;

        /// <summary>
        /// Authenticate the specified user
        /// </summary>
        /// <param name="user"></param>
        public void Authenticate(UserData user)
        {
            _user = user;
        }

        /// <summary>
        /// Get the currently authenticated user, or null if the request
        /// is not authenticated.
        /// </summary>
        /// <returns></returns>
        public UserData GetUser()
        {
            return _user;
        }

        /// <summary>
        /// Generates (DOESN'T STORE) a auth token for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetAuthToken(UserData user)
        {
            // TODO: CHANGE to a real token
            return user.UserName;
        }
    }
}
