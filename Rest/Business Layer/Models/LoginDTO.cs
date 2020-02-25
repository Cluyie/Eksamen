using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Models
{
    public class LoginDTO
    {
        public string UsernameOrMail { get; set; }
        public string Password { get; set; }
    }
}
