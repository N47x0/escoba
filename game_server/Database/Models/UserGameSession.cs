using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace game_server.Database.Models
{
  public class UserGameSession 
  {
    public Guid UserGameSessionId { get; set; }

    [ForeignKey("GameSessions")]
    public Guid GameSessionId { get; set; }

    [ForeignKey("GameStatistics")]
    public Guid GameStatisticId { get; set; } 

    [ForeignKey("GameInfo")]
    public Guid GameInfoId { get; set; }
    public GameInfo GameInfo { get; set; }
    public GameSession GameSession { get; set; }
    public GameStatistic GameStatistic { get; set; }

    [ForeignKey("Users")]
    public Guid UserId { get; set; }
    public User User { get; set; }
  }
}