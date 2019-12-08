using System;
using System.Collections.Generic; 

namespace game_server.Database.Models
{
  public class GameInfo {
    public Guid GameInfoId { get; set; }
    public string GameName { get; set; }
    public ICollection<Rule> Rules { get; set; }  = new List<Rule>();
    public ICollection<GameSession> GameSessions { get; set; } = new List<GameSession>();
    public ICollection<UserGameSession> UserGameSessions { get; set; } = new List<UserGameSession>();
    public ICollection<GameStatistic> GameStatistics { get; set; } = new List<GameStatistic>();
    public ICollection<UserStatistic> UserStatistics { get; set; } = new List<UserStatistic>();
  }
}