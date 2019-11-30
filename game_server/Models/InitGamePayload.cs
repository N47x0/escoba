using System;
using System.Collections.Generic; 

namespace game_server.Models
{
  public class InitGamePayload {
    public Guid SessionId {get; set;}
    public games.GameState GameState {get; set;}
  }

}