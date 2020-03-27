using System;
using Business_Layer;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Business_Layer.Models;
using Data_Access_Layer.Models;

namespace Rest_API.Controllers.ControllerMethods
{
    public class RequestValidator : ControllerBase
    {
        AuthService _authService;

        public RequestValidator(AuthService authService)
        {
            _authService = authService;
        }

        //Takes generic data and methods, for validation and execution.
        /// <summary>
        //Takes a generic T data, and a generic method with return type TR, and validates the user, the data, and finaly, calls the desired method.
        //Returns the ApiResponse of that method. Also sets the correct StatusCode to the HttpResponse of the calling controller.
        /// </summary>
        /// <param name="data">The data that you wish to pass to the method.</param>
        /// <param name="MethodName">The method you wish should handle the data.</param>
        /// <param name="recievedResponse">The original HttpResponse from the calling controller.</param>
        public ApiResponse<TR> ValidateAndPerfom<T, TR>(T data, Func<T, ApiResponse<TR>> MethodName, HttpResponse recievedResponse) where TR : class
        {
            if (data == null || !ModelState.IsValid)
            {
                recievedResponse.StatusCode = StatusCodes.Status400BadRequest;
                return new ApiResponse<TR>(ApiResponseCode.BadRequest, null);
            }

            ApiResponse<TR> apiResponse = MethodName(data);
            recievedResponse.StatusCode = (int)apiResponse.Code;
            return apiResponse;
        }

        //Takes generic methods for validation and execution.
        /// <summary>
        //Takes a generic method with return type TR, and validates the user. Finaly calls the desired method.
        //Returns the ApiResponse of that method. Also sets the correct StatusCode to the HttpResponse of the calling controller.
        /// </summary>
        /// <param name="MethodName">The method you wish to call.</param>
        /// <param name="recievedResponse">The original HttpResponse from the calling controller.</param>
        public ApiResponse<TR> ValidateAndPerfom<TR>(Func<ApiResponse<TR>> MethodName, HttpResponse recievedResponse) where TR : class
        {
            ApiResponse<TR> apiResponse = MethodName();
            recievedResponse.StatusCode = (int)apiResponse.Code;
            return apiResponse;
        }
    }
}
