using System;
using System.Collections.Generic; 

namespace game_server.Database.Models
{
  public class PlayTurnIncomingPayload {
    public Guid GameSessionId {get; set;}
    public List<games.Card> CardsPlayed { get; set; }
  }
}