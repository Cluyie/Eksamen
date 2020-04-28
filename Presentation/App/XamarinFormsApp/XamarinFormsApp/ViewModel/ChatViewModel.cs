using Autofac;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using UCLToolBox;
using Xamarin.Forms;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Model;
using Profile = AutoMapper.Profile;

namespace XamarinFormsApp.ViewModel
{
    public class ChatViewModel : Profile
    {
        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
        public string TextToSend { get; set; }
        public ICommand OnSendCommand { get; set; }

        public ChatViewModel()
        {
            Messages.Add(new Message() { Text = "Hej, du snakker med Maria fra kundeservice. Hvad kan jeg hjælpe med?" });

            OnSendCommand = new Command(() =>
            {
                if (!string.IsNullOrEmpty(TextToSend))
                {
                    Messages.Add(new Message() { Text = TextToSend, Username = App.User });
                    TextToSend = string.Empty;
                }

            });
        }

        //public event PropertyChangedEventHandler PropertyChanged;
    }
}