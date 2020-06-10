using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLToolBox;
using Xamarin.Forms;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Model;
using Profile = AutoMapper.Profile;

namespace XamarinFormsApp.ViewModel
{
    public class ProfileViewModel : Profile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int? ZipCode { get; set; }
        public string Country { get; set; }

        public string ErrorMessage { get; private set; }


        public async Task<bool> UpdateProfile()
        {
            var profile = _mapper.Map<Model.Profile>(this);
            var user = _mapper.Map<User>(profile);
            user.Id = ((User) Application.Current.Properties["UserData"]).Id;
            var response = await _proxy.PutAsync(@"User", user);
            var result = await ApiClientProxy.ReadAnswerAsync<ApiResponse<User>>(response);
            if (response.IsSuccessStatusCode)
            {
                _authService.UpdateUser(result.Value);
                return true;
            }
            else
            {
                ErrorMessage = "Noget gik galt. Fejl: " + response.StatusCode.ToString();
                return false;
            }
        }

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
    }
}