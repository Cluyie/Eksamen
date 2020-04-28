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
    public class LoginViewModel : Profile
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }

        public string ErrorMessage { get; private set; }

        /// <summary>
        ///     Tries to register, returns a bool containing the result
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Login()
        {
            var response = await _proxy.PostAsync(@"Auth/Login", _mapper.Map<Login>(this));
            string token = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode && token != null)
            {
                //Gemmer user token
                _authService.Login(token);

                return true;
            }
            else
            {
                // Auth API no longer returns ApiResult, so we wrap the returned string into
                // a ApiResponse object ourselves, and guess that it is an unauthorized response
                ErrorMessage = _proxy.GenerateErrorMessage(new ApiResponse<string>(
                    ApiResponseCode.Unauthorized, null), response);

                return false;
            }
        }

        #region Constructor

        private readonly ApiClientProxy _proxy;
        private readonly Mapper _mapper;
        private readonly AuthService _authService;

        public LoginViewModel()
        {
            _proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
            _mapper = AutofacHelper.Container.Resolve<Mapper>();
            _authService = AutofacHelper.Container.Resolve<AuthService>();
        }

        #endregion
    }
}