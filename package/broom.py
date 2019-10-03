#!/usr/bin/env 
import random
from collections import namedtuple
from typing import List
from itertools import combinations
from functools import reduce
import time



def returnJSON(game_object):
  deck = {
    "card_store": {},
    "deck_order": []
  }

  for k, v in game_object.deck.card_store.items():
    deck['card_store'][k] = v

  # deck['deck_order'] = list(game_object.deck.card_store.keys())
  deck['deck_order'] = game_object.deck.order()

  pl1 = {
    "score": '',
    "hand": [],
    "name": ''
  }

  pl1['score'] = game_object.pl1.score
  pl1['name'] = game_object.pl1.name
  pl1['hand'] = game_object.pl1.get_hand()

  pl2 = {
    "score": '',
    "hand": [],
    "name": ''
  }

  pl2['score'] = game_object.pl2.score
  pl2['name'] = game_object.pl2.name
  pl2['hand'] = game_object.pl2.get_hand()

  table_cards = []

  table_cards = game_object.get_table_cards()

  # if len(game_object.table_cards) > 0:
  #   for x in range(len(game_object.table_cards)):
  #     table_cards.append(x)

  return {
    "game": {
      "pl1": pl1,
      "pl2": pl2,
      "deck": deck,
      "table_cards": table_cards
    }
  }
  
def playsJSON(plays_set):
  return


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

def get_escombinations(cards: set, pivot: str):
  combos = set()
  #combos.add(frozenset(cards | set([pivot])))
  for i in range(len(cards)):
    r = len(cards) + 1 - i # combinatorial order (from the 5 choose 'x' where 'x' is order)
    combs = combinations(cards | set([pivot]),r)
    for combo in  combs:
      combo_vals = [CardStore[c].face for c in combo ]
      if ( pivot in combo  # pivot card is the player's card - has to be part of combo
      and sum(combo_vals) == 15 # only plays that add to 15 are considered, all other plays are equivalent to laying down card on table
      or r > len(cards) ):
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

class Play:
  def __init__(self, Card={}):
    self.card = Card
  def get_card(self):
    return self.card

class PlayList:
  def __init__(self, owner, plays=set()):
    self.owner = owner
    self.plays = plays

  def get_plays(self):
    return list(self.plays)
  def get_owner(self):
    return self.owner
  def add_play(self, Play):
    self.plays.add(Play)
    return self.plays

# print(CardStore)
# playlist = []
# pl1_pl = PlayList('Player1')
# for key, card in CardStore.items():
#   #playlist.append(Play(card))
#   pl1_pl.add_play(Play(card))



# #pl = PlayList(playlist).get_plays()
# pl = pl1_pl.get_plays()

# for card in pl:
#   print(card.card)
# #print(pl.plays.card)


class Player: 
  score = 0
  hand = set()
  name = ''
  def __init__(self, name, score, hand=[], actual_play=[], ):
    self.name = name
    # added score and hand to init so they can be passed in to deserializer
    self.score = score
    self.hand = hand if hand is not None else []
    self.playlist = PlayList(name)
  def play_turn(self,deck):
    return NotImplemented
  def new_hand(self, cards):
    self.hand = cards
  def award_point(self, points=1):
    self.score += points
  def get_play(self, playable: list, table_cards=[]):
    play = set()
    if len(playable) == 0: # never happens
      play.add(random.choice(list(self.hand)))  # no playable because table_cards were probably empty so play random card
    else:
      play = playable
    #for card in play:
      #if card in self.hand:
        #self.hand.discard(card)
    if len(play) > 1 :
      good_hands = [p for p in play if sum([CardStore[c].face for c in p]) == 15 ]
      if len(good_hands) > 0:
        return random.choice(good_hands)
      return random.choice(list(play))
    # return player object for frontend   
    self.actual_play = play.pop()  
    return self
    # original game logic to return just one card
    #return play.pop
  def get_hand(self):
    return list(self.hand)
  #deserialzer  
  @classmethod
  def from_json(cls, data):
      return cls(**data)


class Game:
  deck = Deck()
  pl1 = Player('p1', 0)
  pl2 = Player('p2', 0)
  table_cards = set()
  paused = True

  def __init__(self, pl1, pl2, deck=Deck(), valid_plays=[]):
    self.pl1 = pl1
    self.pl2 = pl2
    self.deck = deck
    self.valid_plays = valid_plays if valid_plays is not None else []

    print("Start game")
    #return NotImplemented

   # serialzer method
  def toJSON(self):
    # return json.loads(json.dumps(self, default=lambda o: o.__dict__ if type(o.__dict__) == '__dict__' else o.__set__, 
    #   sort_keys=True))
    return json.loads(json.dumps(self, default=lambda o: o.__dict__ , sort_keys=True))
    

  def set_pause_state(self, switch):
    print(f"#### switching paused to:{switch} ####")
    self.paused = switch

  def get_table_cards(self):
    return list(self.table_cards)

  def get_pause_state(self):
    return self.paused

  def set_card_owner(self, card, owner):
    c = self.deck.cards()[card]
    self.deck.cards()[card] = Card(suit=c.suit, face=c.face, owner=owner)

  def reset_deck(self):
    self.deck = Deck(make_deck())
    
  def deal_hand(self):
    p1 = set()
    p2 = set()
    print(f"cards_remaining_before_deal: {len(self.deck.order())}")
    # deal out to p1 and p2 alternating 3 each
    for count in range(0,3):
      [ p1.add(d) for d in self.deck.deal()]
      [ p2.add(d) for d in self.deck.deal()]
    print(f"cards_remaining_after_deal: {len(self.deck.order())}")
    return p1,p2

  def deal_start(self):
    p1,p2 = self.deal_hand()
    start_table_cards = self.deck.deal(4)
    return p1,p2,start_table_cards

  def valid_plays(self, player, table_cards: set):
    # visible_cards = [card for card_id, card in self.deck.cards.items() if (card.owner == 'table') ]
    plays = set()
    for card in player.hand:
      if (len(table_cards) > 0):
        combo_cards = set(table_cards)
        escombinations = get_escombinations(combo_cards,card)
        for combo in escombinations:
          plays.add(combo)
      else:
        plays.add(tuple(player.hand))
    self.valid_plays_list = list(plays)
    return self

  def apply_play(self,play, player):
    # validate(play)
    playable = self.valid_plays(player, self.table_cards)
    scored = False
    t_cards = self.table_cards
    #p_cards = play.pop()
    p_cards = play
    if p_cards in playable:
      if isinstance(p_cards,str):
        card_values = [self.deck.card_store[p_cards].face]
      else:
        card_values = [self.deck.card_store[c].face for c in p_cards ]
      # assign card owners
      s = sum(card_values)
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
      print(f"{player.name} Escoba!")
      self.print_score()
    return scored

  def apply_score(self):
    p1_total = set()
    p1_oros = set()
    p1_sevens = set()

    p2_total = set()
    p2_sevens = set()
    p2_oros = set()

    for card_id, card in self.deck.cards().items():
      if (card.owner == self.pl1.name):
        p1_total.add(card_id)
        if card.suit == 'O': p1_oros.add(card_id) 
        if card.face == 7: p1_sevens.add(card_id) 
      else:
        p2_total.add(card_id)
        if card.suit == 'O': p2_oros.add(card_id)
        if card.face == 7: p2_sevens.add(card_id)

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
    print(f'Points:\tPL1\tPL2\nOros:\t[{len(p1_oros)}]\t[{len(p2_oros)}]\nSevens:\t[{len(p1_sevens)}]\t[{len(p2_sevens)}]\nCards:\t[{len(p1_total)}]\t[{len(p2_total)}]')



  def play_first_round(self, first_player, second_player):
    p1_cards, p2_cards ,table_cards = self.deal_start()
    first_player.new_hand(p1_cards)
    second_player.new_hand(p2_cards)
    self.table_cards = table_cards
    return self

  def get_best_play(self, player):
    playable = self.valid_plays(player,self.table_cards)
    player.get_play(playable)
    return self 

  def play_round(self, first_player, second_player):
    
    last_scored = ''
    cards_left = len(self.deck.order())
    while len(self.deck.order()) > 0:
      if len(first_player.hand) == 0 and len(second_player.hand) == 0:
        p1_cards, p2_cards = self.deal_hand()
        first_player.new_hand(p1_cards)
        second_player.new_hand(p2_cards)
        cards_left = len(self.deck.order())

      def get_play(play):
        #print(play)
        return play

      # hand per player
      while (len(first_player.hand) + len(second_player.hand) > 0):
        if (len(first_player.hand)):
          playable = self.valid_plays(first_player,self.table_cards)
          play = first_player.get_play(playable)
          if self.apply_play(play,first_player): last_scored = first_player.name

        if (len(second_player.hand)):
          playable = self.valid_plays(second_player,self.table_cards)
          play = second_player.get_play(playable)
          if self.apply_play(play,second_player): last_scored = second_player.name
        while (self.paused):
          #print(first_player)
          get_play(play)
          
    # award last_player_to_score remaining cards
    [self.set_card_owner(card_id, last_scored) for card_id, card in self.deck.cards().items() if card.owner == '']
    self.apply_score()

  def print_score(self):
        print("Player 1 score: {}\nPlayer 2 score: {}".format(self.pl1.score, self.pl2.score))
    
def test():
    return 'test'
