using System;
using Xamarin.Forms;

namespace XamarinFormsApp.Helpers
{
  internal class AuthService
  {
    private ApiClientProxy _proxy;

    public AuthService(ApiClientProxy proxy)
    {
      _proxy = proxy;
    }

    public void Login(string token)
    {
      Application.Current.Properties["Token"] = token;
      _proxy.httpClient.DefaultRequestHeaders.Add("Token", token);
    }
  }
}