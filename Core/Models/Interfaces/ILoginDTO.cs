namespace Models.Interfaces
{
    public interface ILoginDTO
    {
        string UsernameOrEmail { get; set; }
        string Password { get; set; }
    }
}