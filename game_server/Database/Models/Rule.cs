using System;
using System.Collections.Generic; 

namespace game_server.Database.Models
{
  public class Rule {
    public Guid RuleId { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid GameInfoId { get; set; }
    public GameInfo GameInfo { get; set; }
    public string RuleName { get; set; }
    public string RuleText { get; set; }
  }
}