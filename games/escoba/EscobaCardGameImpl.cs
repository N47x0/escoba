using System.Collections.Generic;
using System.Linq;

namespace games.escoba
{
  public class EscobaCardGameImpl : games.ICardGame
  {
    public EscobaCardGameImpl() {
      
    }

    public GameState InitGame() {
      var deck = new games.CardDeck();
      deck.Reset();
      var state = new GameState {
        Deck = deck,
        IsDone = false,
        Players = new List<Player> {
          new Player ("Player 1"),
          new Player ("Player 2"),
        },
      };

      var player1 = state.Players.First();
      var player2 = state.Players.Last();

      // Start, alternate 3 cards each player, 
      foreach (var i in Enumerable.Range(1,3)) {
        var player1_card = deck.Deal();
        var player2_card = deck.Deal();
        player1.hand.AddRange(player1_card);
        deck.SetCardOwner(player1_card.First(), player1);
        player2.hand.AddRange(player2_card);
        deck.SetCardOwner(player2_card.First(), player2);

      }
      // Deal 4 to the table
      deck.AddCardsToTable(deck.Deal(4));
      
      // Advance state
      state.TurnCount++;
      state.TableCards = deck.GetTableCards();
      state.CurrentPlayer = player1;
      state.ValidPlays = GetValidPlays(state.Players, state.TableCards);
      
      return state;
    }

    // public List<List<Card>> GetValidPlays( List<Card> hand, List<Card> table_cards ) {
    //   List<List<Card>> valid_plays = ValidPlays(hand, table_cards);
    //   return valid_plays;
    // }

    // public GameState _GetValidPlays (GameState currentState) {
    //   Player _p1 = currentState.Players.First();
    //   Player _p2 = currentState.Players.Last();
    //   List<Card> _table_cards = currentState.Deck.GetTableCards();
    //   List<List<Card>> _p1_valid_plays = ValidPlays(_p1.hand, _table_cards);
    //   List<List<Card>> _p2_valid_plays = ValidPlays(_p2.hand, _table_cards);
    //   currentState.ValidPlays = new Dictionary<string, List<List<Card>>>();
    //   currentState.ValidPlays.Add(_p1.name, _p1_valid_plays);
    //   currentState.ValidPlays.Add(_p2.name, _p2_valid_plays);
    //   return currentState;
    // }
    public Dictionary<string, List<List<Card>>> GetValidPlays (List<Player> players, List<Card> table_cards) {
      Player _p1 = players.First();
      Player _p2 = players.Last();
      List<Card> _table_cards = table_cards;
      List<List<Card>> _p1_valid_plays = ValidPlays(_p1.hand, _table_cards);
      List<List<Card>> _p2_valid_plays = ValidPlays(_p2.hand, _table_cards);
      // _p1_valid_plays.ForEach(play => {

      // })
      var _valid_plays = new Dictionary<string, List<List<Card>>>();
      _valid_plays.Add(_p1.name, _p1_valid_plays);
      _valid_plays.Add(_p2.name, _p2_valid_plays);
      return _valid_plays;
    }

    // This is where "escoba' specific (read: "business") logic belongs
    // After InitGame (deck starts with 40 cards):
    // - there are 30 cards in the deck (3 to each player and 4 to the table).
    // - Players take turns, discarding exactly 1 card.
    // - Odd turns are played by Player1, Even by Player2.
    // - Cards are dealt simultaneously in groups of three to each player when either runs out.
    // - When no cards are left to be played(after 36 turns) the game is over and score should be tallied.
    // - Any cards remaining on the table are awarded to the last player to score.
    public GameState PlayTurn(List<Card> cardsPlayed, Player player, GameState currentState) {

      var player1 = currentState.Players.First();
      var player2 = currentState.Players.Last();

      // possibly unnecessary check for player/turn out of sync
      if ((currentState.TurnCount % 2 == 0 && currentState.CurrentPlayer == player1)
       || (currentState.TurnCount % 2 != 0 && currentState.CurrentPlayer == player2) ) {
        throw new System.Exception("The turn and player dont match");
      }

      if (currentState.TurnCount > 36) {
        throw new System.Exception("Too many turns");
      }
      // possibly unnecessary check for validity of the play.
      // if (!ValidPlays(currentState.CurrentPlayer.hand, currentState.Deck.GetTableCards()).Any(x => x.SequenceEqual(cardsPlayed, new CardComparer()))) {
      if (!ValidPlays(currentState.CurrentPlayer.hand, currentState.Deck.GetTableCards()).Any(x => x.SequenceEqual(cardsPlayed))) {
        throw new System.Exception($"The cardsPlayed: {cardsPlayed} are not currently a valid move");
      }
      
      Player last_scored = null;
      if (ApplyPlay(cardsPlayed, player, currentState.Deck)) {
        last_scored = player;
      }

      if ( currentState.Deck.deck_order.Count > 0 ) {
        if ( (player1.hand.Count + player2.hand.Count) == 0 ) {
          foreach (var i in Enumerable.Range(1,3)) {
            player1.hand.AddRange(currentState.Deck.Deal());
            player2.hand.AddRange(currentState.Deck.Deal());
          }
        }
      } else {
        foreach (var c in currentState.Deck.GetTableCards()) {
          c.owner = last_scored.name;
        }
      }

      // Advance State
      currentState.CurrentPlayer = currentState.Players.Where(p => p.name != currentState.CurrentPlayer.name).Single();
      currentState.TurnCount++;
      if (currentState.TurnCount == 36) {
        TallyScore(currentState);
        currentState.IsDone = true;
      }
      // also advance valid plays state to reflect new hand
      currentState.ValidPlays = GetValidPlays(currentState.Players, currentState.TableCards);

      return currentState;
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
      return acc.Select(x => x).Distinct().ToList();
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
    static void TallyScore(GameState currentState) {
      // Oros
      int pl1_oros = 0;
      int pl2_oros = 0;
      // Sietes
      int pl1_sietes = 0;
      int pl2_sietes = 0;
      // Cartas
      int pl1_cartas = 0;
      int pl2_cartas = 0;

      var player1 = currentState.Players.First();
      var player2 = currentState.Players.Last();

      foreach (var c in currentState.Deck.cards) {
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
