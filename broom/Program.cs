using System;
using System.Collections.Generic;
using System.Linq;

namespace broom
{
  struct Card {
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
    uint score {get; set;}
    List<Card> hand {get; set;}
    public string name {get; }
    public Player(string name) {
      this.name = name;
    }
    public List<Card> GetPlay(List<Card> playable) {
      return new List<Card> {};
    }

    public uint AwardPoint(uint points=1) {
      this.score += points;
      return score;
    }

  }
  class Deck {
    List<Card> cards {get; } = new List<Card> {};
    List<string> deck_order {get; set;}
    string [] suits = new string[] {"B", "O", "E", "C"};
    public static Random rng = new Random();

    public Deck() {
      foreach (uint value in Enumerable.Range(1,10)) {
        foreach (var suit in suits) {
          Card c = new Card(suit, value);
          cards.Add(c);
          deck_order.Add(c.id);
        }

      }
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

    public List<Card> Deal(uint num, string owner) {
      return new List<Card> {};
    }

    public void SetCardOwner(Card card, Player player) {
      card.owner = player.name;
    }

  }

  class Game {
    Deck deck = new Deck {};
    Player pl1 = new Player("p1");
    Player pl2 = new Player("p2");
    List<Card> table_cards = new List<Card> {};

    public void Reset() {
      // reset/clear scorep
      // deal start
    }

    public List<Card> ValidPlays( Player player ) {
      return new List<Card> {};
    }

    public void ApplyPlay( List<Card> play ) {

    }
    public void TallyScore() {

    }

    public void PlayRound() {

    }

  }

  class Program
  {

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
     

    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
      int[] data = Enumerable.Range(1, 10).ToArray();
      int k = 3;
      foreach (string comb in CombinationsOfK(data, k).Select(c => string.Join(" ", c)))
      {
        Console.WriteLine(comb);
      }
      
    }
  }
}
