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
    public ICollection<UserStats> Stats {get; set; }
  }
  public class UserStats {
    public Guid UserStatsId {get;set;}
    public Guid UserId {get; set;}
    public User User {get;set;}
    public Guid SelectedGameInfoId {get;set;}
    public GameInfo SelectedGameInfo {get;set;}
    public int NumberOfPlays {get;set;}
    public int Wins {get;set;}
    public int Losses {get;set;}
    public int Draws {get;set;}
  }
  public class GameSession
  {
    public Guid GameSessionId { get; set;}
    public Guid SelectedGameInfoId {get; set;}
    public GameInfo SelectedGameInfo {get; set;}
    public string GameSessionState {get;set;} // i.e. running, paused, done
    public ICollection<UserGameSession> UserPlayers { get; set;} // this should be some new "User" type, not games.CardGame.Player   
    public ICollection<games.GameState> GameStates {get;set;} // this will store json-string serializations

  }
  public class UserGameSession 
  {
    public Guid GameSessionId {get; set;}
    public GameSession GameSession {get;set;}
    public Guid UserId {get; set;}
    public User User {get; set;}
  }
  public class GameInfo {
    public Guid GameInfoId {get; set;}
    public string GameName {get;set;}
    public string Rules {get; set;}
  }

  public class InitGamePayload {
    public Guid SessionId {get; set;}
    public games.GameState GameState {get; set;}
  }

  public class ValidPlaysPayload {
    public Guid SessionId {get; set;}
    public games.GameState GameState {get; set;}
    public List<List<games.Card>> ValidPlays { get; set; }
  }

  public class ValidPlaysIncomingPayload {
    public Guid SessionId {get; set;}
    public List<games.Card> Hand {get; set;}
    public List<games.Card> TableCards {get; set;}
  }

}