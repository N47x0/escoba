<template>
  <div class="hand-header">
    <b-row>
      <b-col md=4></b-col>
      <b-col md=4>{{getPlayer.name}}'s Hand</b-col>
      <b-col md=4></b-col>
    </b-row>
    <b-row>
      <b-col md=4>
        <!-- <b-button
          @click="loadValidPlays(validPayload)"
        > -->
        <b-button
          @click="onGetValidPlays"
          :disabled="!activePlayer"
        >
          Get Valid Plays
        </b-button>
      </b-col>
      <b-col md=4>
        <v-icon
          id="hand-icon"
          name="hand-spock"
          scale=3.5
        ></v-icon>
      </b-col>
      <b-col md=4>
        <b-button
        >
          Get Best Play
        </b-button>
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
      this.$emit('on-valid-plays')
    }
  },
  watch: {
  },
  mounted: function () {
    console.log('#### hand comp ####')
    // console.log(this)
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

/* set hand comp top padding smaller to separate from icon */

/* set all cards to center of div and position: relative for absolute positioning of child icons */

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
