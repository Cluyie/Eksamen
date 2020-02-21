using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.ViewModel;

namespace XamarinFormsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            //var login = (Login)BindingContext;

            if (!string.IsNullOrWhiteSpace(login.UserName) && !string.IsNullOrWhiteSpace(login.Password))
            {
                //send mig til login api
                //await Navigation.PushAsync(/*hjem eller bruger*/);
            }
            else
            {
                //else
            }

            //
            await Navigation.PopAsync();
        }
    }
}