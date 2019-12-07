using System;
using System.Collections.Generic; 
using Newtonsoft.Json;
using game_server.Database.Models;

namespace game_server.Web.DTO
{
	public class GameSessionDto
	{
    [JsonProperty("gameSessionId")]
    public Guid GameSessionId { get; set; }

    [JsonProperty("gameInfoId")]
    public Guid GameInfoId { get; set; }

    [JsonProperty("gameInfo")]
    public GameInfo GameInfo { get; set; }

    [JsonProperty("gameSessionState")]
    public string GameSessionState { get; set; }

    [JsonProperty("userGameSessions")]

    public ICollection<UserGameSession> UserGameSessions { get; set; }

    [JsonProperty("gameStatistic")]
    public GameStatistic GameStatistic { get; set; }

    [JsonProperty("gameStates")]
    public ICollection<games.GameState> GameStates { get; set; }

	}
}