using System;

namespace Business_Layer.Models
{
    public enum ApiResponseCode : int
    {
        NotAuthenticated = 0,
        Success = 1
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