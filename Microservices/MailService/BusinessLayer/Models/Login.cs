using Models.Interfaces;

namespace UCLDreamTeam.Mail.Domain.Models
{
    public class Login : ILoginDTO
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}