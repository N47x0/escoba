using System;
using System.Collections.Generic; 
using Newtonsoft.Json;
using game_server.Database.Models;

namespace game_server.Web.DTO
{
	public class PlayTurnPayloadDto
	{
    [JsonProperty("gameSessionId")]
    public Guid GameSessionId { get; set; }

    [JsonProperty("gameState")]
    public games.GameState GameState { get; set; }

	}
}