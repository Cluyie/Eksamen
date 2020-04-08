using Microsoft.AspNetCore.Mvc;
using Producer.Domain.Services.Interfaces;

namespace Consumer.Api.Controllers
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