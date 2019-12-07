using System;
using System.Collections.Generic; 

namespace game_server.Database.Models
{
  public class GameInfo {
    public Guid GameInfoId {get; set;}
    public string GameName {get;set;}
    public ICollection<Rule> Rules { get; set; }
    public ICollection<UserGameSession> GameSessions { get; set; }
    public ICollection<GameStatistic> GameStatistics {get; set; }
  }
}