<template>
  <div class="hand-header">
    <b-row class="player-name">
      <b-col>{{getPlayer.name}}'s Hand</b-col>
    </b-row>
    <b-row>
      <b-col>
        <div
          @click="onGetValidPlays"
          :disabled="!activePlayer"
        >
          <v-icon
            id="hand-icon"
            name="hand-spock"
            scale=3.5
          />
        </div>
        <b-tooltip target="hand-icon" triggers="hover" placement="right">
          <b>Toggle Valid Plays Controls</b>
        </b-tooltip>            
      </b-col>
    </b-row>
    <hr />
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'

export default {
  name: 'HandHeader',
  data: function () {
    return {
    }
  },
  props: {
    player: String
  },
  components: {
  },
  computed: {
    ...mapGetters([
      'getPlayer1',
      'getPlayer2',
      'getCurrentPlayer'
    ]),
    getPlayer: function () {
      return this[`getPlayer${this.player}`]
    },
    activePlayer() {
      return !!(this.getCurrentPlayer.name === this.getPlayer.name) //? true : false
    }
  },
  methods: {
    ...mapActions([
    ]),
    log: function (input) {
      var comp = this
      if (input) {
        console.log(input)
      } else {
        console.log(comp)
      }
    },
    onGetValidPlays() {
      this.$emit('get-valid-plays')
    }
  },
  watch: {
  },
  mounted: function () {
    console.log('#### hand header ####')
    // console.log(this)
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

/* set hand comp top padding smaller to separate from icon */

/* set all cards to center of div and position: relative for absolute positioning of child icons */

  .hand-header {
    margin: 0rem 0rem 0rem 0rem;
  }

  .player-name {
    margin: 0rem 0rem 1rem 0rem;
  }
  
  button {
    background-color: rgba(255, 255, 255, 0.219);
    color:#42b983
  }

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
