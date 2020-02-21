using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows;
using XamarinFormsApp.ViewModel;

namespace XamarinFormsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentView
    {
        ProfileViewModel _profileViewModel;
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = _profileViewModel
                = new ProfileViewModel();
        }
    }
}