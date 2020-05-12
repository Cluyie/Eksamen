using RabitMQEasy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UCLDreamTeam.Reservation.Application.Interfaces;
using UCLDreamTeam.Reservation.Domain.Interfaces;
using UCLDreamTeam.Reservation.Domain.Models;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Reservation.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly RabitMQPublicer _eventBus;
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(RabitMQPublicer eventBus, IReservationRepository reservationRepository)
        {
            _eventBus = eventBus;
            _reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<Domain.Models.Reservation>> GetAsync()
        {
            return await _reservationRepository.GetAsync();
        }

        public async Task<Domain.Models.Reservation> GetByIdAsync(Guid id)
        {
            return await _reservationRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Domain.Models.Reservation>> GetByResourceId(Guid resourceId)
        {
            return await _reservationRepository.GetByResourceId(resourceId);
        }

        public async Task AddAsync(Domain.Models.Reservation reservation)
        {
            try
            {
                await _reservationRepository.AddAsync(reservation);
                _eventBus.PunlicEvent<IReservation<ReserveTime>>(Events.NewObject, reservation);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task UpdateAsync(Domain.Models.Reservation reservation)
        {
            await _reservationRepository.UpdateAsync(reservation);
            _eventBus.PunlicEvent<IReservation<ReserveTime>>(Events.UpdateObject, reservation);
        }

        public async Task CancelById(Guid id)
        {
            Domain.Models.Reservation reservation = await _reservationRepository.GetByIdAsync(id);
            await _reservationRepository.CancelById(id);
            _eventBus.PunlicEvent<IReservation<ReserveTime>>(Events.DeleateObject, reservation);
            
        }
    }
}