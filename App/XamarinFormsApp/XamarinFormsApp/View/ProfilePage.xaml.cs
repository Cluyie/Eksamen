using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinFormsApp.ViewModel;
using XamarinFormsApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFormsApp.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ProfilePage : ContentPage
  {
    ProfileViewModel _profileViewModel;
    private ApiClientProxy _proxy;

    public ProfilePage()
    {
      InitializeComponent();
      BindingContext = _profileViewModel
          = new ProfileViewModel();
      _proxy = DependencyService.Get<ApiClientProxy>();
    }

    private void UpdateButton_Clicked(object sender, EventArgs e)
    {
      _profileViewModel.UpdateProfile();
      Navigation.PopAsync();
    }

    private void LoginSettingsButton_Clicked(object sender, EventArgs e)
    {
      Navigation.PushAsync(new LoginSettingsPage());
    }
  }
}