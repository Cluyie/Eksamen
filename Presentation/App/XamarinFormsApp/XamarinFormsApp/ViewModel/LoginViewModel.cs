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
        public string UsernameOrEmail { get; set; } = "admin";
        public string Password { get; set; } = "P@ssw0rd";

        public string ErrorMessage { get; private set; }

        /// <summary>
        ///     Tries to register, returns a bool containing the result
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Login()
        {
            var response = await _proxy.PostAsync(@"Auth/Login", _mapper.Map<Login>(this));
            var result = await ApiClientProxy.ReadAnswerAsync<ApiResponse<string>>(response);
            if (response.IsSuccessStatusCode && result?.Code == ApiResponseCode.OK && result.Value != null)
            {
                //Gemmer user token
                _authService.Login(result.Value);

                return true;
            }
            else
            {
                ErrorMessage = "Noget gik galt. Fejl: " + response.StatusCode.ToString();

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