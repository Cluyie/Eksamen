using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.ViewModel;

namespace XamarinFormsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private void LogoutButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void ProfileButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ResourceView());
        }

        private void Reservationer_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ReservationList());
        }

        #region Constructor

        private HomeViewModel _homeViewModel;

        public HomePage()
        {
            InitializeComponent();
            BindingContext = _homeViewModel =
                new HomeViewModel();
        }

        #endregion
    }
}