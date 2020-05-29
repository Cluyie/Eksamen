using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLToolBox;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Model;
using Profile = AutoMapper.Profile;

namespace XamarinFormsApp.ViewModel
{
    public class RegisterViewModel : Profile
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ErrorMessage { get; set; }

        /// <summary>
        ///     Tries to register, returns a bool containing the result
        /// </summary>
        /// <returns></returns>
        public async Task<IdentityResult> Register()
        {
            List<IdentityError> errors = new List<IdentityError>();
            IdentityResult result = new IdentityResult();
            var register = _mapper.Map<Register>(this);
            var registerResponse = await _proxy.PostAsync(@"User", register);

            if (registerResponse.IsSuccessStatusCode)
            {
                var registerResult = await registerResponse.Content.ReadAsAsync<ApiResponse<User>>();

                if (registerResult.Code == ApiResponseCode.OK)
                {
                    var loginResponse = await _proxy.PostAsync(@"Auth/Login", new Login
                    {
                        UsernameOrEmail = Username,
                        Password = PasswordHash
                    });
                    if (loginResponse.IsSuccessStatusCode)
                    {
                      var loginResult = await loginResponse.Content.ReadAsAsync<ApiResponse<string>>();
                      if (loginResult.Code == ApiResponseCode.OK)
                          _authService.Login(loginResult.Value);
                      else
                        errors.Add(new IdentityError
                        {
                            Code = loginResult.Code.ToString() /*"Request failed"*/,
                            Description = loginResult.Value
                        });
                    }
                    else
                    {
                      errors.Add(new IdentityError
                      {
                          Code = loginResponse.StatusCode.ToString() /*"Request failed"*/,
                          Description = loginResponse.ReasonPhrase
                      });
                    }
                }
                else
                {
                    errors.Add(new IdentityError
                    {
                        Code = registerResponse.StatusCode.ToString() /*"Request failed"*/,
                        Description = registerResponse.ReasonPhrase
                    });
                }
            }
            else
            {
                errors.Add(new IdentityError
                {
                    Code = registerResponse.StatusCode.ToString() /*"Request failed"*/,
                    Description = registerResponse.ReasonPhrase
                });
            }
            return errors.Count != 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }

        #region Constructor

        private readonly ApiClientProxy _proxy;
        private readonly Mapper _mapper;
        private readonly AuthService _authService;

        public RegisterViewModel()
        {
            _proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
            _mapper = AutofacHelper.Container.Resolve<Mapper>();
            _authService = AutofacHelper.Container.Resolve<AuthService>();
        }

      #endregion
    }
}