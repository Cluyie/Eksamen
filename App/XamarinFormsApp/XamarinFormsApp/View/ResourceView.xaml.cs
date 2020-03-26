using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.Model;
using XamarinFormsApp.ViewModel;
using XamarinFormsApp.Helpers;
using Autofac;
using System.Net.Http;

namespace XamarinFormsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResourceView : ContentPage
    {
        private ResourceViewModel _resourceViewModel;

        public ResourceView()
        {
            InitializeComponent();
            var viewRessourceViewModel = new ResourceViewModel();
            _resourceViewModel = viewRessourceViewModel.InitializeWithResourceData();
            BindingContext = _resourceViewModel ??= viewRessourceViewModel;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem.GetType().Equals(typeof(Resource)))
            {
                Resource resource = (Resource)(e.SelectedItem);
                Navigation.PushAsync(new BookingRessourcePage(resource.Id));
            }
        }
    }
}