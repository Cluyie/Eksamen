﻿using System.ComponentModel.DataAnnotations;

namespace Models.Interfaces
{

    public interface IApiResponse<T> where T : class
    {
        ApiResponseCode Code { get; set; }

        T Value { get; set; }
    }
}