using Business_Layer;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

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
        public ApiResponse<IReservation> CreateReservation([FromBody] Reservation reservation)
        {
            if (reservation == null || !ModelState.IsValid)
                return new ApiResponse<IReservation>(ApiResponseCode.BadRequest, reservation);

            return _reservationService.Create(reservation);
        }

        [HttpGet("guid={guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<IReservation> GetReservationById([FromRoute]Guid guid)
        {
            if (guid == null)
                return new ApiResponse<IReservation>(ApiResponseCode.BadRequest, null);

            return _reservationService.Get(guid);
        }
    }
}
