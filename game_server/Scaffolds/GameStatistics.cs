using System;
using System.Collections.Generic;

namespace game_server.Scaffolds
{
    public partial class GameStatistics
    {
        public GameStatistics()
        {
            UserGameSessions = new HashSet<UserGameSessions>();
        }

        public Guid GameStatisticId { get; set; }
        public Guid GameInfoId { get; set; }
        public Guid GameSessionId { get; set; }
        public string FinalScore { get; set; }
        public bool HumanWin { get; set; }
        public bool AiWin { get; set; }
        public bool Draw { get; set; }
        public DateTime GameStart { get; set; }
        public DateTime GameEnd { get; set; }

        public virtual GameInfo GameInfo { get; set; }
        public virtual GameSessions GameSessions { get; set; }
        public virtual ICollection<UserGameSessions> UserGameSessions { get; set; }
    }
}
