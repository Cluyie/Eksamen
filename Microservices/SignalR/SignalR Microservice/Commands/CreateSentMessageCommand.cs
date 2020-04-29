using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Microservice.Commands
{
    public class CreateSentMessageCommand : SentMessageCommand
    {
        public CreateSentMessageCommand(string username, string content)
        {
            Username = username;
            Content = content;
        }
    }
}
