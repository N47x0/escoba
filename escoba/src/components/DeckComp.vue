<template>
  <div class="deck-comp">
    <div v-if="getValidPlaysLoaded">
      {{ getValidPlays }}
    </div>
    <div v-if="getGameDataLoaded">
      <CardComp
        v-for="(card, i) in getDeck"
        :key="i"
        :card="card"
        :isDeck="true"
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
      'getGameDataLoaded',
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
  },
  mounted: function() {
    if(this.getGameDataLoaded) {
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
