import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    gameDataLoaded: false,
    gameDataErrored: false,
    gameData: {},
    clientSessionId: null,
    player1: {},
    player2: {},
    validPlaysLoaded: false,
    validPlaysErrored: false,
    validPlays: []
  },
  getters: {
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
      Object.entries(state.gameData.game.deck.card_store).forEach((value, index, array) => {
        var [a, b, c] = value[1]
        var d = b.toString() + a
        deck.push({ 'suit': a, 'value': b, 'owner': c, 'card': d })
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
      return state.gameData.game.pl1
    },
    getPlayer2: (state) => {
      return state.player2
      return state.gameData.game.pl2
    },
    getGameDataLoaded: (state) => {
      return state.gameDataLoaded
    },
    getDeckOrder: (state) => {
      return state.gameData.game.deck.deck_order
    },
    getValidPlaysLoaded: (state) => {
      return state.validPlaysLoaded
    },
    getValidPlays: (state) => {
      return state.validPlays
    },
    getTableCards: (state) => {
      // console.log(state.gameData)
      return state.gameData.game.table_cards
    }
  },
  mutations: {
    setClientSessionId: function (state, payload) {
      state.clientSessionId = payload
    },
    changeGameDataErrored: function (state, errored) {
      state.gameDataErrored = errored
    },
    changeGameDataLoaded: function (state, loaded) {
      state.gameDataLoaded = loaded
    },
    initGameData: function (state, data) {
      state.gameData = data
    },
    changeGameData: function (state, payload) {
      // console.log(payload)
      state.gameData.game = payload.game
      // console.log(state.gameData)
    },
    changeValidPlaysErrored: function (state, errored) {
      state.validPlaysErrored = errored
    },
    changeValidPlaysLoaded: function (state, loaded) {
      state.validPlaysLoaded = loaded
    },
    changeValidPlays: function (state, data) {
      state.validPlays = data
    },
    changePlayer1Data: function (state, payload) {
      console.log(state.player1)
      state.player1 = payload
      console.log(state.player1)
    },
    changePlayer2Data: function (state, payload) {
      console.log(state.player2)
      state.player2 = payload
      console.log(state.player2)
    }
    
  },
  actions: {
    updateGameData: function ({ commit }, payload) {
      console.log(payload)
      commit('changeGameData', payload)
    },
    async fetchData({ rootState, commit }, payload) {
      try {
          let body = { language: rootState.authStore.currentLocale.locale };
          if (payload) {
              body = Object.assign({}, payload.body, body);
          }
          let response = await axios.post(
              `api.factory.com/${payload.url}`,
              body,
              rootState.config.serviceHeaders
          );
          if (payload.commit) {
              commit('mutate', {
                  property: payload.stateProperty,
                  with: response.data[payload.stateProperty]
              });
          }
          return response.data;
      } catch (error) {
          throw error;
      }
    },
    loadGameData: function ({ commit, state }) {
      axios.get('http://127.0.0.1:5000/makedeck')
        .then(function (response) {
          console.log(response)
          commit('initGameData', response.data.game_state)
          commit('setClientSessionId', response.data.id)
          commit('changePlayer1Data', response.data.game_state.game.pl1)
          commit('changePlayer2Data', response.data.game_state.game.pl2)
        })
        .catch(function (error) {
          console.log(error)
          commit('changeGameDataErrored', true)
        })
        .finally(function () {
          console.log('#### deck finally ####')
          commit('changeGameDataLoaded', true)
        })
    },
    makeDeck: function ({ commit, dispatch, getters }) {
      var payload = {
        clientSessionId: getters.getClientSessionId
      }
      var url = "http://127.0.0.1:5000/makedeck"
      var config = {
        headers: {
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*'
          }
      }
      console.log(payload)
      var component = this
      axios
        .post(url, payload , config)
        .then(function (response) {
          console.log(response)
          //dispatch('updateGameData', response.data.game_state)
        })
        .catch(function (error) {
          console.log(error);
        });
    },
    playFirstRound: function ({ commit, dispatch, getters }) {
      var payload = {
        clientSessionId: getters.getClientSessionId
      }
      console.log(payload)
      var url = "http://127.0.0.1:5000/playfirstround"
      var config = {
        headers: {
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*'
          }
      }
      var component = this
      axios
        .post(url, payload , config)
        .then(function (response) {
          console.log(response)
          commit('changePlayer1Data', response.data.game_state.game.pl1)
          commit('changePlayer2Data', response.data.game_state.game.pl2)
        })
        .catch(function (error) {
          console.log(error);
        });
    },
    loadValidPlays: function ({ commit, getters }) {
      // send current deck and player states as input to getvalidplays endpoint functions
      var payload = {
        clientSessionId: getters.getClientSessionId
      }
      console.log(payload)
      var url = "http://127.0.0.1:5000/validplays"
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
    }
  }
})
