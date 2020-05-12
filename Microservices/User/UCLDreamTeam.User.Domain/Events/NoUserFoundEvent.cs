
namespace UCLDreamTeam.User.Domain.Events
{
    public class NoUserFoundEvent
    {
        public Models.User User { get; set;  }

        public NoUserFoundEvent(Models.User user)
        {
            User = user;
        }
    }
}