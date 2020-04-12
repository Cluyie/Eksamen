namespace XamarinFormsApp.Model
{
    public class LoginSettings : AutoMapper.Profile
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}