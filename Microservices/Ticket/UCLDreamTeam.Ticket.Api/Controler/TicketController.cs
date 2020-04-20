using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UCLDreamTeam.Ticket.Domain.Models;
using UCLDreamTeam.Ticket.Application.Services;
using System.Linq;

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
        public async Task<Domain.Models.Ticket> GetTicket(Guid TickedId)
        {

            return await TicketService.GetByIdAsync(TickedId);
        }

        // GET api/<controller>/5
        [HttpGet("User/{UserId}")]
        public async Task<IEnumerable<Guid>> GetUserTicket(Guid UserId)
        {
            return (await TicketService.GetByUserIdAsync(UserId)).ToList().Select(T => T.Id);
        }
    }
}
