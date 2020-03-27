using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanel.Client.DTOs
{
    public class LoginDTO
    {
        public string UsernameOrEmail { get; set; }

        public string Password { get; set; }
    }
}
