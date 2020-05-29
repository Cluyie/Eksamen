using AdminPanel.Client.DTOs;
using AdminPanel.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Client.Services
{
    public class ApiMailService : IMailService
    {
        private readonly ApiClient _client;

        public ApiMailService(ApiClient client)
        {
            _client = client;
        }

        public async Task SendChatLog(TicketDTO ticketDTO)
        {
            try
            {
                Console.WriteLine($"Ticket nr. {ticketDTO?.Ticket?.Id} mail send");
            }
            catch (Exception e)
            {
                await _client.PostAsync<List<Message>>("Mail/SendChatLog", ticketDTO);
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
