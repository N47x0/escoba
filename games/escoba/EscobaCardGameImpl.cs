using System.Collections.Generic;
using System.Linq;

namespace games.escoba
{
  public class EscobaCardGameImpl : games.ICardGame
  {
    private GameState CurrentState;
    private Player player1 {get;}
    private Player player2 {get;}

    public EscobaCardGameImpl(CardDeck deck, List<Player> players ) {
      if (players.Count != 2) {
        throw new InvalidGameParametersException(
          $"The game of Escoba Requires exactly two players, while {players.Count} were provided");
      }

      player1 = players.First();
      player2 = players.Last();

      CurrentState = new GameState {
        Deck = deck,
        Players = players,
        TurnCount = 0,
        IsDone = false,
        CurrentPlayer = player1,
        TableCards = new List<Card>()
      };
    }

    public GameState InitGame() {
      var deck = CurrentState.Deck;
      deck.Reset(); // shuffle

      // Start, alternate 3 cards each player, 
      foreach (var i in Enumerable.Range(1,3)) {
        player1.hand.AddRange(deck.Deal());
        player2.hand.AddRange(deck.Deal());
      }
      // Deal 4 to the table
      deck.GetTableCards().AddRange(deck.Deal(4));
      
      // Advance state
      CurrentState.TurnCount++;
      CurrentState.CurrentPlayer = player1;
      
      return CurrentState;
    }

    // This is where "escoba' specific (read: "business") logic belongs
    // After InitGame (deck starts with 40 cards):
    // - there are 30 cards in the deck (3 to each player and 4 to the table).
    // - Players take turns, discarding exactly 1 card.
    // - Odd turns are played by Player1, Even by Player2.
    // - Cards are dealt simultaneously in groups of three to each player when either runs out.
    // - When no cards are left to be played(after 36 turns) the game is over and score should be tallied.
    // - Any cards remaining on the table are awarded to the last player to score.
    public GameState PlayTurn(List<Card> cardsPlayed, Player player) {

      // possibly unnecessary check for player/turn out of sync
      if ((CurrentState.TurnCount % 2 == 0 && CurrentState.CurrentPlayer == player1)
       || (CurrentState.TurnCount % 2 != 0 && CurrentState.CurrentPlayer == player2) ) {
        throw new System.Exception("The turn and player dont match");
      }

      if (CurrentState.TurnCount > 36) {
        throw new System.Exception("Too many turns");
      }
      // possibly unnecessary check for validity of the play.
      if (!ValidPlays(CurrentState.CurrentPlayer.hand, CurrentState.Deck.GetTableCards()).Any(x => x.SequenceEqual(cardsPlayed))) {
        throw new System.Exception($"The cardsPlayed: {cardsPlayed} are not currently a valid move");
      }
      
      Player last_scored = null;
      if (ApplyPlay(cardsPlayed, player, CurrentState.Deck)) {
        last_scored = player;
      }

      if ( CurrentState.Deck.deck_order.Count > 0 ) {
        if ( (player1.hand.Count + player2.hand.Count) == 0 ) {
          foreach (var i in Enumerable.Range(1,3)) {
            player1.hand.AddRange(CurrentState.Deck.Deal());
            player2.hand.AddRange(CurrentState.Deck.Deal());
          }
        }
      } else {
        foreach (var c in CurrentState.Deck.GetTableCards()) {
          c.owner = last_scored.name;
        }
      }

      // Advance State
      CurrentState.CurrentPlayer = CurrentState.Players.Where(p => p.name != CurrentState.CurrentPlayer.name).Single();
      CurrentState.TurnCount++;
      return CurrentState;
      
    }
    public GameState EndGame() {
      TallyScore();
      return CurrentState;
    }
    public GameState GetCurrentState() {
      return CurrentState;
    }
    static IEnumerable<IEnumerable<T>> CombinationsOfK<T>(T[] data, int k) {
      int size = data.Length;
      IEnumerable<IEnumerable<T>> Runner(IEnumerable<T> list, int n)
      {
        int skip = 1;
        foreach (var headList in list.Take(size - k + 1).Select(h => new T[] { h }))
        {
          if (n == 1)
            yield return headList;
          else
          {
            foreach (var tailList in Runner(list.Skip(skip), n - 1))
            {
              yield return headList.Concat(tailList);
            }
            skip++;
          }
        }
      }

      return Runner(data, k);
    }
    static List<List<Card>> ValidPlays( List<Card> hand, List<Card> table_cards ) {
      List<List<Card>> acc = new List<List<Card>> {};
      // At least one play per card in player hand
      foreach (Card c in hand) {
        if (table_cards.Count > 0) {
          // add player card to all possible table card combos
          for (int i = 1; i <= table_cards.Count; i++) {
            var combos = CombinationsOfK<Card>((table_cards).ToArray(), i);
            List<List<Card>> hand_combos = new List<List<Card>>{};
            foreach (var combo in combos) {
              List<Card> pivot = new List<Card> {c};
              pivot.AddRange(combo);
              hand_combos.Add(pivot);
            }
            acc.AddRange(hand_combos);
          }
        } else {
          // just play hand card
          acc.Add(new List<Card> {c});
        } 
      }
      return acc;
    }
    
    static bool ApplyPlay( List<Card> play, Player player, CardDeck deck ) {
      bool scored = false;
      // bool isEscoba = false;
      // todo verify play 
      
      // Determine if cards stay on table
      scored = play.Sum( card => card.val ) == 15;
      foreach (var c in play) {
        if (scored) {
          c.owner = player.name;
          if (player.hand.Contains(c)) { 
            player.hand.Remove(c); 
          } else {
            deck.GetTableCards().Remove(c);
          }
        } else {
          if (player.hand.Contains(c)) {
            deck.GetTableCards().Add(c);
            player.hand.Remove(c);
          }
        }
      }

      if (deck.GetTableCards().Count == 0) {
        //isEscoba = true;
        System.Console.WriteLine($"{player.name} ESCOBA!!");
        player.AwardPoint();
      }
      
      return scored;
    }
    public void TallyScore() {
      // Oros
      int pl1_oros = 0;
      int pl2_oros = 0;
      // Sietes
      int pl1_sietes = 0;
      int pl2_sietes = 0;
      // Cartas
      int pl1_cartas = 0;
      int pl2_cartas = 0;

      foreach (var c in CurrentState.Deck.cards) {
        if ( c.Value.owner == player1.name) {
          if (c.Value.suit == "O") {
            pl1_oros++;
          }
          if (c.Value.val == 7) {
            pl1_sietes++;
          }
          if (c.Value.id == "O7") {
            player1.AwardPoint();
            System.Console.WriteLine($"O7 {player1.name}");
          }
          pl1_cartas++;
        }
        if (c.Value.owner == player2.name) {
          if (c.Value.suit == "O") {
            pl2_oros++;
          }
          if (c.Value.val == 7) {
            pl2_sietes++;
          }
          if (c.Value.id == "O7") {
            player2.AwardPoint();
            System.Console.WriteLine($"O7 {player2.name}");
          }
          pl2_cartas++;
        }
      }

      if (pl1_oros > pl2_oros) player1.AwardPoint();
      if (pl2_oros > pl1_oros) player2.AwardPoint();
      if (pl1_sietes > pl2_sietes) player1.AwardPoint();
      if (pl2_sietes > pl1_sietes) player2.AwardPoint();
      if (pl1_cartas > pl2_cartas) player1.AwardPoint();
      if (pl2_cartas > pl1_cartas) player2.AwardPoint();
      System.Console.WriteLine($"PL1 Oros {pl1_oros}\tPL1 Sietes {pl1_sietes}\tPL1 Cartas {pl1_cartas}\nPL2 Oros {pl2_oros}\tPL2 Sietes {pl2_sietes}\tPL2 Cartas {pl2_cartas}");
    }

  }
  
}
