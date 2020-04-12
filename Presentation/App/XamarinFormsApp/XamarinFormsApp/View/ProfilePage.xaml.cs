using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.Model;
using XamarinFormsApp.ViewModel;

namespace XamarinFormsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            if (await _profileViewModel.UpdateProfile())
                await Navigation.PopAsync();
            else
                await DisplayAlert("Alert", _profileViewModel.ErrorMessage, "OK");
        }

        private void LoginSettingsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginSettingsPage());
        }

        #region Constructor

        private readonly ProfileViewModel _profileViewModel;

        public ProfilePage()
        {
            InitializeComponent();
            var user = Application.Current.Properties["UserData"] as User;
            var profileViewModel = new ProfileViewModel();
            //If the conversion of the user object is successful, set BindingContext to _profileViewModel
            _profileViewModel = profileViewModel.InitializeWithUserData(user);
            //If _profileViewModel is null, set it to the empty instantiation of ProfileViewModel
            BindingContext = _profileViewModel ??= profileViewModel;
        }

        #endregion
    }
}