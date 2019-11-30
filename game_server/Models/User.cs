using System;
using System.Collections.Generic; 

namespace game_server.Models
{
  public class User {
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public ICollection<UserGameSession> GameSessions { get; set; }
    public ICollection<UserStatistic> UserStatistics {get; set; }
  }

}