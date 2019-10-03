from flask import Flask, render_template, redirect, Markup, url_for, jsonify, request
from flask_cors import CORS, cross_origin
import json
import re
from datetime import datetime as dt
from pprint import pprint
from package.broom import Player, Deck, Game, make_deck, returnJSON

p1 = Player('player_1', 0)
p2 = Player('player_2', 0)
deck = Deck(make_deck())
g = Game(p1,p2,deck)

app = Flask(__name__)
CORS(app, resources={r"/makedeck": {"origins": "http://localhost:8080"}})

app.config['CORS_HEADERS'] = 'Content-Type'

@app.route("/makedeck", methods=["GET", "POST"])
@cross_origin(origin='localhost',headers=['Content- Type','Authorization'])
def makedeck():

    # TODO new random seed deck
    p1 = Player('player_1', 0)
    p2 = Player('player_2', 0)
    deck = Deck(make_deck())
    g = Game(p1,p2,deck)

    response = jsonify(returnJSON(g))
    # response.headers.add('Access-Control-Allow-Origin', '*')

    if request.method == "POST":
        context = request.get_json(force=True)
        return response
    else:
        return response

@app.route("/playfirstround", methods=["GET", "POST"])
@cross_origin(origin='localhost',headers=['Content- Type','Authorization'])
def playfirstround():


    items = {
    }

    response = jsonify(items)
    # response.headers.add('Access-Control-Allow-Origin', '*')

    if request.method == "POST":

        context = request.get_json(force=True)
        response = returnJSON(g.play_first_round(p1, p2))
        # round_results = f"Player 1 score: {p1.score}\n\tPlayer 2 score: {p2.score}"
        print(response)

        return jsonify(response)
    else:
        return response

@app.route("/getbestplay", methods=["GET", "POST"])
@cross_origin(origin='localhost',headers=['Content- Type','Authorization'])
def getbestplay():


    items = {

    }

    response = jsonify(items)
    # response.headers.add('Access-Control-Allow-Origin', '*')

    if request.method == "POST":

        context = request.get_json(force=True)
        player1 = context['player1']
        player2 = context['player2']
        best_plays = {
        "player_1": returnJSON(g.get_best_play(Player.from_json(player1))),
        "player_2": returnJSON(g.get_best_play(Player.from_json(player2))),
        }
        print(context)

        return jsonify(best_plays)
    else:
        return response

@app.route("/validplays", methods=["GET", "POST"])
@cross_origin(origin='localhost',headers=['Content- Type','Authorization'])
def validplays():


    deck = []
    
    
    if request.method == "POST":

      context = request.get_json(force=True)
      table_cards = context['tableCards']
      player1 = context['player1']
      player2 = context['player2']
      g = Game(player1,player2,deck)
      valid_plays = {
        "player_1": returnJSON(g.valid_plays_list(Player.from_json(player1), table_cards)),
        "player_2": returnJSON(g.valid_plays_list(Player.from_json(player2), table_cards))
      }
      # valid_plays = {
      #   "player_1": "sample data",
      #   "player_2": "sample data 2",
      # }
      # print(g.table_cards)
      response = jsonify(valid_plays)
      print("#### valid plays ####")
      pprint(valid_plays)
      #response.headers.add('Access-Control-Allow-Origin', '*')
      print("#### deck ####")
      print(deck)
      return response
    else:
      return response

if __name__ == "__main__":
    app.run(host='127.0.0.1', port='5000', debug=True)