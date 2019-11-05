<template>
  <div class="deck-comp">
    <div v-if="getValidPlaysLoaded">
      {{ getValidPlays }}
    </div>
    <div v-if="getGameDataLoaded">
      <b-button
        @click="loadGameData()"
      >
        Get New Deck
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
import { mapGetters, mapActions } from 'vuex'

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
      'getClientSessionId',
      'getDeck',
      'getCards',
      'getDeckOrder',
      'getValidPlaysLoaded',
      'getValidPlays',
      'getPlayer1',
      'getPlayer2',
      'getTableCards'
    ])
  },
  methods: {
    ...mapActions([
      'loadValidPlays',
      'makeDeck',
      'loadGameData',
      'playFirstRound'
    ]),
    log: function (input) {
      var comp = this
      if (input) {
        console.log(input)
      } else {
        console.log(comp)
      }
    }
  },
  mounted: function () {
    // this.log(this.getDeckOrder)
    if (this.getGameDataLoaded) {
      // console.log(this.getCards)
      // console.log(this.getDeck)
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
