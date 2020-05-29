using System;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Ticket.Domain.Models
{
    public class ApiResponse<T> : IApiResponse<T>
    {
        public ApiResponseCode Code { get; set; }
        public T Value { get; set; }
        public Exception Exception { get; set; }

        public ApiResponse(ApiResponseCode code, T value = default, Exception exception = null) 
        { 
            Code = code; 
            Value = value; 
            Exception = exception; 
        }
}
}
