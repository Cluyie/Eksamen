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
        /// <summary>
        /// Authenticate the specified user
        /// </summary>
        /// <param name="user"></param>
        void Authenticate(UserData user)
        {

        }

        /// <summary>
        /// Get the currently authenticated user, or null if the request
        /// is not authenticated.
        /// </summary>
        /// <returns></returns>
        UserData GetUser()
        {

        }

        /// <summary>
        /// Generates (DOESN'T STORE) a auth token for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string GenerateAuthToken(UserData user)
        {

        }
    }
}
