using System;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Resource.Domain.Models
{
    public enum ApiResponseCode
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

    public class ApiResponse<T> : IApiResponse<T>
    {
        public ApiResponseCode Code { get; set; }
        SharedInterfaces.Interfaces.ApiResponseCode IApiResponse<T>.Code { get; set; }

        public T Value { get; set;  }

        public Exception Exception { get; set; }

        public ApiResponse(ApiResponseCode code, T value = default)
        {
            Code = code;
            Value = value;
        }
    }
}