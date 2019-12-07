using System;
using System.Collections.Generic;

namespace game_server.Scaffolds
{
    public partial class GameInfo
    {
        public GameInfo()
        {
            GameSessions = new HashSet<GameSessions>();
            GameStatistics = new HashSet<GameStatistics>();
            Rules = new HashSet<Rules>();
            UserGameSessions = new HashSet<UserGameSessions>();
            UserStatistic = new HashSet<UserStatistic>();
        }

        public Guid GameInfoId { get; set; }
        public string GameName { get; set; }

        public virtual ICollection<GameSessions> GameSessions { get; set; }
        public virtual ICollection<GameStatistics> GameStatistics { get; set; }
        public virtual ICollection<Rules> Rules { get; set; }
        public virtual ICollection<UserGameSessions> UserGameSessions { get; set; }
        public virtual ICollection<UserStatistic> UserStatistic { get; set; }
    }
}
