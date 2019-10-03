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
      console.log(state.gameData)
      // console.log(Object.entries(state.gameData.cards))
      Object.entries(state.gameData.game.deck.card_store).forEach((value, index, array) => {
        var [a, b, c] = value[1]
        var d = b.toString() + a
        deck.push({ 'suit': a, 'value': b, 'owner': c, 'card': d })
      })
      console.log(deck)
      console.log(getters.getDeckOrder)
      deck.sort(function (a, b) {
        return getters.getDeckOrder.indexOf(a.card) - getters.getDeckOrder.indexOf(b.card)
      })
      console.log(deck)
      return deck
    },
    getPlayer1: (state) => {
      return state.gameData.game.pl1
    },
    getPlayer2: (state) => {
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
      console.log(state.gameData)
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
      console.log(payload)
      state.gameData.game = payload.game
      console.log(state.gameData)
    },
    changeValidPlaysErrored: function (state, errored) {
      state.validPlaysErrored = errored
    },
    changeValidPlaysLoaded: function (state, loaded) {
      state.validPlaysLoaded = loaded
    },
    changeValidPlays: function (state, data) {
      state.validPlays = data
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
    loadValidPlays: function ({ commit, state }) {
      axios.get('http://127.0.0.1:5000/validplays')
        .then(function (response) {
          console.log(response)
          commit('changeValidPlays', response.data)
        })
        .catch(function (error) {
          console.log(error)
          commit('changeValidPlaysErrored', true)
        })
        .finally(function () {
          console.log('#### deck finally ####')
          commit('changeValidPlaysLoaded', true)
        })
    }
  }
})
