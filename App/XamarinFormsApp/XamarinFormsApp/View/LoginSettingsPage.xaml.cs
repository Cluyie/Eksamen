using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.Model;
using XamarinFormsApp.ViewModel;

namespace XamarinFormsApp.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class LoginSettingsPage : ContentPage
  {
    private LoginSettingsViewModel _loginSettingsViewModel;

    private ApiClientProxy _proxy { get; set; }
    public LoginSettingsPage()
    {
      InitializeComponent();
      _proxy = DependencyService.Get<ApiClientProxy>();
      BindingContext = _loginSettingsViewModel = new LoginSettingsViewModel();
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
      if (await _loginSettingsViewModel.UpdateLogin())
      {
        await Navigation.PopAsync();
      }
      else
      {
        //TODO Notify user of error
      }
    }
  }
}