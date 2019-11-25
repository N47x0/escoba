<template>
  <div id="hand-valid-plays-controls" class="hand-valid-plays-controls">
    <b-row 
      v-if="showValidPlays"
      align-h="center"
    >
      <b-col></b-col>
      <b-col>
        <b-button-toolbar 
          aria-label="Toolbar with buttons to control which valid play is displayed and selected"
          :justify="true"
          class="valid-plays-toolbar"
          id="valid-plays-toolbar"
        >
          <b-button-group 
          >
            <b-button
              @click="previousValidPlay"
            >            
              <v-icon
                id="backward-icon"
                name="backward"
              ></v-icon>
            </b-button>
            <b-button
              id="play-turn-button"
              @click="onPlayTurn"
            >
              {{ playerValidPlaysIds }} | {{ tableValidPlaysIds }}
            </b-button>
            <b-tooltip target="play-turn-button" triggers="hover">
              <b>Play Turn</b>
            </b-tooltip>            
            <b-button
              @click="nextValidPlay"
            >            
              <v-icon
                id="forward-icon"
                name="forward"
              ></v-icon>
            </b-button>
          </b-button-group>
        </b-button-toolbar>
      </b-col>
      <b-col></b-col>
    </b-row>
    <hr />
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'

export default {
  name: 'HandValidPlaysControls',
  data: function () {
    return {
      cardsSelected: Object,
      currentValidPlayIndex: 0
    }
  },
  props: {
    selectable: Number,
    // cards: Object,
    player: String,
    showValidPlays: {
      type: Boolean,
      default: false
    },
    validSelection: {
      type: Array
    }
  },
  components: {
  },
  computed: {
    ...mapGetters([
      'getValidPlays',
      'getPlayer1',
      'getPlayer2',
      'getClientSessionId'
    ]),
    getPlayer: function () {
      return this[`getPlayer${this.player}`]
    },
    validPlays() {
      return this.getValidPlays[`Player ${this.player}`]     
    },
    playerValidPlays() {
      var pvp = []
      if (this.showValidPlays) {
        pvp = this.validPlays[this.currentValidPlayIndex].filter(x => x.owner === this.getPlayer.name)
      }
      return pvp
    },
    tableValidPlays() {
      var tvp = []
      if (this.showValidPlays === true) {
        tvp = this.validPlays[this.currentValidPlayIndex].filter(x => x.owner === 'table')
      }
      return tvp
    },
    playerValidPlaysIds() {
      var text = ''
      var ids = this.playerValidPlays.map(x => x.id)
      if (ids.length > 1) {
        text = ids.join(', ')
      } else {
        text = ids[0]
      }
      return text
    },
    tableValidPlaysIds() {
      var text = ''
      var ids = this.tableValidPlays.map(x => x.id)
      if (ids.length > 1) {
        text = ids.join(', ')
      } else {
        text = ids[0]
      }
      return text
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
    previousValidPlay() {
      if (this.currentValidPlayIndex === 0) {
        this.currentValidPlayIndex = this.validPlays.length - 1
      } else {
        this.currentValidPlayIndex -= 1
      }
    },
    nextValidPlay() {
      if (this.currentValidPlayIndex === this.validPlays.length - 1) {
        this.currentValidPlayIndex = 0
      } else {
      this.currentValidPlayIndex += 1
      }
    },
    // async onPlayTurn() {
    onPlayTurn() {
      var payload = {
        cardsPlayed: this.validSelection,
        sessionId: this.getClientSessionId
      }
      // await this.$store.dispatch('loadNextTurn', payload).then(
      //   this.$emit('on-valid-plays')
      // )
      this.$store.dispatch('loadNextTurn', payload)
      this.$emit('play-turn')
    }
  },
  watch: {
    tableValidPlays: function(val, oldVal) {
      if(val !== oldVal) {
        this.$emit('valid-plays-change', {
          tablePlays: this.tableValidPlays,
          playerPlays: this.playerValidPlays,
          player: this.player
        })
      }
    },
    playerValidPlays: function(val, oldVal) {
      if(val !== oldVal) {
        this.$emit('valid-plays-change', {
          tablePlays: this.tableValidPlays,
          playerPlays: this.playerValidPlays,
          player: this.player
        })
      }
    }
  },
  mounted: function () {
    console.log('#### hand valid plays controls ####')
    // initialize highlighted cards 
    this.$emit('valid-plays-change', {
      tablePlays: this.tableValidPlays,
      playerPlays: this.playerValidPlays,
      player: this.player
    })
    // console.log(this)
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

#valid-plays-toolbar {
  width: 100%;
}
#hand-valid-plays-controls {
  width: 100%;
}

/* set hand comp top padding smaller to separate from icon */

/* set all cards to center of div and position: relative for absolute positioning of child icons */


  
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
