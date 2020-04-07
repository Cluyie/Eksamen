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

        [HttpGet]
        public IActionResult Get()
        {
            return Content("Hello from Consumer");
        }
    }
}