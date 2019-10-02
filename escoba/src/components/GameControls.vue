<template>
  <div class="deck-comp">
    <div v-if="getValidPlaysLoaded">
      {{ getValidPlays }}
    </div>
    <div v-if="getGameDataLoaded">
      <b-button
        @click="loadValidPlays()"
      >
        Get Valid Plays
      </b-button>
        |
      <b-button
        @click="makeDeck()"
      >
        Get New Deck
      </b-button>
        |
      <b-button
        @click="dealHand()"
      >
        Deal Hand
      </b-button>
        |
      <b-button
        @click="playFirstRound()"
      >
        Play First Round
      </b-button>
      |
      <b-button
        @click="unpause()"
      >
        Unpause
      </b-button>
      |
      <b-button
        @click="pause()"
      >
        Pause
      </b-button>
      |
      <b-button
        @click="log"
      >
        Log
      </b-button>
      <hr />
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import axios from 'axios'

export default {
  name: 'GameControls',
  components: {
  },
  props: {
    msg: String
  },
  computed: {
    ...mapGetters([
      'getGameDataLoaded',
      'getDeck',
      'getCards',
      'getDeckOrder',
      'getValidPlaysLoaded',
      'getValidPlays',
      'getPlayer1',
      'getPlayer2'
    ])
  },
  methods: {
    log: function(input) {
      var comp = this
      if(input) {
        console.log(input)
      }
      else {
        console.log(comp)
      }
    },
    post: function (url, payload) {
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
    makeDeck: function () {
      var payload = {
        deck: this.getDeckOrder,
        isDeck: true
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
          component.$store.dispatch('updateGameData', response.data)
        })
        .catch(function (error) {
          console.log(error);
        });
    },
    dealHand: function () {
      var payload = {
        deck: this.getDeck,
        isDeck: true
      }
      this.post("http://127.0.0.1:5000/dealhand", payload)
    },
    playFirstRound: function () {
      var payload = {
        deck: this.getDeck,
        isDeck: true
      }
      var url = "http://127.0.0.1:5000/playfirstround"
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
          component.$store.dispatch('updateGameData', response.data)
        })
        .catch(function (error) {
          console.log(error);
        });
    },
    unpause: function () {
      var payload = {
        paused: false
      }
      this.post("http://127.0.0.1:5000/unpause", payload)
    },
    pause: function () {
      var payload = {
        paused: true
      }
      this.post("http://127.0.0.1:5000/pause", payload)
    },
    loadValidPlays: function () {
      // send current deck and player states as input to getvalidplays endpoint functions
      var payload = {
        deck: this.getDeckOrder,
        player1: this.getPlayer1,
        player2: this.getPlayer2
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
  },
  mounted: function() {
    this.log(this.getDeckOrder)
    if(this.getGameDataLoaded) {
      console.log(this.getCards)
      console.log(this.getDeck)
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
