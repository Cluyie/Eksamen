using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UCLDreamTeam.Reservation.Application.Interfaces;

namespace UCLDreamTeam.Reservation.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.Models.Reservation>>> Get()
        {
            try
            {
                var reservations = await _reservationService.GetAsync();
                if (reservations == null) return BadRequest();
                if (!reservations.Any()) return NotFound();
                return Ok(reservations);
            }
            catch (Exception e)
            {
                return StatusCode(503, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();
            try
            {
                var reservation = await _reservationService.GetByIdAsync(id);
                if (reservation == null) return NotFound();
                return Ok(reservation);
            }
            catch (Exception e)
            {
                return StatusCode(503, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] Domain.Models.Reservation reservation)
        {
            if (reservation == null) return BadRequest();
            try
            {
                await _reservationService.AddAsync(reservation);
                return Ok(reservation);
            }
            catch (Exception e)
            {
                return StatusCode(503, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelById(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();
            try
            {
                await _reservationService.CancelById(id);
                return Ok(id);
            }
            catch (Exception e)
            {
                return StatusCode(503, e.Message);
            }
        }
    }
}