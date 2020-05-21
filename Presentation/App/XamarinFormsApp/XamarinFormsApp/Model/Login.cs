using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace XamarinFormsApp.Model
{
    public class Login : AutoMapper.Profile, ILoginDTO
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}