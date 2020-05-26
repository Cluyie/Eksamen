using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Auth.Api.Models
{
    public class ApiResponse<T> : IApiResponse<T>
    {
        public ApiResponseCode Code { get; set; }
        public T Value { get; set; }
        public Exception Exception { get; set; }

        public ApiResponse(ApiResponseCode code, T value = default, Exception exception = null) { Code = code; Value = value; Exception = exception; }
        public ApiResponse(ApiResponseCode code, Exception exception = null, T value = default) { Code = code; Value = value; Exception = exception; }

    }
}
