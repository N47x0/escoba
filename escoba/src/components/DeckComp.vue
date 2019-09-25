<template>
  <div class="deck-comp">
    <div v-if="getValidPlaysLoaded">
      {{ getValidPlays }}
    </div>
    <div v-if="getDeckLoaded">
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
        @click="playRound()"
      >
        Play Round
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
      <hr />
      <CardComp
        v-for="(card, i) in getDeck"
        :key="i"
        :card="card"
      />
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import CardComp from '@/components/CardComp'
import axios from 'axios'

export default {
  name: 'DeckComp',
  components: {
    CardComp
  },
  props: {
    msg: String
  },
  computed: {
    ...mapGetters([
      'getDeckLoaded',
      'getDeck',
      'getCards',
      'getDeckOrder',
      'getValidPlaysLoaded',
      'getValidPlays'
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
        deck: this.getDeck,
        isDeck: true
      }
      this.post("http://127.0.0.1:5000/makedeck", payload)
    },
    dealHand: function () {
      var payload = {
        deck: this.getDeck,
        isDeck: true
      }
      this.post("http://127.0.0.1:5000/dealhand", payload)
    },
    playRound: function () {
      var payload = {
        deck: this.getDeck,
        isDeck: true
      }
      this.post("http://127.0.0.1:5000/playround", payload)
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
      this.post("http://127.0.0.1:5000/unpause", payload)
    },
    loadValidPlays: function () {
      var payload = {
        deck: this.getDeckOrder,
        isDeck: true
      }
      this.post("http://127.0.0.1:5000/validplays", payload)
      this.$store.dispatch('loadValidPlays')
    }
  },
  mounted: function() {
    this.log(this.getDeckOrder)
    if(this.getDeckLoaded) {
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
