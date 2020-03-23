using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IRegisterDTO
    {
        string Username { get; set; }
        string Email { get; set; }
        string Password { get; set; }
    }
}
