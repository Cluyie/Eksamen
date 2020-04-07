using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var reservations = await _reservationService.GetReservationsAsync();
            if (reservations == null) return BadRequest();
            if (!reservations.Any()) return NotFound();
            return Ok(reservations);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Domain.Models.Reservation reservation)
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
    }
}