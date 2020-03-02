using System.ComponentModel.DataAnnotations;

namespace Data_Access_Layer.Interfaces
{
    public interface IUser
    {
        string Address { get; set; }
        string City { get; set; }
        string Country { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string UserName { get; set; }
        int ZipCode { get; set; }
    }
}