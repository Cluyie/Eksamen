using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.Model;
using XamarinFormsApp.ViewModel;

namespace XamarinFormsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResourceView : ContentPage
    {
        private readonly ResourceViewModel _resourceViewModel;

        public ResourceView()
        {
            InitializeComponent();

            var viewRessourceViewModel = new ResourceViewModel();
            _resourceViewModel = viewRessourceViewModel.InitializeWithResourceData();
            BindingContext = _resourceViewModel ??= viewRessourceViewModel;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as Resource;
            Navigation.PushAsync(new BookingRessourcePage(content.Reservations));
        }
    }
}