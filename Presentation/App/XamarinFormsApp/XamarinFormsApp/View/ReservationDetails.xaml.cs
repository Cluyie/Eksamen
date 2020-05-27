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
    public partial class ReservationDetails : ContentPage
    {
        private readonly ReservationListItem _listItem;
        private readonly ReservationDetailsViewModel _DetailVieWModel;
        public ReservationDetails(ReservationListItem listItem)
        {
           
            InitializeComponent();
            _DetailVieWModel = new ReservationDetailsViewModel();
            _listItem = listItem;
            BindingContext = listItem;
        }
        private void SuportButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QueuePage(_listItem.Id));
        }
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
           
            if (await (_DetailVieWModel.DeleteReservation(_listItem.Id))) await Navigation.PushAsync(new ReservationList());


            //            Navigation.PushAsync(new ReservationList());
        }
    }
}