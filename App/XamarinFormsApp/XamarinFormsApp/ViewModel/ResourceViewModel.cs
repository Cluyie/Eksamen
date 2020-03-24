using XamarinFormsApp.Helpers;
using System.Collections.ObjectModel;
using Autofac;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using Models.Interfaces;
using XamarinFormsApp.Model;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;

namespace XamarinFormsApp.ViewModel
{
    public class ResourceViewModel : AutoMapper.Profile, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ApiClientProxy _proxy;
        private HubConnection _hubConnection;

        public string ErrorMessage { get; private set; }
        private ObservableCollection<Resource> _Resources;

        public ObservableCollection<Resource> Resources
        {
            get
            {
                return _Resources;
            }
            set
            {
                _Resources = value;
                OnPropertyChanged();
            }
        }

        public ResourceViewModel()
        {
            _proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();

            Resources = new ObservableCollection<Resource>();
            _hubConnection = new HubConnectionBuilder().WithUrl($"{Properties.Resources.SignalRBaseAddress}ResourceHub").Build();
            Connect();

            //SignalR Client methods for Resource
            _hubConnection.On<Resource>("CreateResource", (resource) =>
            {
                Resources.Add(resource);
            });

            _hubConnection.On<Resource>("UpdateResource", (resource) =>
            {
                var oldResource = Resources.FirstOrDefault(r => r.Id == resource.Id);
                if (oldResource != null)
                {
                    oldResource = resource;
                }
            });

            _hubConnection.On<Resource>("DeleteResource", (resource) =>
            {
                Resources.Remove(resource);
            });
        }

        public ResourceViewModel InitializeWithResourceData()
        {
            var response = _proxy.Get<ApiResponse<List<Resource>>>(@"Resource");

            if (response?.Code != ApiResponseCode.OK)
            {
                ErrorMessage = _proxy.GenerateErrorMessage(response);
            }

            var resourceViewModel = new ResourceViewModel();
            foreach (var item in response.Value)
            {
                resourceViewModel.Resources.Add(item);
            }

            return resourceViewModel;
        }

        private async Task Connect()
        {
            await _hubConnection.StartAsync();
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}