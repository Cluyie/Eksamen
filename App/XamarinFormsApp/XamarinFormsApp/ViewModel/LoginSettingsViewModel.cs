using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinFormsApp.Model;

namespace XamarinFormsApp.ViewModel
{
  public class LoginSettingsViewModel
  {
    #region Constructor
    private ApiClientProxy _proxy;

    public LoginSettingsViewModel()
    {
      _proxy = DependencyService.Get<ApiClientProxy>();
    }
    #endregion

    public string Email { get; set; }
    public string Password { get; set; }

    public async void UpdateLogin()
    {
      var account = new Account { Email = "Hej", Username = "Test", Password = "ole12345" };
      await _proxy.PostAsync("Register", account);
    }
  }
}
