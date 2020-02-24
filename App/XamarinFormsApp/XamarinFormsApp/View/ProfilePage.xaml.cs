using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinFormsApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFormsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        ProfileViewModel _profileViewModel;
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = _profileViewModel
                = new ProfileViewModel();
        }

        private void UpdateButton_Clicked(object sender, EventArgs e)
        {

        }

        private void LoginSettingsButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}