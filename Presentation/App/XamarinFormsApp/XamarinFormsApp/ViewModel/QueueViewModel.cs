using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.SignalR.Client;
using UCLToolBox;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Model;
using Profile = AutoMapper.Profile;
using System;
using Xamarin.Forms;
using System.Net.Http;

namespace XamarinFormsApp.ViewModel
{
    public class QueueViewModel : Profile, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _index = 420;
        public int Index
        {
            get => _index;
            private set
            {
                _index = value;
                OnPropertyChanged("Index");
            }
        }

        public event Action<string> ReceivedGroupId;

        private readonly ApiClientProxy _proxy;
        private Guid _ticketId;

        private HubConnection _hubConnection;

        public QueueViewModel(Guid? reservationId)
        {
            var proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();

            Ticket ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                Active = true,
                Name = (Application.Current.Properties["UserData"] as User).UserName,
                ReservationId = reservationId,
                Status = UCLDreamTeam.SharedInterfaces.Status.Active,
            };

            var user = Application.Current.Properties["UserData"] as User;

            ticket.UserTickets = new List<UserTicket> 
            { 
                new UserTicket 
                { 
                    TicketId = ticket.Id, 
                    UserId = user.Id 
                }
            };

            HttpResponseMessage response = proxy.Post("Ticket", ticket);

            _ticketId = ticket.Id;

            _hubConnection = new HubConnectionBuilder().WithUrl($"{Properties.Resources.SignalRBaseAddress}QueueHub")
                .Build();

            ConnectToHub();

            // Done waiting in queue
            _hubConnection.On<string>("ReceiveGroupId", id =>
            {
                ReceivedGroupId?.Invoke(id);
            });

            // Your index in the queue
            _hubConnection.On<int>("ReceiveIndex", index =>
            {
                Index = index;
            });

            _hubConnection.SendAsync("Enqueue", _ticketId);
        }

        private async void ConnectToHub()
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
