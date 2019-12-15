using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic; 

namespace game_server.Database.Models
{
  public class GameSession
  {
    public Guid GameSessionId { get; set; }
    
    [ForeignKey("GameInfo")]
    public Guid GameInfoId { get; set; }
    
    public Guid GameStatisticId { get; set; }

    public GameInfo GameInfo { get; set; }
    public GameStatistic GameStatistic { get; set; }
    public string GameSessionState  { get; set; } // i.e. running, paused, done
    public ICollection<UserGameSession> UserGameSessions { get; set; } = new List<UserGameSession>();// this should be some new "User" type, not games.CardGame.Player   
    public ICollection<games.GameState> GameStates { get; set; } = new List<games.GameState>();// this will store json-string serializations
  }
}