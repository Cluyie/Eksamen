using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Model;
using Models;
using UCLToolBox;

namespace XamarinFormsApp.ViewModel
{
    public class LoginSettingsViewModel : AutoMapper.Profile
    {
        #region Constructor
        private ApiClientProxy _proxy;
        private Mapper _mapper;
        private AuthService _authService;

        public LoginSettingsViewModel()
        {
            _proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
            _mapper = AutofacHelper.Container.Resolve<Mapper>();
            _authService = AutofacHelper.Container.Resolve<AuthService>();
        }

        public LoginSettingsViewModel InitializeWithUserData(User user)
        {
            return _mapper.Map<LoginSettingsViewModel>(_mapper.Map<LoginSettings>(user));
        }
        #endregion

        public string Email { get; set; }
        public string Password { get; set; }

        public string ErrorMessage { get; private set; }

        public async Task<bool> UpdateLogin()
        {
            LoginSettings profile = _mapper.Map<LoginSettings>(this);
            User user = _mapper.Map<User>(profile);
            var response = await _proxy.PutAsync(@"User/UpdateProfile", user);
            var result = await ApiClientProxy.ReadAnswerAsync<ApiResponse<User>>(response);
            if (response.IsSuccessStatusCode && result?.Code == ApiResponseCode.OK)
            {
                _authService.UpdateUser(result.Value);
            }
            else
            {
                ErrorMessage = _proxy.GenerateErrorMessage(result, response);
            }
            return result?.Code == ApiResponseCode.OK;
        }


    }
}
