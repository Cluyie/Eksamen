using Business_Layer.Models;
using Data_Access_Layer.Context;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
  /// <summary>
  /// Is responsible for authenticating requests, generating and validating
  /// tokens.
  /// </summary>
  public class AuthService
  {
    /// <summary>
    /// The currently authenticated user
    /// </summary>
    private User _user;
    private IdentityContext _identityContext;

    public AuthService(IdentityContext identityContext)
    {
      _identityContext = identityContext;
    }

    /// <summary>
    /// Authenticate the specified user
    /// </summary>
    /// <param name="user"></param>
    public void Authenticate(User user)
    {
      _user = user;
    }

    /// <summary>
    /// Get the currently authenticated user, or null if the request
    /// is not authenticated. Always sets the password hash to null,
    /// might need to change this in the future
    /// </summary>
    /// <returns></returns>
    public User GetUser()
    {
        User userToReturn = _user;
        if (userToReturn != null)
            userToReturn.PasswordHash = null;
        return userToReturn;
    }

    /// <summary>
    /// Gets the auth token for the specified user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public string GetAuthToken(User user)
    {
      // TODO: CHANGE to a real token
      return user.UserName;
    }

    public async Task<User> GetUserFromToken(string token)
    {
      // TODO: Validate token against token in the database instead of username
      return await _identityContext.Users.FirstOrDefaultAsync(user => user.UserName == token);
    }
  }
}
