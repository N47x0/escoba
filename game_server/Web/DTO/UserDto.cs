using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using game_server.Database.Models;

namespace game_server.Web.DTO
{
  public class UserDto
  {
    [JsonProperty("userId")]
    public Guid UserId { get; set; }

    [JsonProperty("firstName")]
    public string FirstName { get; set; }

    [JsonProperty("lastName")]
    public string LastName { get; set; }

    [JsonProperty("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonProperty("gameSessions")]
    public ICollection<UserGameSession> GameSessions { get; set; }
    [JsonProperty("userStatistics")]
    public ICollection<UserStatistic> UserStatistics { get; set; }

  }
}