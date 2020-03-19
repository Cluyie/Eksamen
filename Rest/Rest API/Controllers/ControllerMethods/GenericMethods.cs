using System;
using Business_Layer;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Business_Layer.Models;
using Data_Access_Layer.Models;

namespace Rest_API.Controllers.ControllerMethods
{
    public class GenericMethods : ControllerBase
    {
        AuthService _authService;
        ResourceService _resourceService;

        private User _user;

        public GenericMethods(AuthService authService, ResourceService resourceService)
        {
            _authService = authService;
            _resourceService = resourceService;

            _user = _authService.GetUser();
        }

        //Takes a generic T data, and a generic method with return type TR, and validates the user, the data, and finaly, calls the desired method.
        //Returns the ApiResponse of that method.
        public ApiResponse<TR> ValidateAndPerfom<T, TR>(T data, Func<T, ApiResponse<TR>> MethodName) where TR : class
        {
            if (_user == null)
            {
                //Response.StatusCode = StatusCodes.Status401Unauthorized;
                return new ApiResponse<TR>(ApiResponseCode.UnAuthenticated, null);
            }

            if (data == null || !ModelState.IsValid)
            {
                //Response.StatusCode = StatusCodes.Status400BadRequest;
                return new ApiResponse<TR>(ApiResponseCode.BadRequest, null);
            }

            ApiResponse<TR> response = MethodName(data);
            //Response.StatusCode = (int)response.Code;
            return response;
        }

        //Takes a generic method with return type TR, and validates the user. Finaly calls the desired method.
        //Returns the ApiResponse of that method.
        public ApiResponse<TR> ValidateAndPerfom<TR>(Func<ApiResponse<TR>> MethodName) where TR : class
        {
            if (_user == null)
            {
                //Response.StatusCode = StatusCodes.Status401Unauthorized;
                return new ApiResponse<TR>(ApiResponseCode.UnAuthenticated, null);
            }

            ApiResponse<TR> response = MethodName();
            //Response.StatusCode = (int)response.Code;
            return response;
        }
    }
}
