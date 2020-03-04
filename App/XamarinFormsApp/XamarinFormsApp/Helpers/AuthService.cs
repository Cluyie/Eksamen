using System;
using Models;
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
      _proxy.httpClient.DefaultRequestHeaders.Add("Token", token);
      Application.Current.Properties["Token"] = token;
      var user = _proxy.Get<User>("User/GetProfile");
      Application.Current.Properties["UserData"] = user;
    }
  }
}