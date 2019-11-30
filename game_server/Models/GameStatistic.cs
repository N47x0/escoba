using System;
using System.Collections.Generic; 

namespace game_server.Models
{
  public class GameStatistic {
    public Guid GameStatisticId {get;set;}
    public Guid GameInfoId {get; set;}
    public int TimesPlayed {get;set;}
    public int HumanWins {get;set;}
    public int AiWins {get;set;}
    public int HumanLosses {get;set;}
    public int AiLosses {get;set;}
    public int HumanDraws {get;set;}
    public int AiDraws {get;set;}
  }
}