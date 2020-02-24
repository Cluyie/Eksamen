using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.Model;
using XamarinFormsApp.ViewModel;

namespace XamarinFormsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        AccountViewModel _accountViewModel;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = _accountViewModel
                = new AccountViewModel();

        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {

            //if (!string.IsNullOrWhiteSpace(Account.Username) && !string.IsNullOrWhiteSpace(Account.Password))
            //{
            //    //send mig til login api
            //    //await Navigation.PushAsync(/*hjem eller bruger*/);
            //}
            //else
            //{
            //    //else
            //}

            ////
            await Navigation.PopAsync();
        }
    }
}