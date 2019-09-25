from flask import Flask, render_template, redirect, Markup, url_for, jsonify, request
from flask_sqlalchemy import SQLAlchemy
from sqlalchemy.ext.automap import automap_base
from sqlalchemy.orm import Session
from sqlalchemy import create_engine, func
from sqlalchemy import *
from sqlalchemy import orm, Column, Integer, LargeBinary, TIMESTAMP, DateTime
from sqlalchemy.engine import reflection
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import relationship, mapper
from flask_cors import CORS, cross_origin

import random
from collections import namedtuple
from typing import List
from itertools import combinations
from functools import reduce

from bson import json_util
import json
import re
from datetime import datetime as dt


connection_string = "root:password@localhost/test"
engine = create_engine(f'mysql://{connection_string}')

app = Flask(__name__)
CORS(app, resources={r"/makedeck": {"origins": "http://localhost:8080"}})

app.config['CORS_HEADERS'] = 'Content-Type'


# app.config["SQLALCHEMY_DATABASE_URI"] = "mysql://root:password@localhost:3306/test"
# db = SQLAlchemy(app)

# Base = automap_base()
# reflect the tables
# Base.prepare(engine, reflect=True)

# Create our session (link) from Python to the DB
# session = Session(engine)
metadata = MetaData()
# metadata.reflect(engine)

# Base = automap_base(metadata=metadata)
# Base.prepare()




Base = declarative_base()
Base.metadata = metadata

# product = Table('product', metadata,
#     Column('id', Integer, primary_key=True),
#     Column('name', String(1024), nullable=False, unique=True),

# )

class LogItem(Base):
    __tablename__ = 'log'
    LogId = Column(Integer, primary_key=True)
    LogTime = Column(TIMESTAMP, default=dt.utcnow())
    LogType = Column(String(32))
    LogBlob = Column(String)
    # def __init__(self):
    #     self.LogTime = dt.utcnow()
    #     self.LogType = self.LogType
    def __repr__(self):
        return f"<LogItem(LogTime={self.LogTime}, LogType={self.LogType}, LogBlob={self.LogBlob})>"

# class LogItem(object):
#     def __init__(self, logid, logtime, logtype, logblob):
#         self.logid = logid
#         self.logtime = logtime
#         self.logtype = logtype
#         self.logblob = logblob
#         logid = logid
#     def __repr__(self):
#         return f"{self.__class__.logid}({self.logid},{self.logtime},{self.logtype},{self.logblob})"


db = create_engine('mysql://root:password@localhost/test',echo=False)
metadata.reflect(bind=db)

log_table = metadata.tables['log']

# mapper(LogItem, log_table)

# deck_table = metadata.tables['deck']
# cards_table = metadata.tables['cards']

sm = orm.sessionmaker(bind=db, autoflush=True, autocommit=True, expire_on_commit=True)
session = orm.scoped_session(sm)

# q = session.query(deck_table,cards_table).join(cards_table)
# for r in q.limit(10):
#     print(r)\

# q = session.query(deck_table,cards_table).join(deck_table)
# for r in q.limit(10):
#     print(r)

# class Card(Base):
#     __tablename__ = "Card"
#     deck = relationship("Deck", backref = "DeckId")


# class Deck(Base):
#     __tablename__ = "Deck"

# insp = reflection.Inspector.from_engine(db)
# print(insp.get_table_names())
# print(insp.get_foreign_keys(Cards.__tablename__))
# print(insp.get_foreign_keys(Deck.__tablename__))


# Deck = Base.classes.Deck
# print(Base.classes)


# class User(db.Model):
#     id = db.Column(db.Integer, primary_key=True)
#     username = db.Column(db.String, unique=True, nullable=False)
#     email = db.Column(db.String, unique=True, nullable=False)

@app.route("/tablenames")
def tablenames():

    table_names = engine.table_names()

    # db.session.add(User(name="Flask", email="example@example.com"))
    # db.session.commit()

    # users = User.query.all()

    return jsonify(table_names)

@app.route("/deck")
def deck():

    deck = session.query(Deck).all()
    # columns = session.query(Deck.__table__.columns).all()
    # col_arr = [col for col in columns]
    columns = Deck.__table__.columns.keys()
    cards = []
    for card in deck:
        passenger_dict = {}
        # passenger_dict["name"] = passenger.name
        # passenger_dict["age"] = passenger.age
        # passenger_dict["sex"] = passenger.sex
        cards.append(card)
        print(card)

    return jsonify(columns)
    # return jsonify(all_passengers)

@app.route("/makedeck", methods=["GET", "POST"])
@cross_origin(origin='localhost',headers=['Content- Type','Authorization'])
def makedeck():

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

    deck = Deck(CardStore)

    cards = deck.cards()

    order = deck.order()

    items = {
        "cards": cards,
        "order": order
    }

    response = jsonify(items)
    # response.headers.add('Access-Control-Allow-Origin', '*')
    

    if request.method == "POST":
        context = request.get_json(force=True)
        id_query = session.query(LogItem.LogId).all()
        ids = []
        for logid in id_query:
            ids.append(logid)
        last_id = ids[-1][0]
        print(int(last_id))
        if context['isDeck'] == True:
            log_item = LogItem(LogTime=dt.utcnow(), LogType='Deck', LogBlob=context)
            print(context['deck'][0])
            print(log_item)
            print(dt.utcnow())
            session.add(log_item)
            print("#### checking update ####")
            for item in session.query(LogItem).filter(LogItem.LogId==last_id):
                print(item)

        # if session.query(LogItem).filter(LogItem.LogId==1).count()==0:
            

        #     # new_log = LogItem("1","Product1")
        #     print("Creating new product:")
        #     # session.add(new_prod)
        #     session.flush()
        # else:
        #     print(f"product with id 1 already exists: {session.query(LogItem).filter(LogItem.logid==1).one()}")

        # print "loading Product with id=1"
        # prod = session.query(Product).filter(Product.id==1).one()
        # print "current name: %s" % prod.name
        # prod.name = "new name"

        # print prod


        # prod.name = 'test'

        # session.add(prod)
        # session.flush()

        # print prod

        return response
    else:
        return response

@app.route("/dealhand", methods=["GET", "POST"])
@cross_origin(origin='localhost',headers=['Content- Type','Authorization'])
def dealhand():

  def deal_hand(deck):
    p1 = set()
    p2 = set()
    # deal out to p1 and p2 alternating 3 each
    for count in range(0,3):
      [ p1.add(d) for d in deck.deal()]
      [ p2.add(d) for d in deck.deal()]
    return p1,p2

  def deal_start(self):
    p1,p2 = self.deal_hand()
    start_table_cards = self.deck.deal(4)
    return p1,p2,start_table_cards

    items = {
    }

    response = jsonify(items)
    # response.headers.add('Access-Control-Allow-Origin', '*')

    if request.method == "POST":
        context = request.get_json(force=True)
        id_query = session.query(LogItem.LogId).all()
        # ids = []
        # for logid in id_query:
        #     ids.append(logid)
        # last_id = ids[-1][0]
        # if context['isDeck'] == True:
        #     log_item = LogItem(LogTime=dt.utcnow(), LogType='Deck', LogBlob=context)
        #     # session.add(log_item)
        #     print("#### checking update ####")
        #     for item in session.query(LogItem).filter(LogItem.LogId==last_id):
        #         print(f"Last Added LogId: {item.LogId}")

        return response
    else:
        return response


if __name__ == "__main__":
    app.run(host='127.0.0.1', port='5000', debug=True)