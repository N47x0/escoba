import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import {
  types
} from './mutation-types'

import {
  endpoints
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
    rulesLoaded: false,
    rulesErrored: false,
    rulesPlays: [],
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
      // console.log(Object.entries(state.gameData.cards))
      Object.entries(state.gameData.deck.cards).forEach((value, index, array) => {
        deck.push({ 'suit': value[1].suit, 'value': value[1].val, 'owner': value[1].owner, 'card': value[1].id })
      })
      deck.sort(function (a, b) {
        return getters.getDeckOrder.indexOf(a.card) - getters.getDeckOrder.indexOf(b.card)
      })
      // console.log(deck)
      return deck
    },
    getPlayer1: (state) => {
      return state.player1[0]
    },
    getPlayer2: (state) => {
      return state.player2[0]
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
      return state.tableCards
    },
    getRules (state) {
      return state.rules
    },
    getRulesLoaded (state) {
      return state.rulesLoaded
    }
  },
  mutations: {
    [types.SET_CLIENT_SESSION_ID]: function (state, payload) {
      state.clientSessionId = payload
    },
    [types.CHANGE_GAME_DATA_ERRORED]: function (state, errored) {
      state.gameDataErrored = errored
    },
    [types.CHANGE_GAME_DATA_LOADED]: function (state, loaded) {
      state.gameDataLoaded = loaded
    },
    [types.INIT_GAME_DATA]: function (state, data) {
      state.gameData = data
    },
    [types.CHANGE_GAME_DATA]: function (state, payload) {
      // console.log(payload)
      state.gameData.game = payload.game
      // console.log(state.gameData)
    },
    [types.CHANGE_RULE_DATA_ERRORED]: function (state, errored) {
      state.rulesErrored = errored
    },
    [types.CHANGE_RULE_DATA_LOADED]: function (state, loaded) {
      state.rulesLoaded = loaded
    },
    [types.LOAD_RULE_DATA]: function (state, data) {
      state.rules = data
    },
    [types.CHANGE_VALID_PLAYS_ERRORED]: function (state, errored) {
      state.validPlaysErrored = errored
    },
    [types.CHANGE_VALID_PLAYS_LOADED]: function (state, loaded) {
      state.validPlaysLoaded = loaded
    },
    [types.CHANGE_VALID_PLAYS]: function (state, data) {
      state.validPlays = data
    },
    [types.CHANGE_PLAYER_1_DATA]: function (state, payload) {
      // console.log(state.player1)
      state.player1 = payload
      // console.log(state.player1)
    },
    [types.CHANGE_PLAYER_2_DATA]: function (state, payload) {
      // console.log(state.player2)
      state.player2 = payload
      // console.log(state.player2)
    },
    [types.CHANGE_TABLE_CARD_DATA]: function (state, payload) {
      // console.log(state.player2)
      state.tableCards = payload
      // console.log(state.player2)
    }

  },
  actions: {
    updateGameData: function ({ commit }, payload) {
      commit(types.CHANGE_GAME_DATA, payload)
    },
    loadRuleData: function ({ commit, state, getters }) {
      var url = getters.getBaseUrl + endpoints.RULES
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
          commit(types.LOAD_RULE_DATA, response.data)
        })
        .catch(function (error) {
          console.log(error)
          commit(types.CHANGE_RULE_DATA_ERRORED, true)
        })
        .finally(function () {
          console.log('#### deck finally ####')
          commit(types.CHANGE_RULE_DATA_LOADED, true)
        })
    },
    loadGameData: function ({ commit, state, getters, dispatch }) {
      var url = getters.getBaseUrl + endpoints.INIT_GAME + '/jdoe@acme.com/escoba'
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
          const parsedResponse = {
            gameState: response.data.gameState,
            player1: response.data.gameState.players.filter(player => player.name === 'Player 1'),
            player2: response.data.gameState.players.filter(player => player.name === 'Player 2'),
            sessionId: response.data.sessionId,
            tableCards: response.data.gameState.tableCards
          }
          commit(types.INIT_GAME_DATA, parsedResponse.gameState)
          commit(types.SET_CLIENT_SESSION_ID, parsedResponse.sessionId)
          commit(types.CHANGE_PLAYER_1_DATA, parsedResponse.player1)
          commit(types.CHANGE_PLAYER_2_DATA, parsedResponse.player2)
          commit(types.CHANGE_TABLE_CARD_DATA, parsedResponse.tableCards)
          dispatch('loadRuleData')
        })
        .catch(function (error) {
          console.log(error)
          commit(types.CHANGE_GAME_DATA_ERRORED, true)
        })
        .finally(function () {
          console.log('#### deck finally ####')
          commit(types.CHANGE_GAME_DATA_LOADED, true)
        })
    },
    loadValidPlays: function ({ commit, getters }, payload) {
      console.log(payload)
      var url = getters.getBaseUrl + endpoints.GET_VALID_PLAYS + getters.getClientSessionId
      var config = {
        headers: {
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*'
        }
      }
      axios
        .post(url, payload, config)
        .then(function (response) {
          console.log(response)
        })
        .catch(function (error) {
          console.log(error)
        })
    },
    getBestPlay: function ({ commit, getters }) {
      var payload = {
        clientSessionId: getters.getClientSessionId
      }
      var url = getters.getBaseUrl + endpoints.GET_BEST_PLAYS
      var config = {
        headers: {
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*'
        }
      }
      axios
        .post(url, payload, config)
        .then(function (response) {
          console.log(response)
        })
        .catch(function (error) {
          console.log(error)
        })
    },
    playRound: function ({ commit, getters }) {
      var payload = {
        clientSessionId: getters.getClientSessionId
      }
      var url = getters.getBaseUrl + endpoints.PLAY_ROUND
      var config = {
        headers: {
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*'
        }
      }
      axios
        .post(url, payload, config)
        .then(function (response) {
          console.log(response)
        })
        .catch(function (error) {
          console.log(error)
        })
    }
  }
})
