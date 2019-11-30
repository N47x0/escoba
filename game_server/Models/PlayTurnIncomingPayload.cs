using System;
using System.Collections.Generic; 

namespace game_server.Models
{
  public class PlayTurnIncomingPayload {
    public Guid SessionId {get; set;}
    public List<games.Card> CardsPlayed { get; set; }
  }
}