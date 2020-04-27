using AdminPanel.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Client.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApiClient _apiClient;
        public ReservationService(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<Reservation> GetFromId(Guid id)
        {
            var response = await _apiClient.GetAsync<Reservation>($"Reservation/{id.ToString()}");

            return response.Value;
        }
    }
}
