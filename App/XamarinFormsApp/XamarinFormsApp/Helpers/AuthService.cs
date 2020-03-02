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
      //TODO IMPLEMENT THIS
      //No method gets the user currently
      Application.Current.Properties["UserData"] = _proxy.GetAsync<User>("User/GetProfile");
    }
  }
}