using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace game_server.Database.Models
{
  public class UserGameSession 
  {
    public Guid UserGameSessionId { get; set; }

    [ForeignKey("GameSession")]
    public Guid GameSessionId { get; set; }
    
    [ForeignKey("GameInfo")]
    public Guid GameInfoId { get; set; }
    public GameInfo GameInfo { get; set; }
    public GameSession GameSession { get; set; }
    public GameStatistic GameStatistic { get; set; }

    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public User User { get; set; }
  }
}