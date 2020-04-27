using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Ticket.Domain.Models
{
    public class ApiResponse<T> : IApiResponse<T>
    {
        public ApiResponseCode Code { get; set; }
        public T Value { get; set; }

        public ApiResponse(ApiResponseCode code, T value)
        {
            Code = code;
            Value = value;
        }
    }
}
