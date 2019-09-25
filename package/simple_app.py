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

import threading
import time

import broom
from package.broom import Player, Game

connection_string = "root:password@localhost/test"
engine = create_engine(f'mysql://{connection_string}')

app = Flask(__name__)
CORS(app, resources={r"/makedeck": {"origins": "http://localhost:8080"}})

app.config['CORS_HEADERS'] = 'Content-Type'

metadata = MetaData()

Base = declarative_base()
Base.metadata = metadata

def test():
    return 'test'

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

p1 = broom.Player('player_1')
p2 = broom.Player('player_2')
deck = broom.Deck(broom.make_deck())
g = broom.Game(p1,p2,deck)

@app.route("/tablenames")
def tablenames():

    table_names = engine.table_names()

    return jsonify(table_names)

@app.route("/makedeck", methods=["GET", "POST"])
@cross_origin(origin='localhost',headers=['Content- Type','Authorization'])
def makedeck():

    deck = broom.Deck(broom.CardStore)

    game_items = {
        "cards": g.deck.cards(),
        "order": g.deck.order()
    }

    response = jsonify(game_items)
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

        return response
    else:
        return response

@app.route("/dealhand", methods=["GET", "POST"])
@cross_origin(origin='localhost',headers=['Content- Type','Authorization'])
def dealhand():


    items = {
    }

    response = jsonify(items)
    # response.headers.add('Access-Control-Allow-Origin', '*')

    if request.method == "POST":

        context = request.get_json(force=True)

        return response
    else:
        return response

@app.route("/playround", methods=["GET", "POST"])
@cross_origin(origin='localhost',headers=['Content- Type','Authorization'])
def playround():


    items = {
    }

    response = jsonify(items)
    # response.headers.add('Access-Control-Allow-Origin', '*')

    if request.method == "POST":

        context = request.get_json(force=True)
        
        g.play_round(p1, p2)
        round_results = f"Player 1 score: {p1.score}\n\tPlayer 2 score: {p2.score}"
        print(round_results)

        return response
    else:
        return response


if __name__ == "__main__":
    app.run(host='127.0.0.1', port='5000', debug=True)