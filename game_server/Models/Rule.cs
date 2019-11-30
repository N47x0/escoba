using System;
using System.Collections.Generic; 

namespace game_server.Models
{
  public class Rule {
    public Guid RuleId { get; set; }
    public Guid GameInfoId { get; set; }
    public string RuleName { get; set; }
    public string RuleText { get; set; }
  }

}