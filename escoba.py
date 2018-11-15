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


def get_combinations(cards: list):
  combos = []
  for i in range(len(cards)-1):
    r = len(cards) - i
    combos += combinations(cards,r)
  return combos

def valid_plays(cards , player_id):
  player_cards = [ card for card_id, card in cards.items() if (card.owner == player_id)]
  visible_cards = [card for card_id, card in cards.items() if (card.owner == 'table') ]
  plays = []
  for card in player_cards:
    combo_cards = visible_cards
    combo_cards.append(card)
    for combo in get_combinations(combo_cards):
      s = reduce(lambda a,b:a+b.face, combo, 0)
      if ( s == 15):
        plays.append(combo)
  
  # remove duplicates from list using set
  return [list(c) for c in set(tuple(c) for c in plays)]

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
  def deal(self, owner, n=1):
    d = self.deck_order[:n]
    self.deck_order = self.deck_order[n:]
    def update_store(card,store, owner):
      store.owner = owner
      return store[card]
    return [update_store(delt,self.card_store, owner) for delt in d]
  def cards(self):
    return self.card_store
  def order(self):
    return self.deck_order



class Player: 
  score = 0
  hand = []
  name = ''
  def __init__(self, name, hand=[]):
    self.name = name
  def play_turn(self,deck):
    return NotImplemented
  def new_hand(self, cards):
    self.hand = cards
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

  def set_card_owner(self, card, owner):
    self.deck.cards()[card].owner = owner
  def new_hand(self):
    return Deck()
  def deal_hand(self, deck :list):
    p1 = []
    p2 = []
    # deal out to p1 and p2 alternating 3 each
    for count in range(0,6):
      p1 += self.deck.deal(self.pl1.name)
      p2 += self.deck.deal(self.pl2.name)
    return p1,p2

  def deal_start(self):
    p1,p2 = self.deal_hand(self.deck.order())
    start_table_cards = self.deck.deal(4)
    for card in start_table_cards:
      self.set_card_owner(card.name, 'table')
    for card in p1:
      self.set_card_owner(card.name, self.pl1.name)
    for card in p2:
      self.set_card_owner(card.name,self.pl2.name)

    return p1,p2,start_table_cards

  def valid_plays(self, player):
    visible_cards = [card for card_id, card in self.deck.cards.items() if (card.owner == 'table') ]
    plays = []
    for card in player.hand:
      combo_cards = visible_cards
      combo_cards.append(card)
      plays.append(get_combinations(combo_cards))
    return plays

  def play_card(self):
    return NotImplemented
  def apply_play(self,play, player):
    # validate(play)
    playable = self.valid_plays(player)
    if (play in playable):
      # assign card owners
      
      s = reduce(lambda a,b:a+b.face, play, 0)
      if ( s == 15):   
        for card in play:
          self.set_card_owner(card, player.name)
      else:
        for card in play:
          self.set_card_owner(card,'table')
      

    # move deck
    # award escoba
    return 
  def play_game(self):
    p1_cards, p2_cards ,table_cards = self.deal_start()
    self.pl1.new_hand(p1_cards)
    self.pl2.new_hand(p2_cards)
    # main loop per player
    
    while (len(self.pl1.hand) and len(self.pl2.hand)):
      if len(self.pl1.hand) == 0 and len(self.pl2.hand == 0):
        self.deal_hand()
      playable = self.valid_plays(self.pl1)
      play = self.pl1.get_play(playable, table_cards)
      self.apply_play(play,self.pl1)

      playable = self.valid_plays(self.pl2)
      play = self.pl2.get_play(playable, table_cards)
      self.apply_play(play, self.pl2)

    if len(self.deck.order()) > 0:
      self.pl1, self.pl2 = self.deal_hand(self.deck.order())

    

if __name__ == "__main__":
  d = Deck(make_deck())
  d.shuffle()
  print("shuffle deck {}".format(d.deck_order))
  dl = d.deal(2)
  print("deal {}".format(dl.deck_order))
  print("left with {}".format(d.deck_order))

  def make_test_card(n,s):
    o = 'none'
    if n <= 7:
      o = 'table'
    if n >= 8:
      o = 'p1'
    return Card(s,n,owner=o)

  test_table_sort = { str(n)+s: make_test_card(n,s) for n in faces for s in suits }

  print("Pre sorted deck{}".format(test_table_sort))

  test_combo = { str(n)+s:make_test_card(n,s) for s in suits[2:] for n in faces[6:9] }
  print("The combo: {}".format(get_combinations(test_combo)))

  plays = valid_plays(test_combo, 'p1')
  print("The plays: {}".format(plays))