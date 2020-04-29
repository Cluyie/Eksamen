using Autofac;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using UCLToolBox;
using Xamarin.Forms;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Model;
using Profile = AutoMapper.Profile;
using System;

namespace XamarinFormsApp.ViewModel
{
    public class ChatViewModel : Profile, INotifyPropertyChanged
    {
        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
        public string TextToSend { get; set; }
        public ICommand OnSendCommand { get; set; }

        private readonly string _groupId;
        private readonly HubConnection _hubConnection;
        private readonly Guid _userId;

        public ChatViewModel(string groupId)
        {
            _groupId = groupId;
            _userId = (Application.Current.Properties["UserData"] as User).Id;

            _hubConnection = new HubConnectionBuilder().WithUrl($"{Properties.Resources.SignalRBaseAddress}ChatHub")
                .Build();
            Connect();


            Messages.Add(new Message() { Text = "Hej, du snakker med Maria fra kundeservice. Hvad kan jeg hjælpe med?" });

            _hubConnection.On<Message>("SendMessageToRoom", message => {
                if(message.UserId != _userId)
                    Messages.Add(message);
            });

            OnSendCommand = new Command(() =>
            {
                if (!string.IsNullOrEmpty(TextToSend))
                {
                    Message message = new Message()
                    {
                        Id = Guid.NewGuid(),
                        Text = TextToSend,
                        UserId = _userId,
                        TimeStamp = DateTime.Now,
                        Seen = false
                    };

                    Messages.Add(message);
                    TextToSend = string.Empty;

                    _hubConnection.SendAsync("SendMessageToRoom", message, _groupId).Wait();
                }
            });
        }

        public async Task Stop()
        {
            await _hubConnection.StopAsync();
        }

        private async Task Connect()
        {
            await _hubConnection.StartAsync();

            await _hubConnection.SendAsync("JoinGroup", _groupId);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}