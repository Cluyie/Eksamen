namespace Models.Interfaces
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

    public interface IApiResponse<T> where T : class
    {
        ApiResponseCode Code { get; set; }

        T Value { get; set; }
    }
}