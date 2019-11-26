<template>
  <div v-if="getGameDataLoaded"
    :class="attrStr"
    :id="attrStr"
  >
    <b-row>
      <b-col>
        <HandHeader
          :player="player"
          @toggle-valid-plays="onToggleValidPlays"
        />
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <HandValidPlaysControls
          v-if="showValidPlays"
          :player="player"
          :show-valid-plays="showValidPlays"
          @valid-plays-change="onValidPlaysChange"
          :valid-selection="validSelection"
          @play-turn="onPlayTurn"
        />
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <HandCompCards
          :player="player"
          :highlighted="highlighted"
          @new-selected="onNewSelected"
          :selected="selected"
        />
      </b-col>
    </b-row>
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
import HandHeader from '@/components/Hand/HandHeader'
import HandCompCards from '@/components/Hand/HandCompCards'
import HandValidPlaysControls from '@/components/Hand/HandValidPlaysControls'

export default {
  name: 'HandComp',
  data: function () {
    return {
      showValidPlays: this.activePlayer === true ? true : false,
      showHighlighted: false
    }
  },
  props: {
    player: String,
    activePlayer: {
      type: Boolean
    },
    validSelection: {
      type: Array
    },
    selected: {
      type: Array,
      required: false,
      default: () => []
    },
    highlighted: {
      type: Array,
      required: false
    },
  },
  components: {
    HandHeader,
    HandValidPlaysControls,
    HandCompCards
  },
  computed: {
    ...mapGetters([
      'getGameDataLoaded',
      'getValidPlays',
      'getPlayer1',
      'getPlayer2',
      'getCurrentPlayer'
    ]),
    getPlayer: function () {
      return this[`getPlayer${this.player}`]
    },
    // activePlayer() {
    //   return !!(this.getCurrentPlayer.name === this.getPlayer.name) //? true : false
    // },
    validPayload () {
      // return "test-payload"
      return JSON.stringify({
        sessionId: this.getClientSessionId
      })
    },
    attrStr () {
      var formatted = this.getPlayer.name.toLowerCase()
      formatted = formatted.replace(' ', '-')
      return 'hand-' + formatted
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
    onToggleValidPlays() {
      console.log('on toggle valid plays from hand comp')
      if (this.activePlayer === true) {
        this.showValidPlays = !this.showValidPlays
      }
    },
    onValidPlaysChange(payload) {
      // console.log('on valid plays change')
      // console.log(payload)
      // console.log(this.highlighted)
      // this.highlighted = payload.player
      // console.log(this.highlighted)
      this.$emit('valid-plays-change', payload)
    },
    onNewSelected (payload) {
      console.log('on new selected from hand comp')
      console.log(payload)
      this.$emit('new-selected', payload)
    },
    onPlayTurn () {
      console.log('on play turn from hand comp')
      this.$emit('play-turn')
      var payload = {
        player: this.player,
        setting: false
      }
      this.$emit('toggle-valid-plays', payload)
    }
  },
  watch: {
    activePlayer: function(val, oldVal) {
      if(val !== oldVal) {
        this.showValidPlays = val
      }
    },
    showHighlighted: function(val, oldVal) {
      if(val !== oldVal) {
        this.highlighted = val
      }
    },
    highlighted: function(val, oldVal) {
      if(val !== oldVal) {
        // console.log(val)
        // console.log(oldVal)
      }
    },
    selected: function(val, oldVal) {
      if(val.length !== oldVal.length) {
        console.log('change in selected watch from hand comp')
        // console.log(val)
        // console.log(oldVal)
      }
    }
  },
  mounted: function () {
    console.log('#### hand comp ####')
    // console.log(this.showValid)
    // console.log(this)
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

  [class*=hand-player-] {
    background-color: #1d2833;
    border: solid 1px #42b983;
  }

  /* h3 {
    margin: 40px 0 0;
  } */
  ul {
    list-style-type: none;
    padding: 0;
  }
  /* li {
    display: inline-block;
    margin: 0 10px;
  } */
  a {
    color: #42b983;
  }

</style>
