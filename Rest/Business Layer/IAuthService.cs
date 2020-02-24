using Business_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Data_Access_Layer;
using Data_Access_Layer.Models;

namespace Business_Layer
{
    /// <summary>
    /// Is responsible for authenticating requests, generating and validating
    /// tokens.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Authenticate the specified user
        /// </summary>
        /// <param name="user"></param>
        void Authenticate(User user);

        /// <summary>
        /// Get the currently authenticated user, or null if the request
        /// is not authenticated.
        /// </summary>
        /// <returns></returns>
        User GetUser();

        /// <summary>
        /// Generates (DOESN'T STORE) a auth token for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string GenerateAuthToken(User user);
    }
}
