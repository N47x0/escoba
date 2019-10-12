<template>
  <div class="deck-table">
    <div v-if="getGameDataLoaded">
      <b-table responsive striped hover :items="this.getDeck"></b-table>
      {{ this.getDeck }}
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  name: 'DeckTable',
  props: {
    msg: String
  },
  computed: {
    ...mapGetters([
      'getGameDataLoaded',
      'getDeck'
    ])
  },
  methods: {
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
    // this.log(this.getDeck)
    if (this.getGameDataLoaded) {
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
