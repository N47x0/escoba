using System;

namespace game_server.Database.Models
{
  public class InitGamePayload {
    public Guid GameSessionId {get; set;}
    public games.GameState GameState {get; set;}
  }

}