using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.Model;

namespace XamarinFormsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservationDetails : ContentPage
    {
        public ReservationDetails(ReservationListItem listItem)
        {

            InitializeComponent();
            BindingContext = listItem;
        }
        private void SuportButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QueuePage());
        }
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (await ())
                await Navigation.PushAsync(new ReservationList())

//            Navigation.PushAsync(new ReservationList());
        }
    }
}