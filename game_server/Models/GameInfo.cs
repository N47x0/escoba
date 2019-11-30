using System;
using System.Collections.Generic; 

namespace game_server.Models
{
  public class GameInfo {
    public Guid GameInfoId {get; set;}
    public string GameName {get;set;}
    public ICollection<Rule> Rules { get; set; }
    public ICollection<UserGameSession> GameSessions { get; set; }
    public ICollection<GameStatistic> GameStats {get; set; }
  }
}