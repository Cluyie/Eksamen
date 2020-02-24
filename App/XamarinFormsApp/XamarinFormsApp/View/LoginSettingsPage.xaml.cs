using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.Model;

namespace XamarinFormsApp.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class LoginSettingsPage : ContentPage
  {
    private ApiClientProxy _proxy { get; set; }
    public LoginSettingsPage(ApiClientProxy proxy)
    {
      InitializeComponent();
      _proxy = proxy;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
      var account = new Account { Email = "Hej", Username="Test", Password="ole12345"};
      await _proxy.PostAsync("Register", account);
      await Navigation.PushAsync(new MainPage());
    }
  }
}