﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Data_Access_Layer;
using Data_Access_Layer.Context;
using Data_Access_Layer.Models;
using Business_Layer.Models;
using System.Threading.Tasks;
using AutoMapper;

namespace Business_Layer
{
  public class UserService
  {
    private IdentityContext _identityContext;
    private AuthService _authService;
    private Mapper _mapper;

    public UserService(IdentityContext identityContext, AuthService authService, Mapper mapper)
    {
      _identityContext = identityContext;
      _authService = authService;
      _mapper = mapper;
    }

    public ApiResponse<string> Register(RegisterDTO registerDTO)
    {
      // The email is already in use
      if (GetFromEmail(registerDTO.Email).Result != null)
      {
        return new ApiResponse<string>(ApiResponseCode.EmailAlreadyTaken, "");
      }

      // Username is already in use
      if (GetFromUsername(registerDTO.Username).Result != null)
      {
        return new ApiResponse<string>(ApiResponseCode.UsernameAlreadyTaken, "");
      }

      // TODO: Create a real user in identity with hashed password
      User user = new User
      {
        UserName = registerDTO.Username,
        Email = registerDTO.Email,
        FirstName = registerDTO.FirstName,
        LastName = registerDTO.LastName,
        PasswordHash = ""
      };

      _identityContext.Users.Add(user);
      _identityContext.SaveChanges();

      return new ApiResponse<string>(ApiResponseCode.OK, _authService.GetAuthToken(user));
    }

    public ApiResponse<User> Update(User userData)
    {
      //Prevent changing the ID
      userData.Id = null;
      User userToChange = _authService.GetUser();
      // Can only update an existing user
      if (userToChange == null)
      {
        return new ApiResponse<User>(ApiResponseCode.UnAuthenticated, null);
      }
      string oldPassword = userToChange.PasswordHash;

      /* TODO: Uncomment this when authentication is working
      // User can only update self
      if(_authService.GetUser().Id != id.ToString())
      {
          return new ApiResponse<UserData>(ApiResponseCode.UnAuthenticated, null);
      }
      */

      // Update the user
      // Automapper is configured to only overwrite the fields that are not null
      _mapper.Map(userData, userToChange);

      _identityContext.Update(userToChange);
      _identityContext.SaveChanges();

      return new ApiResponse<User>(ApiResponseCode.OK, userToChange);
    }

    public ApiResponse<string> Login(LoginDTO credentials)
    {
      // TODO: Make proper login functionality. For now it always authenticates
      // if the username matches a user

      User user = GetFromEmail(credentials.UsernameOrEmail).Result;

      // Didn't find a user with that email, try to find by username
      if (user == null)
      {
        user = GetFromUsername(credentials.UsernameOrEmail).Result;
      }

      // Didn't find a user by either email or username, so login fails
      if (user == null)
      {
        return new ApiResponse<string>(ApiResponseCode.BadRequest, "");
      }

      // TODO: Send a proper token instead of just the username
      return new ApiResponse<string>(ApiResponseCode.OK, _authService.GetAuthToken(user));
    }

    // ----- Internal methods -----

    private async Task<User> GetFromID(string id)
    {
      return await _identityContext.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    private async Task<User> GetFromEmail(string email)
    {
      return await _identityContext.Users.FirstOrDefaultAsync(user => user.Email == email);
    }

    private async Task<User> GetFromUsername(string username)
    {
      return await _identityContext.Users.FirstOrDefaultAsync(user => user.UserName == username);
    }


  }
}