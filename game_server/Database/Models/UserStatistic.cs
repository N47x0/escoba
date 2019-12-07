using System;
using System.Collections.Generic; 

namespace game_server.Database.Models
{
  public class UserStatistic {
    public Guid UserStatisticId {get;set;}
    public Guid UserId {get; set;}
    public User User {get;set;}
    public Guid GameInfoId {get;set;}
    public GameInfo GameInfo {get;set;}
    public int NumberOfPlays {get;set;}
    public int Wins {get;set;}
    public int Losses {get;set;}
    public int Draws {get;set;}
  }
}