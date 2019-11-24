<template>
  <div class="hand-comp" id="hand">
    <div v-if="getGameDataLoaded">
      <b-card
        :class="attrStr"
        :id="attrStr"
      >
      <HandHeader
        :player="player"
        @get-valid-plays="onGetValidPlays"
      />
      <HandValidPlaysControls
        v-if="showValidComputed"
        :player="player"
        :show-valid-plays="activePlayer"
        @valid-plays-change="onValidPlaysChange"
        :valid-selection="validSelection"
      />
      <HandCompCards
        :player="player"
        :highlighted="highlighted"
        @new-selected="onNewSelected"
      />
      </b-card>
    </div>
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
      showValidPlays: true,
      highlighted: [],
      showHighlighted: false
    }
  },
  props: {
    player: String,
    showValid: {
      type: Boolean
    },
    validSelection: {
      type: Array
    }
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
    activePlayer() {
      return !!(this.getCurrentPlayer.name === this.getPlayer.name) //? true : false
    },
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
    },
    showValidComputed () {
      var show
      if (this.activePlayer) {
        show = this.showValidPlays
      }
      return show
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
      console.log('on get valid plays')
      console.log(this.showValidPlays)
      // TODO 
      // separate logic for all these actions aka toggle display  vs highlighted
      this.showValidPlays = !this.showValidPlays
      console.log(this.showValidPlays)
      this.$emit('toggle-valid', this.player)
      if (this.highlighted.length > 0) {
        this.highlighted = []
        this.showHighlighted = false
      }
      this.$emit('new-table-highlighted', [])
    },
    onValidPlaysChange(payload) {
      // console.log('on valid plays change')
      // console.log(payload)
      // console.log(this.highlighted)
      this.highlighted = payload.player
      // console.log(this.highlighted)
      this.$emit('new-table-highlighted', payload.table)
    },
    onNewSelected (payload) {
      console.log('on new selected from hand comp')
      this.$emit('new-selected', payload)
    }
  },
  watch: {
    showValid: function(val, oldVal) {
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
