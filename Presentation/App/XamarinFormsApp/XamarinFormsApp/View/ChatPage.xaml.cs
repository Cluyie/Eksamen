using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.ViewModel;

namespace XamarinFormsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        private readonly ChatViewModel _viewModel;

        public ChatPage(string groupId)
        {
            InitializeComponent();
            _viewModel = new ChatViewModel(groupId);
            BindingContext = _viewModel;
        }
        private async void OnExitButtonClicked(object sender, EventArgs e)
        {
            await _viewModel.Stop();
            await Navigation.PushAsync(new HomePage());
        }
    }
}