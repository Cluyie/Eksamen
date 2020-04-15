using AdminPanel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AdminPanel.Server.Controllers
{
    public class TestController : ControllerBase
    {
        public int Test()
        {
            return 1;
        }
    }
}