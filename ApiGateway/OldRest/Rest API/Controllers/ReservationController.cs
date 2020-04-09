using System;
using Business_Layer;
using Business_Layer.Models;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rest_API.Controllers.ControllerMethods;

namespace Rest_API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly RequestValidator _requestValidator;
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService, RequestValidator requestValidator)
        {
            _reservationService = reservationService;
            _requestValidator = requestValidator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Reservation> CreateReservation([FromBody] Reservation reservation)
        {
            return _requestValidator.ValidateAndPerfom(reservation, _reservationService.Create, Response);
        }

        [HttpGet("guid={guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Reservation> GetReservationById([FromRoute] Guid guid)
        {
            return _requestValidator.ValidateAndPerfom(guid, _reservationService.Get, Response);
        }

        [HttpDelete("guid={guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse<Reservation> CancelReservationById([FromRoute] Guid guid)
        {
            return _requestValidator.ValidateAndPerfom(guid, _reservationService.Cancel, Response);
        }
    }
}