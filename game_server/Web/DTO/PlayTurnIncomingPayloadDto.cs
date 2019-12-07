using System;
using System.Collections.Generic; 
using Newtonsoft.Json;

namespace game_server.Web.DTO
{
	public class PlayTurnIncomingPayloadDto
	{
    [JsonProperty("gameSessionId")]
    public Guid GameSessionId { get; set; }

    [JsonProperty("cardsPlayed")]
    public List<games.Card> CardsPlayed { get; set; }

	}
}