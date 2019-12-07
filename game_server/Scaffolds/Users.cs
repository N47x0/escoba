using System;
using System.Collections.Generic;

namespace game_server.Scaffolds
{
    public partial class Users
    {
        public Users()
        {
            UserGameSessions = new HashSet<UserGameSessions>();
            UserStatistic = new HashSet<UserStatistic>();
        }

        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        public virtual ICollection<UserGameSessions> UserGameSessions { get; set; }
        public virtual ICollection<UserStatistic> UserStatistic { get; set; }
    }
}
