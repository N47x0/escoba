using System.Collections.Generic; 

namespace game_server.Models
{
  public class User {
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    
    // The key of type string is to identify the game implementation. Defer for now as only escoba
    // This did not work with EF Core - Dictionaries are not supported
    // public Dictionary<string,UserStats> Stats { get; set; }
    public ICollection<UserGameSession> GameSessions { get; set; }
    public ICollection<UserStats> Stats {get; set; }
  }
  public class UserStats {
      public int UserStatsId {get;set;}
      public int UserId {get; set;}
      public User User {get;set;}
      public int SelectedGameInfoId {get;set;}
      public GameInfo SelectedGameInfo {get;set;}
      public int NumberOfPlays {get;set;}
      public int Wins {get;set;}
      public int Losses {get;set;}
      public int Draws {get;set;}
    }
  public class GameSession
  {
    public int GameSessionId { get; set;}
    public int SelectedGameInfoId {get; set;}
    public GameInfo SelectedGameInfo {get; set;}
    public string GameSessionState {get;set;} // i.e. running, paused, done
    public ICollection<UserGameSession> UserPlayers { get; set;} // this should be some new "User" type, not games.CardGame.Player   
    public ICollection<games.GameState> GameStates {get;set;} // this will store json-string serializations

  }
  public class UserGameSession 
  {
    public int GameSessionId {get; set;}
    public GameSession GameSession {get;set;}
    public int UserId {get; set;}
    public User User {get; set;}
  }
  public class GameInfo {
    public int GameInfoId {get; set;}
    public string GameName {get;set;}
    public string Rules {get; set;}
  }

  public class InitGamePayload {
    public string Id {get; set;}
    public games.GameState GameState {get; set;}
  }

}