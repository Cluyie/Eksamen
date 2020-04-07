using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Reservation.Application.Interfaces;
using UCLDreamTeam.Reservation.Domain.Commands;
using UCLDreamTeam.Reservation.Domain.Interfaces;

namespace UCLDreamTeam.Reservation.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IEventBus _eventBus;
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IEventBus eventBus, IReservationRepository reservationRepository)
        {
            _eventBus = eventBus;
            _reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<Domain.Models.Reservation>> GetReservationsAsync()
        {
            return await _reservationRepository.GetReservationsAsync();
        }

        public async Task AddAsync(Domain.Models.Reservation reservation)
        {
            var command = new ReservationCommand(reservation.Id, reservation.UserId, reservation.ResourceId,
                reservation.Timeslot);
            await _eventBus.SendCommand(command);
        }
    }
}