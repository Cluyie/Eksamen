namespace Models.Interfaces
{
    public interface IRegisterDTO
    {
        string Username { get; set; }
        string Email { get; set; }
        string Password { get; set; }
    }
}