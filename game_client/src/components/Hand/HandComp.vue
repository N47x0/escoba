<template>
  <div class="hand-comp">
    <div v-if="getGameDataLoaded">
      <b-card
        :class="'hand-' +getPlayer.name"
        :id="'hand-' +getPlayer.name"
      >
      <HandHeader
        :player="player"
        @on-valid-plays="onValidPlays"
      />
      <HandValidPlaysControls
        v-if="showValidPlays"
        :player="player"
        :show-valid-plays="showValidPlays"
      />
      <HandCompCards
        :player="player"
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
      showValidPlays: false,
      currentValidPlayIndex: 0
    }
  },
  props: {
    player: String,
    showValid: {
      type: Boolean,
      default: false
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
      'getPlayer2'
    ]),
    getPlayer: function () {
      return this[`getPlayer${this.player}`]
    },
    validPayload () {
      // return "test-payload"
      return JSON.stringify({
        sessionId: this.getClientSessionId
      })
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
    onValidPlays() {
      console.log('on valid plays')
      // console.log(this.showValidPlays)
      this.showValidPlays = !this.showValidPlays
      // console.log(this.showValidPlays)
      this.currentValidPlayIndex = 0
      this.$emit('toggle-valid', this.player)
    },
  },
  watch: {
    showValid: function(val, oldVal) {
      console.log(this.showValidPlays)
      if(val !== oldVal) {
        console.log(this.showValidPlays)
        this.showValidPlays = val
        console.log(this.showValidPlays)
      }
    }
  },
  mounted: function () {
    console.log('#### hand comp ####')
    console.log(this.showValid)
    // console.log(this)
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
