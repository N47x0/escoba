from flask import Flask, render_template, redirect, Markup, url_for, jsonify, request
from flask_sqlalchemy import SQLAlchemy
from sqlalchemy.ext.automap import automap_base
from sqlalchemy.orm import Session
from sqlalchemy import create_engine, func
from sqlalchemy import *
from sqlalchemy import orm
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

metadata = MetaData()

Base = declarative_base()
Base.metadata = metadata

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

db = create_engine('mysql://root:password@localhost/test',echo=False)
metadata.reflect(bind=db)

log_table = metadata.tables['log']

sm = orm.sessionmaker(bind=db, autoflush=True, autocommit=True, expire_on_commit=True)
session = orm.scoped_session(sm)

@app.route("/tablenames")
def tablenames():

    table_names = engine.table_names()
    return jsonify(table_names)

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
        if context['isDeck'] == True:
            log_item = LogItem(LogTime=dt.utcnow(), LogType='Deck', LogBlob=context)
            session.add(log_item)
            print("#### checking update ####")
            for item in session.query(LogItem).filter(LogItem.LogId==last_id):
                print(f"Last Added LogId: {item.LogId}")

        return response
    else:
        return response

if __name__ == "__main__":
    app.run(host='127.0.0.1', port='5000', debug=True)