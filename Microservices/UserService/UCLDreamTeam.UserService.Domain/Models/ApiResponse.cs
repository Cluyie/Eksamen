﻿using Models.Interfaces;

namespace UCLDreamTeam.UserServiceApi.Domain.Models
{
    public class ApiResponse<T> : IApiResponse<T>
        where T : class
    {
        public ApiResponse(ApiResponseCode code, T value)
        {
            Code = code;
            Value = value;
        }

        public ApiResponseCode Code { get; set; }
        public T Value { get; set; }
    }
}