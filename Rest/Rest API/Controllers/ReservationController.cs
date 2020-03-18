using Business_Layer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Data_Access_Layer.Models;
using Business_Layer.Models;

namespace Rest_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Reservation> CreateReservation([FromBody] Reservation reservation)
        {
            if (reservation == null || !ModelState.IsValid)
                return new ApiResponse<Reservation>(ApiResponseCode.BadRequest, reservation);

            return _reservationService.Create(reservation);
        }

        [HttpGet("guid={guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Reservation> GetReservationById([FromRoute]Guid guid)
        {
            if (guid == null)
                return new ApiResponse<Reservation>(ApiResponseCode.BadRequest, null);

            return _reservationService.Get(guid);
        }

        [HttpDelete("guid={guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Reservation> CancelReservationById([FromRoute]Guid guid)
        {
            if (guid == null)
                return new ApiResponse<Reservation>(ApiResponseCode.BadRequest, null);

            return _reservationService.Cancel(guid);
        }
    }
}
