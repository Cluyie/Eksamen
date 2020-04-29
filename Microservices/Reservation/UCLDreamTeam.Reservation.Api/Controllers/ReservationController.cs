using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UCLDreamTeam.Reservation.Application.Interfaces;
using UCLDreamTeam.Reservation.Domain.Models;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Reservation.Api.Controllers
{
    [Authorize]
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
        public async Task<ApiResponse<IEnumerable<Domain.Models.Reservation>>> Get()
        {
            try
            {
                var reservations = await _reservationService.GetAsync();
                if (reservations == null)
                    return new ApiResponse<IEnumerable<Domain.Models.Reservation>>(ApiResponseCode.BadRequest,
                        null); //return BadRequest();
                if (!reservations.Any())
                    return new ApiResponse<IEnumerable<Domain.Models.Reservation>>(ApiResponseCode.NotFound,
                        null); //return NotFound();
                return new ApiResponse<IEnumerable<Domain.Models.Reservation>>(ApiResponseCode.OK,
                    reservations); //return Ok(reservations);
            }
            catch (Exception e)
            {
                return new ApiResponse<IEnumerable<Domain.Models.Reservation>>(ApiResponseCode.ServiceUnavailable,
                    null); //return StatusCode(503, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<Domain.Models.Reservation>> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return new ApiResponse<Domain.Models.Reservation>(ApiResponseCode.BadRequest,
                    null); //return BadRequest();
            try
            {
                var reservation = await _reservationService.GetByIdAsync(id);
                if (reservation == null)
                    return new ApiResponse<Domain.Models.Reservation>(ApiResponseCode.NotFound,
                        null); //return NotFound();
                return new ApiResponse<Domain.Models.Reservation>(ApiResponseCode.OK,
                    reservation); //return Ok(reservation);
            }
            catch (Exception e)
            {
                return new ApiResponse<Domain.Models.Reservation>(ApiResponseCode.ServiceUnavailable,
                    null); //return StatusCode(503, e.Message);
            }
        }

        [HttpGet("Resource/{resourceId}")]
        public async Task<ApiResponse<IEnumerable<Domain.Models.Reservation>>> GetByResourceId(Guid resourceId)
        {
            if (resourceId == Guid.Empty)
                return new ApiResponse<IEnumerable<Domain.Models.Reservation>>(ApiResponseCode.BadRequest,
                    null); //return BadRequest();
            try
            {
                var reservation = await _reservationService.GetByResourceId(resourceId);
                if (reservation == null)
                    return new ApiResponse<IEnumerable<Domain.Models.Reservation>>(ApiResponseCode.NotFound,
                        null); //return NotFound();
                return new ApiResponse<IEnumerable<Domain.Models.Reservation>>(ApiResponseCode.OK,
                    reservation); //return Ok(reservation);
            }
            catch (Exception e)
            {
                return new ApiResponse<IEnumerable<Domain.Models.Reservation>>(ApiResponseCode.ServiceUnavailable,
                    null); //return StatusCode(503, e.Message);
            }
        }

        [HttpPost]
        public async Task<ApiResponse<Domain.Models.Reservation>> CreateReservation(
            [FromBody] Domain.Models.Reservation reservation)
        {
            if (reservation == null)
                return new ApiResponse<Domain.Models.Reservation>(ApiResponseCode.BadRequest,
                    reservation); //return BadRequest();
            try
            {
                await _reservationService.AddAsync(reservation);
                return new ApiResponse<Domain.Models.Reservation>(ApiResponseCode.OK,
                    reservation); //return Ok(reservation);
            }
            catch (Exception e)
            {
                return new ApiResponse<Domain.Models.Reservation>(ApiResponseCode.ServiceUnavailable,
                    null); //return StatusCode(503, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse<Guid>> CancelById(Guid id)
        {
            if (id == Guid.Empty) return new ApiResponse<Guid>(ApiResponseCode.BadRequest, id); //return BadRequest();
            try
            {
                await _reservationService.CancelById(id);
                return new ApiResponse<Guid>(ApiResponseCode.OK, id); //return Ok(id);
            }
            catch (Exception e)
            {
                return new ApiResponse<Guid>(ApiResponseCode.ServiceUnavailable,
                    Guid.Empty); //return StatusCode(503, e.Message);
            }
        }
    }
}