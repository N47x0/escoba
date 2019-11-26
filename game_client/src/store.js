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

const dedupe = (objectArray) => {
  var deepCopy = JSON.parse(JSON.stringify(objectArray))
  console.log(deepCopy)
  deepCopy = deepCopy.map(play => play
    .reduce((accumulator, play) => {
      accumulator.push(play.id)
      return accumulator
    },
      []
    ))
      .filter((v, i, a) => a.indexOf(v) === i)
  console.log(deepCopy)
  return deepCopy
}

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
    baseUrl: process.env.NODE_ENV === 'development' ? 'https://localhost:5001' : 'TODO prod URL',
    currentPlayer: null
  },
  getters: {
    getBaseUrl: function (state) {
      return state.baseUrl
    },
    getClientSessionId: function (state) {
      return state.clientSessionId
    },
    __getCards: function (state) {
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
      console.log(state.validPlays)
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
    },
    validateSelection: (state) => (selection) => {
      var plays = JSON.parse(JSON.stringify(state.validPlays[state.currentPlayer.name]))
      var playsIds = dedupe(plays)
      // deep copy because not making copy of object
      // var plays = [ ...state.validPlays[state.currentPlayer.name]]
      // plays = plays.map(x => x.sort())
      var sel = [ ...selection]
      // sel = sel.sort()
      var match = false
      // perhaps only compare on id.  index being returned doesn
      var index = 0
      var keys = Object.keys(plays[0][0])
      var equalLengthPlays = plays.filter(play => play.length === sel.length)
      var selIds = sel.map(s => s.id)
      console.log(selIds)
      var equalLengthPlaysIds = dedupe(equalLengthPlays)
      
      // if (equalLengthPlays.length > 0) {
      //   while (match !== true) {
      //     equalLengthPlays.forEach((play, ip) => {
      //       play.forEach((card, ic) => {
      //         keys.forEach((key, ik) => {
      //           if (play[ic][key] === sel[ic][key]) {
      //             // console.log(play[ic][key])
      //             console.log(ip)
      //             // console.log(ic)
      //             // console.log(key)
      //             match = true
      //           }
      //         })  
      //       })
      //     })  
      //   }
      // }
      var selString = selIds.sort().join('')
      console.log(selString)

      if (equalLengthPlays.length > 0) {
        while (match !== true) {
          equalLengthPlaysIds.forEach((play, ip) => {
            var playString = play.sort().join('')
            if (playString === selString) {
              console.log(playString)
              console.log(ip)
              // console.log(ic)
              // console.log(key)
              match = true
            }
          })  
        }
      }
      // if (match) {
      //   console.log(index)
      // }
      return match
    },
    getCurrentPlayer(state) {
      return state.currentPlayer
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
      state.gameData = payload
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
    },
    [types.CHANGE_CURRENT_PLAYER]: function (state, payload) {
      state.currentPlayer = payload
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
          console.log(response)
          const parsedResponse = {
            gameState: response.data.gameState,
            player1: response.data.gameState.players.filter(player => player.name === 'Player 1')[0],
            player2: response.data.gameState.players.filter(player => player.name === 'Player 2')[0],
            sessionId: response.data.sessionId,
            tableCards: response.data.gameState.tableCards,
            validPlays: response.data.gameState.validPlays,
            validPlaysDeduped: {
              'Player 1': dedupe(response.data.gameState.validPlays['Player 1']),
              'Player 2': dedupe(response.data.gameState.validPlays['Player 2'])
            },
            currentPlayer: response.data.gameState.currentPlayer
          }
          console.log(parsedResponse)
          commit(types.INIT_GAME_DATA, parsedResponse.gameState)
          commit(types.SET_CLIENT_SESSION_ID, parsedResponse.sessionId)
          commit(types.CHANGE_PLAYER_1_DATA, parsedResponse.player1)
          commit(types.CHANGE_PLAYER_2_DATA, parsedResponse.player2)
          commit(types.CHANGE_TABLE_CARD_DATA, parsedResponse.tableCards)
          commit(types.CHANGE_VALID_PLAYS, parsedResponse.validPlays)
          commit(types.CHANGE_CURRENT_PLAYER, parsedResponse.currentPlayer)
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
    loadNextTurn: function ({ commit, getters }, payload) {
      // not sure if i want to change the loaded state and force rerender of everything
      // commit(types.CHANGE_RULE_DATA_LOADED, false)
      console.log(payload)
      // payload = JSON.stringify(payload)
      var url = getters.getBaseUrl + endpoints.PLAY_NEXT_TURN
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
          const parsedResponse = {
            gameState: response.data.gameState,
            player1: response.data.gameState.players.filter(player => player.name === 'Player 1')[0],
            player2: response.data.gameState.players.filter(player => player.name === 'Player 2')[0],
            tableCards: response.data.gameState.tableCards,
            validPlays: response.data.gameState.validPlays,
            validPlaysDeduped: {
              'Player 1': dedupe(response.data.gameState.validPlays['Player 1']),
              'Player 2': dedupe(response.data.gameState.validPlays['Player 2'])
            },
            currentPlayer: response.data.gameState.currentPlayer
          }
          console.log(parsedResponse)
          commit(types.CHANGE_GAME_DATA, parsedResponse.gameState)
          commit(types.CHANGE_PLAYER_1_DATA, parsedResponse.player1)
          commit(types.CHANGE_PLAYER_2_DATA, parsedResponse.player2)
          commit(types.CHANGE_TABLE_CARD_DATA, parsedResponse.tableCards)
          commit(types.CHANGE_VALID_PLAYS, parsedResponse.validPlays)
          commit(types.CHANGE_CURRENT_PLAYER, parsedResponse.currentPlayer)
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
