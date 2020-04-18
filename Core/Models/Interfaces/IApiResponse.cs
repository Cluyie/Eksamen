namespace UCLDreamTeam.SharedInterfaces.Interfaces
{

    public interface IApiResponse<T> where T : class
    {
        ApiResponseCode Code { get; set; }

        T Value { get; set; }
    }
}