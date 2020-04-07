using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Models.Interfaces;

namespace XamarinFormsApp.Model
{
  public class ApiResponse<T> : IApiResponse<T> where T : class
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
