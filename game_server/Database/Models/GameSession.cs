using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic; 

namespace game_server.Database.Models
{
  public class GameSession
  {
    public Guid GameSessionId { get; set;}
    
    [ForeignKey("GameInfo")]
    public Guid GameInfoId {get; set;}
    public GameInfo GameInfo {get; set;}
    public string GameSessionState {get;set;} // i.e. running, paused, done
    public ICollection<UserGameSession> UserPlayers { get; set;} // this should be some new "User" type, not games.CardGame.Player   
    public ICollection<games.GameState> GameStates {get;set;} // this will store json-string serializations
  }
}