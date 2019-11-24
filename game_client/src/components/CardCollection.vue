<template>
  <div class="card-collection" v-if="getGameDataLoaded">
    <!-- <b-alert
      :show="tooManySelections"
      dismissible
      variant="warning"
      @dismissed="clearSelections"
    ></b-alert> -->
    <b-row>
      <b-col
        lg="6"
        sm="12"
        v-for="(c, i) in collection"
        :key="i"
      >
        <CardComp
          :class="[owner + '-card']"
          :card="c"
          :isHand="true"
          @toggle-select="onToggleSelect"
          :highlighted="cardHighlighted(c)"
          :selected="cardSelected(c)"
        />
      </b-col>
    </b-row>
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
import CardComp from '@/components/CardComp'

export default {
  name: 'CardCollection',
  data: function () {
    return {
      cardsSelected: Object,
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
    },
    highlighted: {
      type: Array,
      required: false
    },
    selected: {
      type: Array,
      required: false
    }
  },
  components: {
    CardComp
  },
  computed: {
    ...mapGetters([
      'getGameDataLoaded',
      'validateSelection'
    ]),
    getPlayer: function () {
      return this[`getPlayer${this.player}`]
    },
    getHand: function () {
      return this.getPlayer.hand
    },
    columns () {
      return this.collection.length < 3 ? 6 : 4
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
    cardHighlighted (card) {
      return this.highlighted.some(x => x.id === card.id) ? true : false
    },
    cardSelected (card) {
      var cardSelected
      if (this.selected !== undefined) {
        // console.log(this.selected)
        // console.log(this.selected.map(x => x.id))
        // console.log(card.id)
        cardSelected = this.selected.some(x => x.id === card.id) ? true : false
      }
      // console.log(cardSelected)
      return cardSelected
    },
    onToggleSelect (payload) {
      console.log('on new selected from card collection')
      console.log(payload)
      this.$emit('new-selected', payload)
    }
  },
  mounted: function () {
    console.log('#### card collection comp ####')
    console.log(this.owner)
    console.log(this)
  },
  watch: {
    selected: function(val, oldVal) {
      // console.log(this.selected)
      // console.log(val)
      if (val.length !== oldVal.length) {
        console.log('new val from selected watch in card collection')
      }
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

/* set hand comp top padding smaller to separate from icon */

/* set all cards to center of div and position: relative for absolute positioning of child icons */

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
