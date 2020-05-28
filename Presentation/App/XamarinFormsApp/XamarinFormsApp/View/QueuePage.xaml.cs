using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.Model;
using XamarinFormsApp.ViewModel;

namespace XamarinFormsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QueuePage : ContentPage
    {
        public QueuePage(Guid? reservationId = null)
        {
            InitializeComponent();

            var viewModel = new QueueViewModel(reservationId);

            BindingContext = viewModel;

            viewModel.ReceivedGroupId += (id, ticketId) =>
            {
                Navigation.PushAsync(new ChatPage(id, ticketId));
            };
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}