using System;
using System.Collections.Generic;

namespace game_server.Scaffolds
{
    public partial class UserGameSessions
    {
        public Guid UserGameSessionId { get; set; }
        public Guid GameSessionId { get; set; }
        public Guid GameStatisticId { get; set; }
        public Guid GameInfoId { get; set; }
        public Guid UserId { get; set; }

        public virtual GameInfo GameInfo { get; set; }
        public virtual GameSessions GameSession { get; set; }
        public virtual GameStatistics GameStatistic { get; set; }
        public virtual Users User { get; set; }
    }
}
