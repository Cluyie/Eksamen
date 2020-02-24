using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
      await Navigation.PushAsync(new MainPage());
    }
  }
}