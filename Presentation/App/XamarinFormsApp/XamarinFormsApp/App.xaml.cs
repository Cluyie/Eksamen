using Xamarin.Forms;
using XamarinFormsApp.Helpers;
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

            MainPage = new NavigationPage(new MainPage());
        }
        public static string User = "TestUser";

        protected override void OnStart()
        {
            //DependencyService.Get<HttpClient>().BaseAddress = new Uri("http://10.0.2.2:5000/Auth/");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}