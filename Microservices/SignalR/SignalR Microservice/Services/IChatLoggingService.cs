using SignalR_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Microservice.Services
{
    public interface IChatLoggingService
    {
        Task SendMessageAsync(Message message);
    }
}
