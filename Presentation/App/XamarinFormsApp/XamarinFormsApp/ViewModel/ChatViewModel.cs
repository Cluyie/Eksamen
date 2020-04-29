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

namespace XamarinFormsApp.ViewModel
{
    public class ChatViewModel : Profile, INotifyPropertyChanged
    {
        private readonly HubConnection _hubConnection;
        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
        public string TextToSend { get; set; }
        public ICommand OnSendCommand { get; set; }

        public ChatViewModel()
        {
            _hubConnection = new HubConnectionBuilder().WithUrl($"{Properties.Resources.SignalRBaseAddress}ChatHub")
                .Build();
            Connect();


            Messages.Add(new Message() { Text = "Hej, du snakker med Maria fra kundeservice. Hvad kan jeg hjælpe med?" });

            _hubConnection.On<Message>("SendMessageToRoom", message => { Messages.Add(message); });

            OnSendCommand = new Command(() =>
            {
                if (!string.IsNullOrEmpty(TextToSend))
                {
                    Messages.Add(new Message() { Text = TextToSend, Username = App.User });
                    TextToSend = string.Empty;
                }

            });
        }

        private async Task Connect()
        {
            await _hubConnection.StartAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}