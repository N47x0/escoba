using System;
using System.Collections.Generic;
using System.Linq;
using GameManager;

namespace broom
{

  class Program
  {
    static void Main(string[] args)
    {
      AutoGame();
    }
    static void AutoGame () {
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
