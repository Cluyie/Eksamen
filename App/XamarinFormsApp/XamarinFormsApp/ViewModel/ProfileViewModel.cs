using Autofac;
using AutoMapper;
using System;
using System.Net;
using System.Threading.Tasks;
using XamarinFormsApp.Helpers;
using Models;
using UCLToolBox;
using Models.Interfaces;
using XamarinFormsApp.Model;

namespace XamarinFormsApp.ViewModel
{
    public class ProfileViewModel : AutoMapper.Profile
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int? ZipCode { get; set; }
        public string Country { get; set; }

        public string ErrorMessage { get; private set; }

        #region Constructor
        private ApiClientProxy _proxy;
        private Mapper _mapper;
        private AuthService _authService;

        public ProfileViewModel()
        {
            _proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
            _mapper = AutofacHelper.Container.Resolve<Mapper>();
            _authService = AutofacHelper.Container.Resolve<AuthService>();
        }

        public ProfileViewModel InitializeWithUserData(User user)
        {
            return _mapper.Map<ProfileViewModel>(_mapper.Map<Model.Profile>(user));
        }
        #endregion


        public async Task<bool> UpdateProfile()
        {
            Model.Profile profile = _mapper.Map<Model.Profile>(this);
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