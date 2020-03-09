using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API.Middleware
{
  public static class RequestTokenValidationMiddlewareExtentions
  {
    public static IApplicationBuilder UseTokenValidation(
        this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<TokenValidation>();
    }

  }
}
