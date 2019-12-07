using System;
using System.Collections.Generic; 
using Newtonsoft.Json;
using game_server.Database.Models;

namespace game_server.Web.DTO
{
	public class GameInfoDto
	{
    [JsonProperty("gameInfoId")]
    public Guid GameInfoId { get; set; }

    [JsonProperty("gameName")]
    public string GameName { get; set; }

    [JsonProperty("rules")]
    public ICollection<Rule> Rules { get; set; }

    [JsonProperty("gameSessions")]
    public ICollection<GameSession> GameSessions { get; set; }

    [JsonProperty("userGameSessions")]
    public ICollection<UserGameSession> UserGameSessions { get; set; }

    [JsonProperty("gameStatistics")]
    public ICollection<GameStatistic> GameStatistics { get; set; }

    [JsonProperty("gameStatistic")]
    public GameStatistic GameStatistic { get; set; }
	}
}