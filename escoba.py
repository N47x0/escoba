#!/usr/bin/env 
import random
from collections import namedtuple
from typing import List
from itertools import combinations
from functools import reduce
Card = namedtuple("Card", ['suit', 'face', 'owner'])
faces = list(range(1,11))
suits = ['B', 'O', 'E', 'C']

def make_deck():
  return {  str(n) + s: Card(suit=s, face=n, owner='') for n in faces for s in suits }

CardStore = make_deck()

def make_deck_from(card_ids :list):
  return { card_id: CardStore[card_id] for card_id in card_ids }

def shuffle(deck :list):
  random.shuffle(deck)
  return deck

def deal(deck :list, n=1):
  return NotImplemented

def deal_start(deck :list):
  p1,p2, deck = deal_hand(deck)
  start_table_cards = deck[:4]
  deck = deck[4:]
  return p1,p2,start_table_cards, deck

def deal_hand(deck :list):
  count = 0
  p1 = []
  p2 = []
  # deal out to p1 and p2 alternating
  for card in deck[:6]:
    if count % 2 == 0:
      p1 += card
    else:
      p2 += card
    count += 1
  return p1,p2,deck

def get_combinations(cards: list):
  combos = []
  for i in range(len(cards)-1):
    r = len(cards) - i
    combos += combinations(cards,r)
  return combos

def valid_plays(cards , player_id):
  visible_cards = [card for card_id, card in cards.items() if (card.owner == 'table' or card.owner == player_id)]#.sort(key=lambda x:x.face)
  plays = []
  for combo in get_combinations(visible_cards):
    s = reduce(lambda a,b:a+b.face, combo, 0)
    if ( s == 15):
      plays.append(set(combo))
  
  return plays

def player_rewards(card_store):
  # point if more O cards
  # point if more cards
  # point if 3+ 7s
  # point if 7O card
  # point when escoba but not here
  return NotImplemented

class Deck:
  card_store = {}

  deck_order = []
  def __init__(self,card_store={}):
    self.card_store = card_store
    self.deck_order = list(card_store.keys())
    random.shuffle(self.deck_order)
    print("Hello deck {}".format(self.deck_order))
  def shuffle(self):
    return random.shuffle(self.deck_order)
  def deal(self, n=1):
    d = self.deck_order[:n]
    self.deck_order = self.deck_order[n:]
    return Deck(make_deck_from(d))
  def cards(self):
    return self.card_store
  def order(self):
    return self.deck_order



class Player: 
  score = 0
  hand = Deck()
  name = ''
  def __init__(self, name, deck=[]):
    self.name = name
  def play_turn(self,deck):
    return NotImplemented
  def play_card(self, deck):
    return NotImplemented
  def award_point(self, points):
    self.score += points

class Game:
  deck = Deck()
  pl1 = Player('p1')
  pl2 = Player('p2')
  def __init__(self, pl1, pl2, deck):
    self.pl1 = pl1
    self.pl2 = pl2
    self.deck = deck
    print("Start game")
    #return NotImplemented

  def new_hand(self):
    return Deck()
  def play_card(self):
    return NotImplemented
  def apply_play(self,play):
    # validate(play)
    # assign card owners
    # award escoba
    return NotImplemented
  def play_game(self):
    p1,p2,table_cards,self.deck = deal_start(self.deck)
    for card in table_cards:
      self.deck.cards()[card] = 'table'
    for card in p1:
      self.deck.cards()[card] = self.pl1.name
    for card in p2:
      self.deck.cards()[card] = self.pl2.name
    
    # main loop per player
    while (len(p1) and len(p2)):
      playable = valid_plays(self.deck.cards(), self.pl1.name)
      self.apply_play(self.pl1.get_play(playable))
      playable = valid_plays(self.deck.cards(), self.pl2.name)
      self.apply_play(self.pl2.get_play(playable))

      

    

if __name__ == "__main__":
  d = Deck(make_deck())
  d.shuffle()
  print("shuffle deck {}".format(d.deck_order))
  dl = d.deal(2)
  print("deal {}".format(dl.deck_order))
  print("left with {}".format(d.deck_order))

  def make_test_card(n,s):
    o = 'none'
    if n < 6:
      o = 'table'
    if n >= 8:
      o = 'p1'
    return Card(s,n,owner=o)

  test_table_sort = { str(n)+s: make_test_card(n,s) for n in faces for s in suits }

  print("Pre sorted deck{}".format(test_table_sort))

  test_combo = { str(n)+s:Card(s,n,owner='table') for s in suits[2:] for n in faces[6:8] }
  print("The combo: {}".format(get_combinations(test_combo)))

  valid_plays(test_combo, 'p1')