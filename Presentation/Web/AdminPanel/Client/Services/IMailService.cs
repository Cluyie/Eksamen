using AdminPanel.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Client.Services
{
    public interface IMailService
    {
        public Task SendChatLog(List<Message> messages);
    }
}
