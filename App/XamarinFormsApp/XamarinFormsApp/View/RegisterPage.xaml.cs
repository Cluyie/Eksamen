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
  public partial class RegisterPage : ContentPage
  {
    public RegisterPage()
    {
      InitializeComponent();
    }

    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
      var proxy = DependencyService.Get<ApiClientProxy>();
      var account = new Account { Email = "Hej", Username = "Test", Password = "ole12345" };
      await proxy.PostAsync("Register", account);
      await Navigation.PopAsync();
    }
  }
}