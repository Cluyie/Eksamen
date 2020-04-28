using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.SignalR.Client;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLToolBox;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Model;
using Profile = AutoMapper.Profile;
using Newtonsoft.Json;

namespace XamarinFormsApp.ViewModel
{
    public class ResourceViewModel : Profile, INotifyPropertyChanged
    {
        private readonly HubConnection _hubConnection;

        private readonly ApiClientProxy _proxy;
        private ObservableCollection<Resource> _Resources;

        public ResourceViewModel()
        {
            _proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();

            Resources = new ObservableCollection<Resource>();
            _hubConnection = new HubConnectionBuilder().WithUrl($"{Properties.Resources.SignalRBaseAddress}ResourceHub")
                .Build();
            Connect();

            //SignalR Client methods for Resource
            _hubConnection.On<Resource>("CreateResource", resource => { Resources.Add(resource); });

            _hubConnection.On<Resource>("UpdateResource",
                resource =>
                {
                    Resources[Resources.IndexOf(Resources.FirstOrDefault(r => r.Id == resource.Id))] = resource;
                });

            _hubConnection.On<Resource>("DeleteResource",
                resource => { Resources.Remove(Resources.FirstOrDefault(r => r.Id == resource.Id)); });
        }

        public string ErrorMessage { get; private set; }

        public ObservableCollection<Resource> Resources
        {
            get => _Resources;
            set
            {
                _Resources = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ResourceViewModel InitializeWithResourceData()
        {
            var response = _proxy.Get<ApiResponse<List<Resource>>>(@"Resource");

            if (response?.Code != ApiResponseCode.OK) ErrorMessage = _proxy.GenerateErrorMessage(response);

            var resourceViewModel = new ResourceViewModel();
            foreach (var item in response.Value) resourceViewModel.Resources.Add(item);

            return resourceViewModel;
        }

        private async Task Connect()
        {
            await _hubConnection.StartAsync();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}