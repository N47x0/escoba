using System.Linq;
using System.Collections.Generic;
namespace games {
  
  // Generic card stucture
  // TODO - seed with a "deck type" ? Naipes/French-Deck/UNO-cards
  public class Card {
    public string suit {get; set;}
    public uint val {get; set;}
    public string owner {get; set;} 
    public string id {get; set;} 
    public Card (string suit, uint val, string owner = "") {
      this.suit = suit;
      this.val = val;
      this.owner = owner;
      this.id = suit + val.ToString();
    }
  }

  // Generic 'card' player (not a generic player tho :-} )
  // just keeps track of player-state (score, current hand)
  // action policy is separate
  public class Player {
    public uint score {get; set;} = 0;
    public List<Card> hand {get; set;} = new List<Card> {};
    public string name {get; set;} = "unnamed";
    public Player(string name) {
      this.name = name;
    }
    
    static System.Random rng = new System.Random();

    public List<Card> GetPlay(List<List<Card>> playable_hands) {
      List<List<Card>> good_hands = (from hand in playable_hands
                                    where hand.Sum( card => card.val ) == 15
                                    select hand).ToList();
      int r = rng.Next(playable_hands.Count);
      int rg = rng.Next(good_hands.Count);
      return good_hands.Count > 0 ? good_hands[rg] : playable_hands[r];
    }

    public uint AwardPoint(uint points=1) {
      this.score += points;
      return score;
    }
  }

  // Generic stucture for manipulating "Cards" as definied above
  // TODO - seed with a "deck type" ? Naipes/French-Deck/UNO-cards
  public class CardDeck {
    public Dictionary<string, Card> cards {get; set; } = new Dictionary<string, Card> {};
    public List<string> deck_order {get; private set;} = new List<string> {};
    
    // TODO - This could be 'seeded' with a 'deck specialization'
    string [] suits = new string[] {"B", "O", "E", "C"};
    static System.Random rng = new System.Random();

    // TODO - This still has some 'non-generic' behavior declared
    public CardDeck() {
      foreach (uint value in Enumerable.Range(1,10)) {
        foreach (var suit in suits) {
          Card c = new Card(suit, value);
          cards[c.id] = c;
          deck_order.Add(c.id);
        }
      }
      Reset();
    }
    public void Reset() {
      //shuffle deck_order
      int n = deck_order.Count;
      while (n > 0) {
        n--;
        int k = rng.Next(n+1);
        var val = deck_order[k];
        deck_order[k] = deck_order[n];
        deck_order[n] = val;
      }
    }

    public List<Card> Deal(int num=1, Player player=null) {
      List<Card> dealt = new List<Card> {};
      foreach (var c in deck_order.Take(num)) {
        dealt.Add(cards[c]);
        deck_order.Remove(c);
      }
      player?.hand.AddRange(dealt);
      return dealt;
    }

    public void SetCardOwner(Card card, Player player) {
      card.owner = player.name;
    }

    // Decide where "table" definition belongs, more broadly: ownership could be better defined than "strings"
    // Current implementation will break if Player.name == 'table'
    public void AddCardToTable(Card card) { card.owner = "table" ;}
    public List<Card> GetTableCards() {
      return cards.Where( card => card.Value.owner == "table" ).Select(card => card.Value ).ToList();
    }

  }

  // PODO to hold generic game-state data
  // Notice that Players, TableCards, Deck are "read-only"
  public class GameState {
    public List<Player> Players { get; set;}
    public List<Card> TableCards { 
      get { return Deck.GetTableCards(); } 
      set {}
    }
    public CardDeck Deck { get; set;}
    public Player CurrentPlayer {get; set;}
    public int TurnCount {get; set; }

    // This has been commented as "not generic enough"
    //public int current  { get; set; }
    public bool IsDone { get; set;}

    // public GameState( CardDeck deck, List<Player> players, int turnCount, bool isDone ) {
    //   Deck = deck;
    //   Players = players;
    //   TurnCount = turnCount;
    //   IsDone = isDone;
    // }
  }

  // Generic Card Game interface
  public interface ICardGame {
    GameState InitGame();
    GameState PlayTurn(List<Card> cardsPlayed, Player player, GameState currentState);
  }

  public class InvalidGameParametersException : System.Exception {
    public InvalidGameParametersException(){}
    public InvalidGameParametersException(string message) : base(message) {}
    public InvalidGameParametersException(string message, System.Exception innerException): base(message, innerException) {}
  }
}