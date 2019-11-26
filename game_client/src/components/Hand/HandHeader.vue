<template>
  <div class="hand-header">
    <div class="score-container">
      <b-row class="score-header">
        <b-col>
          <b>Score:</b>
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <div class="score-display">
            <v-icon
              class="score-icon-stack"
              label="score-icon"
            >
              <v-icon 
              class="score-icon"
              name="circle" 
              scale="5"
              />
              <!-- <v-icon style="color:white" name="times" /> -->
            </v-icon>
            <b class="score-text">
                {{getPlayer.score}}
            </b>
          </div>
        </b-col>
      </b-row>
    </div>
    <b-row class="player-name">
      <b-col>{{getPlayer.name}}'s Hand</b-col>
    </b-row>
    <b-row>
      <b-col>
        <div
          @click="toggleValidPlays"
          :class="[{'active-player': this.activePlayer}, {'inactive-player': !this.activePlayer}, this.getPlayer.name.toLowerCase().replace(' ', '') + '-hand-icon-div']"
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
    toggleValidPlays() {
      this.$emit('toggle-valid-plays')
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
  

  /* .score-container::before {
    content: "Score";
  } */

  .score-container {
    padding: 1rem 0rem 0rem 0rem;
  }

  .score-display {
    padding: 1rem 0rem 0rem 0rem;
    position: relative;
    height: 8rem;
  }
  
  .score-text {
    color: white;
    font-size: 3.5rem;
    position: relative;
    bottom: 5rem;
    display: block;
  }


  .score-icon-stack {
    position: relative;
    display: inline-block;
  }

  /* .score-icon::after {
    content: "\0030";
    color: white;
  } */

  .hand-header {
    margin: 0rem 0rem 0rem 0rem;
  }

  .player-name {
    margin: 0rem 0rem 1rem 0rem;
  }
  
  .inactive-player {
    color: gray
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
