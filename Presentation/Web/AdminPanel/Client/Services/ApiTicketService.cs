using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminPanel.Client.Models;
using System.Linq;
using AdminPanel.Client.DTOs;

namespace AdminPanel.Client.Services
{
    public class ApiTicketService : ITicketService
    {
        private readonly ApiClient _client;

        public ApiTicketService(ApiClient client)
        {
            _client = client;
        }

        public async Task<Ticket> GetByIdAsync(Guid id)
        {
            var response = await _client.GetAsync<Ticket>($"/Ticket?ticketId={id}");
           
            return response.Value;
        }

        public async Task<List<Ticket>> GetByUserIdAsync(Guid userId)
        {
            var response = await _client.GetAsync<List<Ticket>>($"Ticket/User?userId={userId}");
            return response.Value ?? new List<Ticket>();
        }

        public async Task UpdateTicket(Ticket ticket)
        {
            await _client.PutAsync<Ticket>($"Ticket", ticket);
        }
    }
}