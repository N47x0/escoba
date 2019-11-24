<template>
  <div class="play-area-comp">
    <div v-if="getGameDataLoaded">
      <b-container
        class="play-area"
        id="play-area"
      >
        <v-icon
          id="play-area-icon"
          name="child"
          scale=3.5
        />
        <h5>Play Area</h5>
        <hr />
        <b-row>
          <b-col md=4>
            <HandComp
              @toggle-valid="onToggleValid"
              :show-valid="showValidPlayer1"
              @new-table-highlighted="onNewTableHighlighted" 
              player=1 
              @new-selected="onNewSelected"
              :valid-selection="validSelection"
              @play-turn="onPlayTurn"
              :selected="selectedPlayer1"
            />
          </b-col>
          <b-col md=4>
            <TableCardsSingle
              :table-cards="getTableCards"
              :highlighted="tableCardsHighlighted"
              @new-selected="onNewSelected"
              @play-turn="onPlayTurn"
              :selected="selectedTable"
            />
          </b-col>
          <b-col md=4>
            <HandComp
              @toggle-valid="onToggleValid"
              :show-valid="showValidPlayer2" 
              @new-table-highlighted="onNewTableHighlighted" 
              player=2 
              @new-selected="onNewSelected"
              :valid-selection="validSelection"
              @play-turn="onPlayTurn"
              :selected="selectedPlayer2"
            />
          </b-col>
        </b-row>
      </b-container>
    </div>
  </div>
</template>

<script>
import TableCardsSingle from '@/components/Table/TableCardsSingle'
import HandCompSingle from '@/components/Hand/HandCompSingle'
import HandComp from '@/components/Hand/HandComp'

import { mapGetters } from 'vuex'

function validate (query, selection) {
  if (selection.length > 0) {
    var keys = Object.keys(selection[0])
    selection.forEach((set, is) => {
      set.forEach((item, ii) => {
        keys.forEach((key, ik) => {
          if (set[ii][key] === query[ii][key]) {
            return true
          } else {
            return false
          }
        })  
      })
    })  
  } else {
    return false
  }
}

function compareArray (arr1, arr2) {
  if (arr1.length === arr2.length) {
    var keys = Object.keys(arr2[0])
    arr1.forEach((item, i) => {
      keys.forEach((key, ik) => {
        if (arr1[i][key] === arr2[i][key]) {
          return true
        } else {
          return false
        }
      })  
    })      
  } 
}


export default {
  name: 'PlayArea',
  props: {
  },
  components: {
    TableCardsSingle,
    HandComp,
    HandCompSingle
  },
  data () {
    return {
      tableCardsHighlighted: [],
      showValidPlayer1: false,
      showValidPlayer2: false,
      selectedPlayer1: [],
      selectedPlayer2: [],
      selectedTable: []
    }
  },
  computed: {
    ...mapGetters([
      'getGameDataLoaded',
      'getTableCards',
      'validateSelection',
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
    totalSelected () {
      var totalSelected = []
      console.log(this.selectedPlayer1)
      console.log(this.selectedPlayer2)
      console.log(this.selectedTable)
      if ((this.selectedPlayer1.length > 0 || this.selectedPlayer2.length > 0) || this.selectedTable.length > 0) {
        console.log(this.selectedPlayer1)
        console.log(this.selectedPlayer2)
        console.log(this.selectedTable)
        totalSelected = [ ...this.selectedPlayer1, ...this.selectedPlayer2, ...this.selectedTable]
        console.log(totalSelected)
      return totalSelected
      }
    },
    validSelection () {
      var validSelection
      // console.log(val)
      if (this.totalSelected !== undefined) {
        console.log("validating")
        if (this.validateSelection(this.totalSelected)) {
          console.log("selection validated")
          validSelection = this.totalSelected
        }
      }
      return validSelection
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
    newTableCardsHighlighted(payload) {
      this.tableCardsHighlighted = payload
      // console.log(payload)
    },
    onToggleValid(payload) {
      console.log('on toggle valid')
      // console.log(payload)
      if (payload === '1') {
        // console.log(this.showValidPlayer2)
        this.showValidPlayer1 = true
        this.showValidPlayer2 = false  
        // console.log(this.showValidPlayer2)
      } else if (payload === '2') {
        // console.log(this.showValidPlayer1)
        this.showValidPlayer2 = true
        this.showValidPlayer1 = false  
        // console.log(this.showValidPlayer1)
      }
      // list of players in store for future games with more than 2 possible players
      // payload === '1' ? this.showValidPlayer2 = false : this.showValidPlayer1 = false
    },
    onNewTableHighlighted(payload) {
      this.tableCardsHighlighted = payload
    },
    onNewSelected (payload) {
      console.log('on new selected from play area')
      console.log(payload)
      var owner = payload.card.owner.charAt(0).toUpperCase() + payload.card.owner.slice(1).replace(' ', '')
      console.log(owner)
      if(payload.isSelected === true) {
        console.log(this[`selected${owner}`])
        this[`selected${owner}`].push(payload.card)
        console.log(this[`selected${owner}`])
        console.log('on true new selected from play area')
      } else if (payload.isSelected === false ) {
        console.log('on false new selected from play area')
        console.log(this[`selected${owner}`])
        this[`selected${owner}`] = this[`selected${owner}`].filter(x => x !== payload.card)
        console.log(this[`selected${owner}`])
      } else if (payload.length === 0) {
        console.log('empty payload from play area')
        this.selected = payload
        console.log(this.selected)
      }
    },
    onPlayTurn () {
      this.selectedTable = []
      this.selectedPlayer1 = []
      this.selectedPlayer2 = []
    }
  },
  mounted: function () {
  },
  watch: {
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
