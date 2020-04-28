using System;
using UCLToolBox;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.ViewModel;

namespace XamarinFormsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly LoginViewModel _loginViewModel;
        private ApiClientProxy _proxy;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = _loginViewModel
                = new LoginViewModel();
            _proxy = DependencyService.Get<ApiClientProxy>();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            //if (!string.IsNullOrWhiteSpace(Register.Username) && !string.IsNullOrWhiteSpace(Register.Password))
            //{
            //    //send mig til login api
            //    //await Navigation.PushAsync(/*hjem eller bruger*/);
            //}
            //else
            //{
            //    //else
            //}

            bool success = await _loginViewModel.Login();

            if (success)
                await Navigation.PushAsync(new HomePage());
            else
                await DisplayAlert("Alert", _loginViewModel.ErrorMessage, "OK");
        }
    }
}