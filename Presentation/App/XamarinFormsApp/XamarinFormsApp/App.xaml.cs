using Autofac;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLToolBox;
using Xamarin.Forms;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Model;
using XamarinFormsApp.View;

namespace XamarinFormsApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //DependencyService.Register<HttpClient>();
            //DependencyService.Register<ApiClientProxy>();

            AutofacHelper.Initialize();

            //TODO remove in future
            //var authService = AutofacHelper.Container.Resolve<AuthService>();
            //var api = AutofacHelper.Container.Resolve<ApiClientProxy>();
            //var response = api.Post("Auth/Login", new Login { UsernameOrEmail="test", Password = "P@ssw0rd" });
            //var token = ApiClientProxy.ReadAnswer<ApiResponse<string>>(response).Value;
            //authService.Login(token);

            //MainPage = new NavigationPage(new ResourceView());

            MainPage = new NavigationPage(new MainPage());
        }
        protected override void OnStart()
        {
            //DependencyService.Get<HttpClient>().BaseAddress = new Uri("http://10.0.2.2:5000/Auth/");
        }
        public static string User = "TestUser";

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}