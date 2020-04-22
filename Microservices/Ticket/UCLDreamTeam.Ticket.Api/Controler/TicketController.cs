﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UCLDreamTeam.Ticket.Domain.Models;
using UCLDreamTeam.Ticket.Application.Services;
using System.Linq;
using UCLDreamTeam.SharedInterfaces.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UCLDreamTeam.Ticket.Api
{
    [Route("[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        TicketService TicketService { get; set; }
        public TicketController(TicketService ticketService)
        {
            TicketService = ticketService;
        }

        // GET: <controller>
        [HttpGet("{TickedId}")]
        public async Task<ApiResponse<Domain.Models.Ticket>> GetTicket(Guid TickedId)
        {
            try
            {
                var ticket = await TicketService.GetByIdAsync(TickedId);
                if (ticket == null)
                    return new ApiResponse<Domain.Models.Ticket>(ApiResponseCode.NotFound, null); //return NotFound();
                return new ApiResponse<Domain.Models.Ticket>(ApiResponseCode.OK, ticket); //return Ok(ticket);
            }
            catch (Exception e)
            {
                return new ApiResponse<Domain.Models.Ticket>(ApiResponseCode.ServiceUnavailable, null); //return StatusCode(503, e.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet("User/{UserId}")]
        public async Task<ApiResponse<IEnumerable<Domain.Models.Ticket>>> GetUserTicket(Guid userId)
        {
            try
            {
                var result = await TicketService.GetByUserIdAsync(userId);
                if (result == null)
                    return new ApiResponse<IEnumerable<Domain.Models.Ticket>>(ApiResponseCode.BadRequest, null); //return BadRequest();
                if (!result.Any())
                    return new ApiResponse<IEnumerable<Domain.Models.Ticket>>(ApiResponseCode.NotFound, null); //return NotFound();
                return new ApiResponse<IEnumerable<Domain.Models.Ticket>>(ApiResponseCode.OK, result); //return Ok(ticket);
            }
            catch (Exception e)
            {
                return new ApiResponse<IEnumerable<Domain.Models.Ticket>>(ApiResponseCode.ServiceUnavailable,
                    null); //return StatusCode(503, e.Message);
            }
        }
    }
}