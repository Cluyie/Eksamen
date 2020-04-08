using Models.Interfaces;

namespace UCLDreamTeam.User.Domain.Models
{
    public class ApiResponse<T> : IApiResponse<T>
        where T : class
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
