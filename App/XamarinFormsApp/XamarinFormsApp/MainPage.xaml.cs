using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.View;

namespace XamarinFormsApp
{
  // Learn more about making custom code visible in the Xamarin.Forms previewer
  // by visiting https://aka.ms/xamarinforms-previewer
  [DesignTimeVisible(false)]
  public partial class MainPage : ContentPage
  {
    public MainPage()
    {
      InitializeComponent();
    }

    private void RegisterButton_Clicked(object sender, EventArgs e)
    {
      Navigation.PushAsync(new RegisterPage());
    }

    private void LoginButton_Clicked(object sender, EventArgs e)
    {
      Navigation.PushAsync(new LoginPage());
    }

    private void ProfileButton_Clicked(object sender, EventArgs e)
    {
      Navigation.PushAsync(new ProfilePage());
    }
  }
}
