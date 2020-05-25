using Xamarin.Forms;
using XamarinFormsApp.Model;
using XamarinFormsApp.View;
using System;

namespace XamarinFormsApp.Helpers
{
    class ChatTemplateSelector : DataTemplateSelector
    {
        DataTemplate incomingDataTemplate;
        DataTemplate outgoingDataTemplate;

        private readonly Guid _userId;

        public ChatTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));

            _userId = (Application.Current.Properties["UserData"] as User).Id;
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as Message;
            if (messageVm == null)
                return null;


            return (messageVm.UserId == _userId) ? outgoingDataTemplate : incomingDataTemplate;
        }

    }
}