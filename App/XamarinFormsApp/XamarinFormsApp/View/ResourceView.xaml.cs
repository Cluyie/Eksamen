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

        ResourceViewModel _resourceViewModel;
        private HubConnection _hubConnection;
        

        public ResourceView()
        {
            InitializeComponent();
            
            var viewRessourceViewModel = new ResourceViewModel();
            _resourceViewModel = viewRessourceViewModel.InitializeWithResourceData();
            BindingContext = _resourceViewModel ??= viewRessourceViewModel;

            _hubConnection = new HubConnectionBuilder().WithUrl($"{Properties.Resources.SignalRBaseAddress}SignalR/ResourceHub").Build();

            //SignalR Client methods for UpdateResource
            _hubConnection.On<Resource>("UpdateResource", (resource) =>
            {
                if (_resourceViewModel.Resources.Find(r => r.Id == resource.Id) != null)
                {
                    _resourceViewModel.Resources[_resourceViewModel.Resources.FindIndex(r => r.Id == resource.Id)] = resource;
                    BindingContext =_resourceViewModel;
                }
                else
                {
                    _resourceViewModel.Resources.Add(resource);
                    BindingContext = _resourceViewModel;
                }
            });

        }

        
    }

}