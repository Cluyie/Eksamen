namespace UCLDreamTeam.User.Domain.Events
{
    public class UserDeletedEvent
    {
        public Models.User User { get; set;  }

        public UserDeletedEvent(Models.User user)
        {
            User = user;
        }
    }
}