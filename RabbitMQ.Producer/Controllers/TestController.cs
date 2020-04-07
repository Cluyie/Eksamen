using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Producer.Domain.Services.Interfaces;

namespace RabbitMQ.Producer.Controllers
{
    [ApiController]
    [Route("")]
    public class TestController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public TestController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string message)
        {
            if (message == null) return BadRequest();
            await _messageService.Send(message);
            return Ok(message);
        }
    }
}