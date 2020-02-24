using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinFormsApp.Model;

namespace XamarinFormsApp.ViewModel
{
  public class LoginViewModel
  {
    #region Constructor
    private ApiClientProxy _proxy;

    public LoginViewModel()
    {
      _proxy = DependencyService.Get<ApiClientProxy>();
    }
    #endregion

    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }

    public async void Login()
    {
      var login = new Login { UsernameOrEmail = UsernameOrEmail, Password = Password };
      await _proxy.PostAsync("Login", login);
    }
  }
}
