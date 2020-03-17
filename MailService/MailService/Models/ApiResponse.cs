using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Interfaces;

namespace MailService.Models
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
