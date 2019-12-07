using System;
using System.Collections.Generic;

namespace game_server.Scaffolds
{
    public partial class Rules
    {
        public Guid RuleId { get; set; }
        public Guid GameInfoId { get; set; }
        public string RuleName { get; set; }
        public string RuleText { get; set; }

        public virtual GameInfo GameInfo { get; set; }
    }
}
