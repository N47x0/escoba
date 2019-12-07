using System;
using System.Collections.Generic;

namespace game_server.Scaffolds
{
    public partial class UserStatistic
    {
        public Guid UserStatisticId { get; set; }
        public Guid UserId { get; set; }
        public Guid GameInfoId { get; set; }
        public int NumberOfPlays { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }

        public virtual GameInfo GameInfo { get; set; }
        public virtual Users User { get; set; }
    }
}
