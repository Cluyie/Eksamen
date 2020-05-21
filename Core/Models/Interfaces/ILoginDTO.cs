namespace UCLDreamTeam.SharedInterfaces.Interfaces
{
    public interface ILoginDTO
    {
        string UsernameOrEmail { get; set; }
        string Password { get; set; }
    }
}