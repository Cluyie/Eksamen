using System.Threading.Tasks;
using Autofac;
using AutoMapper;
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
        public string ConfirmEmail { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ErrorMessage { get; set; }


        /// <summary>
        ///     Tries to register, returns a bool containing the result
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Register()
        {
            var response = await _proxy.PostAsync(@"Auth/Register", _mapper.Map<Register>(this));
            var result = await ApiClientProxy.ReadAnswerAsync<ApiResponse<string>>(response);
            if (response.IsSuccessStatusCode && result?.Code == ApiResponseCode.OK)
                _authService.Login(result.Value);
            else
                ErrorMessage = _proxy.GenerateErrorMessage(result, response);
            return result?.Code == ApiResponseCode.OK;
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