<template>
  <div id="table-cards-single" class="table-cards-single"  v-if="getGameDataLoaded">
    <b-row>
      <b-col md=4>
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
      @new-selected="onNewSelected"
      :selected="selected"
    />
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import CardCollection from '@/components/CardCollection'
import CardComp from '@/components/CardComp'

export default {
  name: 'TableCardsSingle',
  props: {
    tableCards: Array,
    highlighted: Array,
    selected: {
      type: Array,
      required: false,
      default: () => []
    },
  },
  components: {
    CardComp,
    CardCollection
  },
  data() {
    return {
      validTurn: false
    }
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
    onNewSelected (payload) {
      console.log('on new selected from table cards single')
      console.log(payload)
      this.$emit('new-selected', payload)
    },
  },
  watch: {
    selected: {
      deep: true,
      immediate: true,
      handler: function(val, oldVal) {
        if (val !== undefined && oldVal !== undefined) {
          // console.log(this.selected)
          if(val.length !== oldVal.length) {
            console.log('change in selected watch from table cards')
            // console.log(val)
            // console.log(oldVal)
          }
        }
      }
    }
  },
  mounted: function () {
    // console.log(this.selected)
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

/* set all cards to center of div and position: relative for absolute positioning of child icons */


  .table-cards-single {
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
