using System;
using System.Collections.Generic;
using System.Linq;

namespace broom
{
  class Card {
    public string suit {get;}
    public uint val {get;}
    public string owner {get; set;} 
    public string id {get;} 
    public Card (string suit, uint val, string owner = "") {
      this.suit = suit;
      this.val = val;
      this.owner = owner;
      this.id = suit + val.ToString();
    }
  }
  class Player {
    public uint score {get; set;} = 0;
    public List<Card> hand {get; set;} = new List<Card> {};
    public string name {get; } = "unnamed";
    public Player(string name) {
      this.name = name;
    }

    static Random rng = new Random();

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
  class Deck {
    public Dictionary<string, Card> cards {get; } = new Dictionary<string, Card> {};
    public List<string> deck_order {get; private set;} = new List<string> {};
    string [] suits = new string[] {"B", "O", "E", "C"};
    static Random rng = new Random();

    public Deck() {
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

  }

  class Game {
    Deck deck = new Deck {};
    public Player pl1 {get;} = new Player("p1");
    public Player pl2 {get;} = new Player("p2");
    List<Card> table_cards = new List<Card> {};

    public void Reset() {
      // reset/clear scorep
      // deal start
    }
    public static IEnumerable<IEnumerable<T>> CombinationsOfK<T>(T[] data, int k)
    {
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

    public List<List<Card>> ValidPlays( Player player ) {
      List<List<Card>> acc = new List<List<Card>> {};
      // At least one play per card in player hand
      foreach (Card c in player.hand) {
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

    public bool ApplyPlay( List<Card> play, Player player ) {
      bool scored = false;
      // todo verify play 
      
      // Determine if cards stay on table
      scored = play.Sum( card => card.val ) == 15;
      foreach (var c in play) {
        if (scored) {
          c.owner = player.name;
          if (player.hand.Contains(c)) { 
            player.hand.Remove(c); 
          } else {
            table_cards.Remove(c);
          }
        } else {
          if (player.hand.Contains(c)) {
            table_cards.Add(c);
            player.hand.Remove(c);
          }
        }
      }

      if (table_cards.Count == 0) {
        Console.WriteLine($"{player.name} ESCOBA!!");
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

      foreach (var c in deck.cards) {
        if ( c.Value.owner == pl1.name) {
          if (c.Value.suit == "O") {
            pl1_oros++;
          }
          if (c.Value.val == 7) {
            pl1_sietes++;
          }
          if (c.Value.id == "O7") {
            pl1.AwardPoint();
            Console.WriteLine($"O7 {pl1.name}");
          }
          pl1_cartas++;
        }
        if (c.Value.owner == pl2.name) {
          if (c.Value.suit == "O") {
            pl2_oros++;
          }
          if (c.Value.val == 7) {
            pl2_sietes++;
          }
          if (c.Value.id == "O7") {
            pl2.AwardPoint();
            Console.WriteLine($"O7 {pl2.name}");
          }
          pl2_cartas++;
        }
      }

      if (pl1_oros > pl2_oros) pl1.AwardPoint();
      if (pl2_oros > pl1_oros) pl2.AwardPoint();
      if (pl1_sietes > pl2_sietes) pl1.AwardPoint();
      if (pl2_sietes > pl1_sietes) pl2.AwardPoint();
      if (pl1_cartas > pl2_cartas) pl1.AwardPoint();
      if (pl2_cartas > pl1_cartas) pl2.AwardPoint();
      Console.WriteLine($"PL1 Oros {pl1_oros}\tPL1 Sietes {pl1_sietes}\tPL1 Cartas {pl1_cartas}\nPL2 Oros {pl2_oros}\tPL2 Sietes {pl2_sietes}\tPL2 Cartas {pl2_cartas}");
      
    }

    public void PlayRound(Player adv_player, Player flw_player) {
      deck = new Deck();
      // Start, alternate 3 cards each player, 
      foreach (var i in Enumerable.Range(1,3)) {
        adv_player.hand.AddRange(deck.Deal());
        flw_player.hand.AddRange(deck.Deal());
      }
      table_cards.AddRange(deck.Deal(4));
      Player last_scored = null;
      while (deck.deck_order.Count > 0) {
        if (adv_player.hand.Count == 0 && flw_player.hand.Count == 0) {
          foreach (var i in Enumerable.Range(1,3)) {
            adv_player.hand.AddRange(deck.Deal());
            flw_player.hand.AddRange(deck.Deal());
          }
        }

        while ((adv_player.hand.Count + flw_player.hand.Count) > 0) {
          if (adv_player.hand.Count > 0) {
            if (ApplyPlay(adv_player.GetPlay(ValidPlays(adv_player)), adv_player)) {
              last_scored = adv_player;
            }
          }

          if (flw_player.hand.Count > 0) {
            if (ApplyPlay(flw_player.GetPlay(ValidPlays(flw_player)), flw_player)) {
              last_scored = flw_player;
            }
          }
        }
      }

      foreach (var c in table_cards) {
        c.owner = last_scored.name;
      }
      TallyScore();
    }
  }

  class Program
  {

    static void Main(string[] args)
    {
      Game g = new Game();
      Console.WriteLine("Starting a game of ESCOBA!");

      int rounds = 0;
      while (g.pl1.score < 15 && g.pl2.score < 15) {
        rounds++;
        if (rounds % 2 == 1) {
          g.PlayRound(g.pl1, g.pl2);
        } else {
          g.PlayRound(g.pl2, g.pl1);
        }
        Console.WriteLine($"Round {rounds}\t PL1: {g.pl1.score}\tPL2: {g.pl2.score}");
      }
    }
  }
}
