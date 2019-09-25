import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    deckLoaded: false,
    deckErrored: false,
    deckData: [],
    validPlaysLoaded: false,
    validPlaysErrored: false,
    validPlays: []
  },
  getters: {
    getCards: function (state) {
      var cards = []
      // console.log(Object.entries(state.deckData.cards))
      Object.entries(state.deckData.cards).forEach((v, i, a) => {
        cards.push(v[0])
      })
      return cards
    },
    getDeck: function (state, getters) {
      var deck = []
      console.log(state.deckData)
      // console.log(Object.entries(state.deckData.cards))
      Object.entries(state.deckData.cards).forEach((value, index, array) => {
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
    getDeckLoaded: (state) => {
      return state.deckLoaded
    },
    getDeckOrder: (state) => {
      return state.deckData.order
    },
    getValidPlaysLoaded: (state) => {
      return state.validPlaysLoaded
    },
    getValidPlays: (state) => {
      return state.validPlays
    }
  },
  mutations: {
    changeDeckErrored: function (state, errored) {
      state.deckErrored = errored
    },
    changeDeckLoaded: function (state, loaded) {
      state.deckLoaded = loaded
    },
    initData: function (state, data) {
      state.deckData = data
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
    loadDeck: function ({ commit, state }) {
      axios.get('http://127.0.0.1:5000/makedeck')
        .then(function (response) {
          console.log(response)
          commit('initData', response.data)
        })
        .catch(function (error) {
          console.log(error)
          commit('changeDeckErrored', true)
        })
        .finally(function () {
          console.log('#### deck finally ####')
          commit('changeDeckLoaded', true)
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
