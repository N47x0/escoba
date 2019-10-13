import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import {
  SET_CLIENT_SESSION_ID,
  CHANGE_GAME_DATA_ERRORED,
  CHANGE_GAME_DATA_LOADED,
  INIT_GAME_DATA,
  CHANGE_GAME_DATA,
  CHANGE_VALID_PLAYS_ERRORED,
  CHANGE_VALID_PLAYS_LOADED, 
  CHANGE_VALID_PLAYS,
  CHANGE_PLAYER_1_DATA,
  CHANGE_PLAYER_2_DATA,
  CHANGE_TABLE_CARD_DATA
} from './mutation-types'

import {
  MAKE_DECK,
  INIT_GAME,
  PLAY_FIRST_ROUND,
  GET_VALID_PLAYS,
  GET_BEST_PLAYS
} from './api-endpoints'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    gameDataLoaded: false,
    gameDataErrored: false,
    gameData: {},
    clientSessionId: null,
    player1: {},
    player2: {},
    tableCards: [],
    validPlaysLoaded: false,
    validPlaysErrored: false,
    validPlays: [],
    // baseUrl: process.env.NODE_ENV === 'development' ? 'http://127.0.0.1:5000' : 'TODO prod URL'
    baseUrl: process.env.NODE_ENV === 'development' ? 'https://localhost:5001' : 'TODO prod URL'
  },
  getters: {
    getBaseUrl: function (state) {
      return state.baseUrl
    },
    getClientSessionId: function (state) {
      return state.clientSessionId
    },
    getCards: function (state) {
      var cards = []
      // console.log(Object.entries(state.gameData.cards))
      Object.entries(state.gameData.game.deck.card_store).forEach((v, i, a) => {
        cards.push(v[0])
      })
      return cards
    },
    getDeck: function (state, getters) {
      var deck = []
      // console.log(state.gameData)
      // console.log(Object.entries(state.gameData.cards))
      Object.entries(state.gameData.deck.cards).forEach((value, index, array) => {
        deck.push({ 'suit': value[1].suit, 'value': value[1].val, 'owner': value[1].owner, 'card': value[1].id })
      })
      // console.log(deck)
      // console.log(getters.getDeckOrder)
      deck.sort(function (a, b) {
        return getters.getDeckOrder.indexOf(a.card) - getters.getDeckOrder.indexOf(b.card)
      })
      // console.log(deck)
      return deck
    },
    getPlayer1: (state) => {
      return state.player1
    },
    getPlayer2: (state) => {
      return state.player2
    },
    getGameDataLoaded: (state) => {
      return state.gameDataLoaded
    },
    getDeckOrder: (state) => {
      return state.gameData.deck.deck_order
    },
    getValidPlaysLoaded: (state) => {
      return state.validPlaysLoaded
    },
    getValidPlays: (state) => {
      return state.validPlays
    },
    getTableCards: (state) => {
      // console.log(state.gameData)
      return state.tableCards
    }
  },
  mutations: {
    [SET_CLIENT_SESSION_ID]: function (state, payload) {
      state.clientSessionId = payload
    },
    [CHANGE_GAME_DATA_ERRORED]: function (state, errored) {
      state.gameDataErrored = errored
    },
    [CHANGE_GAME_DATA_LOADED]: function (state, loaded) {
      state.gameDataLoaded = loaded
    },
    [INIT_GAME_DATA]: function (state, data) {
      state.gameData = data
    },
    [CHANGE_GAME_DATA]: function (state, payload) {
      // console.log(payload)
      state.gameData.game = payload.game
      // console.log(state.gameData)
    },
    [CHANGE_VALID_PLAYS_ERRORED]: function (state, errored) {
      state.validPlaysErrored = errored
    },
    [CHANGE_VALID_PLAYS_LOADED]: function (state, loaded) {
      state.validPlaysLoaded = loaded
    },
    [CHANGE_VALID_PLAYS]: function (state, data) {
      state.validPlays = data
    },
    [CHANGE_PLAYER_1_DATA]: function (state, payload) {
      // console.log(state.player1)
      state.player1 = payload
      // console.log(state.player1)
    },
    [CHANGE_PLAYER_2_DATA]: function (state, payload) {
      // console.log(state.player2)
      state.player2 = payload
      // console.log(state.player2)
    },
    [CHANGE_TABLE_CARD_DATA]: function (state, payload) {
      // console.log(state.player2)
      state.tableCards = payload
      // console.log(state.player2)
    }
    
  },
  actions: {
    updateGameData: function ({ commit }, payload) {
      console.log(payload)
      commit(CHANGE_GAME_DATA, payload)
    },
    loadGameData: function ({ commit, state, getters }) {
      var url = getters.getBaseUrl + INIT_GAME
      var config = {
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*'
          }
      }
      // var url = getters.getBaseUrl + MAKE_DECK
      console.log(config)
      axios.get(url, config)
        .then(function (response) {
          console.log(response)
          commit(INIT_GAME_DATA, response.data)
          //commit(SET_CLIENT_SESSION_ID, response.data.id)
          commit(CHANGE_PLAYER_1_DATA, response.data.player1)
          commit(CHANGE_PLAYER_2_DATA, response.data.player2)
          commit(CHANGE_TABLE_CARD_DATA, response.data.tableCards)
        })
        .catch(function (error) {
          console.log(error)
          commit(CHANGE_GAME_DATA_ERRORED, true)
        })
        .finally(function () {
          console.log('#### deck finally ####')
          commit(CHANGE_GAME_DATA_LOADED, true)
        })
    },
    loadValidPlays: function ({ commit, getters }) {
      var payload = {
        clientSessionId: getters.getClientSessionId
      }
      console.log(payload)
      var url = getters.getBaseUrl + GET_VALID_PLAYS
      var config = {
        headers: {
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*'
          }
      }
      axios
        .post(url, payload , config)
        .then(function (response) {
          console.log(response);
        })
        .catch(function (error) {
          console.log(error);
        });
    },
    getBestPlay: function ({ commit, getters}) {
      var payload = {
        clientSessionId: getters.getClientSessionId
      }
      console.log(payload)
      var url = getters.getBaseUrl + GET_BEST_PLAYS
      var config = {
        headers: {
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*'
          }
      }
      console.log(payload)
      axios
        .post(url, payload , config)
        .then(function (response) {
          console.log(response);
        })
        .catch(function (error) {
          console.log(error);
        });
    }
  }
})
