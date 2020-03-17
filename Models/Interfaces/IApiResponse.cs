﻿using System;

namespace Models.Interfaces
{
  public enum ApiResponseCode : int
  {
    OK = 200,
    Created = 201,
    NoContent = 204,
    NotModified = 304,
    BadRequest = 400,
    UnAuthenticated = 401,
    InternalServerError = 500,
    EmailAlreadyTaken = 1001,
    UsernameAlreadyTaken = 1002
  }

  public interface IApiResponse<T>
    {
         ApiResponseCode Code { get; set; }

        T Value { get; set; }
    }
}