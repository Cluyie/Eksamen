using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public interface ILoginDTO
    {
        string UsernameOrEmail { get; set; }
        string Password { get; set; }
    }
}
