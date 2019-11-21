<template>
  <div class="table-cards-comp">
    <div v-if="getGameDataLoaded">
      <b-card
        class="table-cards"
        id="table-cards"
      >
        <b-row>
          <b-col md=4>
            <b-button
            @click="onPlayTurn"
          >
            Play Turn
          </b-button>

          </b-col>
          <b-col md=4>Table Cards</b-col>
          <b-col md=4></b-col>
        </b-row>
        <b-row>
          <b-col md=4></b-col>
          <b-col md=4>
            <v-icon
              id="table-cards-icon"
              name="brands/stack-overflow"
              scale=3.5
            ></v-icon>
          </b-col>
          <b-col md=4></b-col>
        </b-row>
        <hr />
          <CardCollection 
            :collection="getTableCards"
            owner="table"
            :highlighted="highlighted"
          />
      </b-card>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import CardCollection from '@/components/CardCollection'
import CardComp from '@/components/CardComp'

export default {
  name: 'TableCards',
  props: {
    tableCards: Array,
    highlighted: Array
  },
  components: {
    CardComp,
    CardCollection
  },
  computed: {
    ...mapGetters([
      'getGameDataLoaded',
      'getDeck',
      'getTableCards'
    ]),
    cards: function () {
      return this.getDeck.filter(x => this.tableCards.includes(x.card))
    }
  },
  methods: {
    log: function (input) {
      var comp = this
      if (input) {
        console.log(input)
      } else {
        console.log(comp)
      }
    },
    onPlayTurn() {
      var payload
      this.$store.dispatch('loadNextTurn', payload)
    }
  },
  mounted: function () {
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

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
