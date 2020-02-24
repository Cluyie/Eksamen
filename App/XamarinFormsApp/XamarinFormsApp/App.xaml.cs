using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFormsApp
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();
      DependencyService.Register<HttpClient>();
      DependencyService.Register<ApiClientProxy>();
      MainPage = new NavigationPage(new MainPage());
    }

    protected override void OnStart()
    {
      DependencyService.Get<HttpClient>().BaseAddress = new Uri("http://10.0.2.2:5000/Auth/");
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }
  }
}
