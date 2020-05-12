
namespace UCLDreamTeam.User.Domain.Events
{
    public class UserUpdatedEvent 
    {
        public Models.User User { get; set;  }

        public UserUpdatedEvent(Models.User user)
        {
            User = user;
        }
    }
}