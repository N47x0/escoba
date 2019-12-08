using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using game_server.Database.Models;

namespace game_server.Web.DTO
{
  public class UserStatisticDto
  {
    [JsonProperty("userStatisticId")]
    public Guid UserStatisticId { get; set; }

    [JsonProperty("userId")]
    public Guid UserId { get; set; }

    [JsonProperty("user")]
    public User User { get; set; }

    [JsonProperty("gameInfoId")]
    public Guid GameInfoId { get; set; }

    [JsonProperty("gameInfo")]
    public GameInfo GameInfo { get; set; }

    [JsonProperty("numberOfPlays")]
    public int NumberOfPlays { get; set; }

    [JsonProperty("wins")]
    public int Wins { get; set; }

    [JsonProperty("losses")]
    public int Losses { get; set; }

    [JsonProperty("draws")]
    public int Draws { get; set; }

    [JsonProperty("gameStatistics")]
    public ICollection<GameStatistic> GameStatistics { get; set; }


  }
}