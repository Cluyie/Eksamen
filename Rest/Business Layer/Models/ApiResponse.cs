using System;

namespace Business_Layer.Models
{
    public enum ApiResponseCode : int
    {
        NotAuthenticated = 0,
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

        public string CodeDescription
        {
            get; private set;
        }

        public ApiResponse(ApiResponseCode code, T value)
        {
            Code = code;
            Value = value;
        }

        public ApiResponse(ApiResponseCode code, T value, string codeDescription)
        {
            Code = code;
            Value = value;
            CodeDescription = codeDescription;
        }
    }
}