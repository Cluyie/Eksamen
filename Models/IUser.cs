using System.ComponentModel.DataAnnotations;

namespace Models
{
    public interface IUser
    {
        string Address { get; set; }
        string City { get; set; }
        string Country { get; set; }
        [Required]
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        [Required]
        string UserName { get; set; }
        int ZipCode { get; set; }
    }
}