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
              @toggle-select="onToggleSelect"
              :highlighted="cardHighlighted(c)"
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
      cardsSelected: Object,
      selected: []
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
    onToggleSelect (payload) {
      console.log(payload)
      if(payload.isSelected === true) {
        this.selected.push(payload.card)
        console.log('on true new selected from card collection')
        this.$emit('new-selected', this.selected)
      } else if (payload.isSelected === false ) {
        console.log('on false new selected from card collection')
        this.selected = this.selected.filter(x => x !== payload.card)
        this.$emit('new-selected', this.selected)
      }
    }
  },
  mounted: function () {
    console.log('#### card collection comp ####')
    // console.log(this)
  },
  watch: {
    selected: function(val, oldVal) {
      // console.log(this.selected)
      // console.log(val)
      if (val.length !== oldVal.length) {
        console.log('new val from selected watch in card collection')
        this.$emit('new-selected', val)
      }
    }
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
