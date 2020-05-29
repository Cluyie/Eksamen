using System;

namespace UCLDreamTeam.SharedInterfaces.Interfaces
{

    public interface IApiResponse<T>
    {
        ApiResponseCode Code { get; set; }

        T Value { get; set; }

        Exception Exception { get; set; }
    }
}