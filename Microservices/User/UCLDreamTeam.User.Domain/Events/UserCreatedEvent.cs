namespace UCLDreamTeam.User.Domain.Events
{
    public class UserCreatedEvent
    {
        public Models.User User { get; set;  }

        public UserCreatedEvent(Models.User user)
        {
            User = user;
        }
    }
}