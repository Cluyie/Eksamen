using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.Model;
using XamarinFormsApp.ViewModel;

namespace XamarinFormsApp.View
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservationList : ContentPage
    {
        
        private readonly ReservationListViewModel _reservationListViewModel;

        public ReservationList()
        {
            InitializeComponent();
            var reservationListViewModel = new ReservationListViewModel();           
            _reservationListViewModel = reservationListViewModel.initialize();
            BindingContext = _reservationListViewModel ??= reservationListViewModel;
          
            
        }






         void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as ReservationListItem;
            Navigation.PushAsync(new ReservationDetails(content));
        }
    }
}


