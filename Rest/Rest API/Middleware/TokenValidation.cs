using Business_Layer;
using Business_Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest_API.Middleware
{
  public class TokenValidation : Attribute
  {
    private readonly RequestDelegate _next;

    public TokenValidation(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context, AuthService authService)
    {
      if (context.Request.Headers.ContainsKey("Token"))
      {
        string token = context.Request.Headers["Token"];
        var user = await authService.GetUserFromToken(token);
        if (user != null)
        {
          authService.Authenticate(user);
        }
      }
      // Call the next delegate/middleware in the pipeline
      await _next(context);
    }
  }
}
