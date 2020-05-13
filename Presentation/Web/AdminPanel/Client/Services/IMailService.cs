using AdminPanel.Client.DTOs;
using AdminPanel.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Client.Services
{
    public interface IMailService
    {
        public Task SendChatLog(TicketDTO ticketDTO);
    }
}
