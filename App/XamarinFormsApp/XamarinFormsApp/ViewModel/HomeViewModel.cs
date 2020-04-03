using Autofac;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UCLToolBox;
using Xamarin.Forms;
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
      Username = (Application.Current.Properties["UserData"] as User).UserName.ToString();
    }
    #endregion

    public string Username { get; set; }
  }
}
