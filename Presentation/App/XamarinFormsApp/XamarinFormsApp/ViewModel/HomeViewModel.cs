using Autofac;
using UCLToolBox;
using Xamarin.Forms;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Model;
using Profile = AutoMapper.Profile;

namespace XamarinFormsApp.ViewModel
{
    public class HomeViewModel : Profile
    {
        public string Username { get; set; }

        #region Constructor

        private ApiClientProxy _proxy;

        public HomeViewModel()
        {
            _proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
            Username = (Application.Current.Properties["UserData"] as User).UserName;
        }

        #endregion
    }
}