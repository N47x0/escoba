using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic; 

namespace game_server.Database.Models
{
  public class GameStatistic {
    public Guid GameStatisticId { get; set; }

    [ForeignKey("GameInfo")]
    public Guid GameInfoId { get; set; }

    [ForeignKey("GameSessions")]
    public Guid GameSessionId { get; set; }

    [ForeignKey("UserStatistics")]
    public Guid UserStatisticId { get; set; }

    [ForeignKey("Users")]
    public Guid UserId { get; set; }
    public User User{ get; set; }
    public UserStatistic UserStatistic { get; set; }
    public GameInfo GameInfo { get; set; }
    public GameSession GameSession { get; set; }
    public ICollection<UserGameSession> UserGameSessions { get; set; } = new List<UserGameSession>();
    public string FinalScore { get; set; }
    public bool? HumanWin { get; set; }
    public bool? AiWin  { get; set; }
    public bool? Draw { get; set; }
    public bool GameComplete { get; set; }
    public DateTime GameStart { get; set;}
    public DateTime? GameEnd { get; set;}
  }
}