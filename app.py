from flask import Flask, render_template, redirect, Markup, url_for, jsonify, request
from flask_cors import CORS, cross_origin
import json
import re
from datetime import datetime as dt
from pprint import pprint
from package.broom import Player, Deck, Game, make_deck

LOCAL_HOST = '127.0.0.1'
LOCAL_HOST_PORT = '5000'
NPM_LOCALHOST = "http://localhost:8080"
NPM_LOCALHOST_MOBILE = "http://192.168.1.106:8080"
ORIGIN = 'localhost'
HEADERS = ['Content- Type','Authorization']

def make_url(args, base=NPM_LOCALHOST):
    return base

def make_localhost(args, localhost=LOCAL_HOST):
    return localhost

def make_port(args, port=LOCAL_HOST_PORT):
    return port

def make_origin(args, origin=ORIGIN):
    return origin

def make_headers(args, headers=HEADERS):
    return headers

app = Flask(__name__)
CORS(app, resources={r"/makedeck": {"origins": make_url}})
# CORS(app, resources={r"/makedeck": {"origins": NPM_LOCALHOST_MOBILE}})

app.config['CORS_HEADERS'] = 'Content-Type'

client_sessions = {}

client_ids = [x for x in client_sessions.items()]

class ClientSession:
    
    id_count = 0

    def __init__(self, player1, player2, deck, game):
        ClientSession.id_count += 1
        self.id = ClientSession.id_count
        self.p1 = player1
        self.p2 = player2
        self.deck = deck
        self.g = game

@app.route("/makedeck", methods=["GET", "POST"])
@cross_origin(origin=make_origin,headers=make_headers)
def makedeck():

    # TODO new random seed deck

    # response.headers.add('Access-Control-Allow-Origin', '*')

    if request.method == "POST":
        context = request.get_json(force=True)
        csId = context['clientSessionId']
        cs = client_sessions[csId]
        response = jsonify({
            "game_state": cs.g.returnJSON(),
            "id": cs.id,
            'post_response': True
        })

        return response
    else:
        p1 = Player('player_1', 0)
        p2 = Player('player_2', 0)
        deck = Deck(make_deck())
        g = Game(p1,p2,deck)

        cs = ClientSession(p1, p2, deck, g)
        client_sessions[cs.id] = cs
        client_ids = [k for k, v in client_sessions.items()]
        response = jsonify({
            "game_state": g.returnJSON(),
            "id": cs.id
        })
        print(f"response_cs_id: {cs.id}")
        print(f"client_ids: {client_ids}")

        return response

@app.route("/playfirstround", methods=["GET", "POST"])
@cross_origin(origin=make_origin,headers=make_headers)
def playfirstround():

    items = {
    }
    response = jsonify(items)
    # response.headers.add('Access-Control-Allow-Origin', '*')

    if request.method == "POST":

        context = request.get_json(force=True)
        csId = context['clientSessionId']
        cs = client_sessions[csId]
        game = cs.g.play_first_round(cs.p1, cs.p2).returnJSON()
        #game = game.returnJSON()
        return jsonify({
            "game_state": game,
            "id": cs.id
        })

        #response = g.play_first_round(p1, p2).returnJSON()
        # round_results = f"Player 1 score: {p1.score}\n\tPlayer 2 score: {p2.score}"

    else:
        return response

@app.route("/validplays", methods=["GET", "POST"])
@cross_origin(origin=make_origin,headers=make_headers)
def validplays():
    
    if request.method == "POST":

        context = request.get_json(force=True)
        csId = context['clientSessionId']
        cs = client_sessions[csId]

        first_player = list(cs.g.valid_plays(cs.p1, cs.g.table_cards))
        second_player = list(cs.g.valid_plays(cs.p1, cs.g.table_cards))

        return jsonify({
            "first_player": first_player,
            "second_player": second_player,
            "id": cs.id,
            'post_response': True
        })

    else:
        return 

@app.route("/getbestplay", methods=["GET", "POST"])
@cross_origin(origin=make_origin,headers=make_headers)
def getbestplay():

    if request.method == "POST":

        context = request.get_json(force=True)
        csId = context['clientSessionId']
        cs = client_sessions[csId]

        playable1 = cs.g.valid_plays(cs.p1, cs.g.table_cards)
        playable2 = cs.g.valid_plays(cs.p2, cs.g.table_cards)
        first_player = cs.p1.get_play(playable1)
        second_player = cs.p2.get_play(playable2)
        return jsonify({
            "first_player": first_player,
            "second_player": second_player,
            "id": cs.id,
            'post_response': True
        })

    else:
        return 

print(f"client_ids: {client_ids}")

if __name__ == "__main__":
    app.run(host=make_localhost(LOCAL_HOST), port=make_port(LOCAL_HOST_PORT), debug=True)