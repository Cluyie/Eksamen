using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Model;

namespace XamarinFormsApp.ViewModel
{
  public class HomeViewModel : AutoMapper.Profile
  {
    #region Constructor
    private ApiClientProxy _proxy;

    public HomeViewModel()
    {
      _proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
    }
    #endregion

    public string Username
    { 
      get 
      {
        return $"Velkommen {Username ?? "No username"}!";  //TODO Fjern no username
      } 
      set { Username = value; } 
    }

    private async void SetUsername()
    {
      var homeUser = await _proxy.GetAsync<Home>("User/7166f6e1-7de5-4757-8bc3-26145f991a7b");
      Username = homeUser.Username;
    }
  }
}
