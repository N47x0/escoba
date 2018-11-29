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

def get_combinations(cards: list, pivot: str):
  combos = set()
  combos.update(set([pivot]))
  for i in range(len(cards)-1):
    r = len(cards) - i
    for combo in  combinations(cards,r):
      if pivot in combo:
        combos.add(combo)
  return combos

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
  def deal(self, n=1, owner=''):
    d = self.deck_order[:n]
    self.deck_order = self.deck_order[n:]
    return set(d)
    # def update_store(card,store, owner):
    #   store.owner = owner
    #   return store[card]
    # return [update_store(delt,self.card_store, owner) for delt in d]
  def cards(self):
    return self.card_store
  def order(self):
    return self.deck_order

class Player: 
  score = 0
  hand = set()
  name = ''
  def __init__(self, name, hand=[]):
    self.name = name
  def play_turn(self,deck):
    return NotImplemented
  def new_hand(self, cards):
    self.hand = cards
  def award_point(self, points=1):
    self.score += points
  def get_play(self, playable: list, table_cards=[]):
    play = set()
    play.add(random.choice(list(playable)))
    for p in playable:
      empty = self.name
      if (isinstance(p,str)):
        card_values = [ CardStore[p].face ]
      else:
        card_values = [CardStore[c].face for c in p ]
      s = sum(card_values)
      if s == 15:
        play.add(p)
    
    #for card in play:
      #if card in self.hand:
        #self.hand.discard(card)
    return play 

class Game:
  deck = Deck()
  pl1 = Player('p1')
  pl2 = Player('p2')
  table_cards = set()
  def __init__(self, pl1, pl2, deck=Deck()):
    self.pl1 = pl1
    self.pl2 = pl2
    self.deck = deck
    print("Start game")
    #return NotImplemented

  def set_card_owner(self, card, owner):
    c = self.deck.cards()[card]
    self.deck.cards()[card] = Card(suit=c.suit, face=c.face, owner=owner)

  def reset(self):
    return NotImplemented
    
  def deal_hand(self):
    p1 = set()
    p2 = set()
    # deal out to p1 and p2 alternating 3 each
    for count in range(0,3):
      [ p1.add(d) for d in self.deck.deal()]
      [ p2.add(d) for d in self.deck.deal()]
    return p1,p2

  def deal_start(self):
    p1,p2 = self.deal_hand()
    start_table_cards = self.deck.deal(4)
    return p1,p2,start_table_cards

  def valid_plays(self, player, table_cards: set):
    # visible_cards = [card for card_id, card in self.deck.cards.items() if (card.owner == 'table') ]
    plays = set()
    for card in player.hand:
      combo_cards = set(table_cards)
      combo_cards.add(card)
      for combo in get_combinations(combo_cards,card):
        plays.add(combo)
      plays.add(card)
    return plays

  def apply_play(self,play, player):
    # validate(play)
    playable = self.valid_plays(player, self.table_cards)
    scored = False
    t_cards = self.table_cards
    p_cards = play.pop()
    if p_cards in playable:
      if isinstance(p_cards,str):
        card_values = [self.deck.card_store[p_cards].face]
      else:
        card_values = [self.deck.card_store[c].face for c in p_cards ]
      # assign card owners
      s = reduce(lambda a,b:a+b, card_values, 0)
      if ( s == 15):
        scored = True
        for card in p_cards:
          self.set_card_owner(card, player.name)
          if card in self.table_cards:
            self.table_cards.discard(card)
      else:
        if isinstance(p_cards, str):
          self.table_cards.update({p_cards})
        else:
          self.table_cards.update(p_cards)
    
    for card in p_cards:
      if card in player.hand:
        player.hand.discard(card)
    if not self.table_cards:
      player.award_point() #escoba
    return scored

  def apply_score(self):
    p1_total = set()
    p1_oros = set()
    p1_sevens = set()

    p2_total = set()
    p2_sevens = set()
    p2_oros = set()

    for card_id,card in self.deck.cards().items():
      p1_total.add(card_id) if card.owner == self.pl1.name else p2_total.add(card_id)
      p1_oros.add(card_id) if card.suit == 'O' else p2_oros.add(card_id)
      p1_sevens.add(card_id) if card.face == 7 else p2_sevens.add(card_id)
      if card_id == '7O':
        self.pl1.award_point() if card.owner == self.pl1.name else self.pl2.award_point()
      
    if len(p1_total) > len(p2_total):
      self.pl1.award_point()
    elif len(p2_total) > len(p1_total):
      self.pl2.award_point()
    
    if len(p1_oros) > len(p2_oros):
      self.pl1.award_point()
    elif len(p2_oros) > len(p1_oros):
      self.pl2.award_point()
    
    if len(p1_sevens) > len(p2_sevens):
      self.pl1.award_point()
    elif len(p2_sevens) > len(p1_sevens):
      self.pl2.award_point()

  def play_game(self):
    p1_cards, p2_cards ,table_cards = self.deal_start()
    self.pl1.new_hand(p1_cards)
    self.pl2.new_hand(p2_cards)
    self.table_cards = table_cards
    
    last_scored = ''
    cards_left = len(self.deck.order())
    while len(self.deck.order()) > 0:
      if len(self.pl1.hand) == 0 and len(self.pl2.hand) == 0:
        p1_cards, p2_cards = self.deal_hand()
        self.pl1.new_hand(p1_cards)
        self.pl2.new_hand(p2_cards)
        cards_left = len(self.deck.order())


      # hand per player
      while (len(self.pl1.hand) + len(self.pl2.hand) > 0):
        if (len(self.pl1.hand)):
          playable = self.valid_plays(self.pl1,self.table_cards)
          play = self.pl1.get_play(playable)
          if self.apply_play(play,self.pl1): last_scored = self.pl1.name

        if (len(self.pl2.hand)):
          playable = self.valid_plays(self.pl2,self.table_cards)
          play = self.pl2.get_play(playable)
          if self.apply_play(play,self.pl2): last_scored = self.pl2.name

    # award last_player_to_score remaining cards
    [self.set_card_owner(card_id, last_scored) for card_id, card in self.deck.cards().items() if card.owner == '']
    self.apply_score()

    

if __name__ == "__main__":
  
  p1 = Player('player_1')
  p2 = Player('player_2')
  deck = Deck(make_deck())
  g = Game(p1,p2,deck)
  g.play_game()
  print("Player 1 score: {}\nPlayer 2 score: {}".format( p1.score, p2.score))