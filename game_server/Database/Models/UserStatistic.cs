using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations.Schema;

namespace game_server.Database.Models
{
  public class UserStatistic {
    public Guid UserStatisticId {get;set;}

    [ForeignKey("Users")]
    public Guid UserId {get; set;}
    public User User {get;set;}

    [ForeignKey("GameInfo")]
    public Guid GameInfoId {get;set;}
    public GameInfo GameInfo {get;set;}
    public int NumberOfPlays {get;set;}
    public int Wins {get;set;}
    public int Losses {get;set;}
    public int Draws {get;set;}
    public ICollection<GameStatistic> GameStatistics { get; set; } = new List<GameStatistic>();
  }
}