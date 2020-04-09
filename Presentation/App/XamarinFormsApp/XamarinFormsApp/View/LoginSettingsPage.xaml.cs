using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.Model;
using XamarinFormsApp.ViewModel;

namespace XamarinFormsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginSettingsPage : ContentPage
    {
        private readonly LoginSettingsViewModel _loginSettingsViewModel;

        public LoginSettingsPage()
        {
            InitializeComponent();
            var user = Application.Current.Properties["UserData"] as User;
            var loginSettingsViewModel = new LoginSettingsViewModel();
            //If the conversion of the user object is successful, set BindingContext to _loginSettingsViewModel
            _loginSettingsViewModel = loginSettingsViewModel.InitializeWithUserData(user);
            //If _loginSettingsViewModel is null, set it to the empty instantiation of LoginSettingsViewModel
            BindingContext = _loginSettingsViewModel ??= loginSettingsViewModel;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (await _loginSettingsViewModel.UpdateLogin())
                await Navigation.PopAsync();
            else
                await DisplayAlert("Alert", _loginSettingsViewModel.ErrorMessage, "OK");
        }
    }
}