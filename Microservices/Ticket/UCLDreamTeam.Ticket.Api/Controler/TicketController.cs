using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UCLDreamTeam.Ticket.Api
{
    [Route("[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        // GET: <controller>
        [HttpGet("{TickedId}")]
        public IEnumerable<string> GetTicked(Guid TickedId)
        {
            return new List<string>{ "test"};
        }

        // GET api/<controller>/5
        [HttpGet("User/{UserId}")]
        public string GetUserTicked(Guid UserId)
        {
            return "test2";
        }
    }
}
