using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using UCLToolBox;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.ViewModel;

namespace XamarinFormsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private readonly RegisterViewModel _registerViewModel;

        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = _registerViewModel =
                new RegisterViewModel();
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            var result = await _registerViewModel.Register();
            if (result == IdentityResult.Success)
                await Navigation.PushAsync(new HomePage());
            else
                await DisplayAlert("Alert", result.Errors.SelectMany(e => e.Description).ToSingularString(), "OK");
        }
    }
}