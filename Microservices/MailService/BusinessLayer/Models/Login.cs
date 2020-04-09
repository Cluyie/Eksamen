using Models.Interfaces;

namespace BusinessLayer.Models
{
    public class Login : ILoginDTO
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}