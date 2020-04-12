using Models.Interfaces;
using Models.Mail;

namespace UCLDreamTeam.Mail.Domain.Models
{
    public class Reservation
    {
        public ReserveTime Timeslot { get; set; }
        public User Recipent { get; set; }
        public Resource Resource { get; set; }
    }
}