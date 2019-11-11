<template>
  <div class="card-collection">
    <div v-if="getGameDataLoaded">
      <!-- <b-alert
        :show="tooManySelections"
        dismissible
        variant="warning"
        @dismissed="clearSelections"
      ></b-alert> -->
        <b-row>
          <b-col
            v-for="(c, i) in collection"
            :key="i"
          >
            <CardComp
              :class="[owner + '-card']"
              :card="c"
              :isHand="true"
              v-on:card-selected="cardSelected($event)"
              v-bind:isSelected="cardsSelected[c.id]"
            />
          </b-col>
        </b-row>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
import CardComp from '@/components/CardComp'

export default {
  name: 'CardCollection',
  data: function () {
    return {
      cardsSelected: Object
    }
  },
  props: {
    owner: {
      type: String
    },
    collection: {
      type: Array,
      required: true,
      default: () => []
    }
  },
  components: {
    CardComp
  },
  computed: {
    ...mapGetters([
      'getGameDataLoaded'
    ]),
    getPlayer: function () {
      return this[`getPlayer${this.player}`]
    },
    getHand: function () {
      return this.getPlayer.hand
    },
    cards: function () {
      console.log(this.getHand)
      return this.getDeck.filter(x => this.getHand.includes(x.card))
    },
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
    cardSelected (card) {
      console.log(card)
      console.log(this.cardsSelected)
      if (card.id in this.cardsSelected) {
        var s = this.cardsSelected[card.id]
        this.cardsSelected = Object.assign({}, this.cardsSelected, { [card.id]: s })
        this.$set(this.cardsSelected, card.id, !(s))
      } else {
        this.cardsSelected = Object.assign({}, this.cardsSelected, { [card.id]: true })
      }
    }
  },
  mounted: function () {
    console.log('#### card collection comp ####')
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
