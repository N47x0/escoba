using System;
using System.Collections.Generic; 

namespace game_server.Database.Models
{
  public class User {
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public ICollection<Rule> Rules { get; set; } = new List<Rule>();
    public ICollection<UserGameSession> UserGameSessions { get; set; } = new List<UserGameSession>();
    public ICollection<UserStatistic> UserStatistics {get; set; } = new List<UserStatistic>();
  }
}