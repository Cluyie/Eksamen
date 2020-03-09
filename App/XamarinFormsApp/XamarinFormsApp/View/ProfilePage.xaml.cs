using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using XamarinFormsApp.ViewModel;
using XamarinFormsApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFormsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        #region Constructor
        ProfileViewModel _profileViewModel;

        public ProfilePage()
        {
            InitializeComponent();
            User user = Application.Current.Properties["UserData"] as User;
            var profileViewModel = new ProfileViewModel();
            //If the conversion of the user object is successful, set BindingContext to _profileViewModel
            _profileViewModel = profileViewModel.InitializeWithUserData(user);
            //If _profileViewModel is null, set it to the empty instantiation of ProfileViewModel
            BindingContext = _profileViewModel ??= profileViewModel;
        }
        #endregion

        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            if (await _profileViewModel.UpdateProfile())
            {
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Alert", _profileViewModel.ErrorMessage, "OK");
            }
        }

        private void LoginSettingsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginSettingsPage());
        }
    }
}