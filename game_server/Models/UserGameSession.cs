using System;
using System.Collections.Generic; 

namespace game_server.Models
{
  public class UserGameSession 
  {
    public Guid GameSessionId {get; set;}
    public GameSession GameSession {get;set;}
    public Guid UserId {get; set;}
    public User User {get; set;}
  }

}