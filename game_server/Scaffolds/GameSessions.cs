using System;
using System.Collections.Generic;

namespace game_server.Scaffolds
{
    public partial class GameSessions
    {
        public GameSessions()
        {
            UserGameSessions = new HashSet<UserGameSessions>();
        }

        public Guid GameSessionId { get; set; }
        public Guid GameInfoId { get; set; }
        public Guid GameStatisticId { get; set; }
        public string GameSessionState { get; set; }
        public string GameStates { get; set; }

        public virtual GameInfo GameInfo { get; set; }
        public virtual GameStatistics GameStatistic { get; set; }
        public virtual ICollection<UserGameSessions> UserGameSessions { get; set; }
    }
}
