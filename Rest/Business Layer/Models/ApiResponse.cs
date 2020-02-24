using System;

namespace Business_Layer.Models
{
    public enum ApiResponseCode : int
    {
        UnAuthenticated = 401,
        OK = 200,
        BadRequest = 400,
        NotModified = 304,
        Created = 201,
        InternalServerError = 500
    }

    public class ApiResponse<T>
    {
        public ApiResponseCode Code
        {
            get; private set;
        }

        public T Value
        {
            get; private set;
        }

        public ApiResponse(ApiResponseCode code, T value)
        {
            Code = code;
            Value = value;
        }
    }
}