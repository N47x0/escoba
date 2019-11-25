<template>
  <div class="play-area-comp" v-if="getGameDataLoaded">
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
            @toggle-valid-plays="onToggleValidPlays"
            :active-player="activePlayer1" 
            :show-valid="activePlayer1" 
            @valid-plays-change="onValidPlaysChange" 
            player=1 
            @new-selected="onNewSelected"
            :valid-selection="validSelection"
            @play-turn="onPlayTurn"
            :selected="selectedPlayer1"
            :highlighted="higlightedPlayer1"
          />
        </b-col>
        <b-col md=4>
          <TableCardsSingle
            :table-cards="getTableCards"
            @new-selected="onNewSelected"
            @play-turn="onPlayTurn"
            :selected="selectedTable"
            :highlighted="higlightedTable"
          />
        </b-col>
        <b-col md=4>
          <HandComp
            @toggle-valid-plays="onToggleValidPlays"
            :active-player="activePlayer2" 
            :show-valid="activePlayer2" 
            @valid-plays-change="onValidPlaysChange" 
            player=2 
            @new-selected="onNewSelected"
            :valid-selection="validSelection"
            @play-turn="onPlayTurn"
            :selected="selectedPlayer2"
            :highlighted="higlightedPlayer2"
          />
        </b-col>
      </b-row>
    </b-container>
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
      showValidPlayer1: null,
      showValidPlayer2: null,
      selectedPlayer1: [],
      selectedPlayer2: [],
      selectedTable: [],
      higlightedPlayer1: [],
      higlightedPlayer2: [],
      higlightedTable: []
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
    getPlayer: function (payload) {
      return this[`getPlayer${payload}`]
    },
    activePlayer1 () {
      return !!(this.getCurrentPlayer.name === 'Player 1') //? true : false
    },
    activePlayer2 () {
      return !!(this.getCurrentPlayer.name === 'Player 2') //? true : false
    },
    totalSelected () {
      var totalSelected = []
      if ((this.selectedPlayer1.length > 0 || this.selectedPlayer2.length > 0) || this.selectedTable.length > 0) {
        totalSelected = [ ...this.selectedPlayer1, ...this.selectedPlayer2, ...this.selectedTable]
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
    onToggleValidPlays(payload) {
      console.log('on toggle valid plays from play area')
      console.log(payload)
      if (payload === '1') {
        this.showValidPlayer1 = !this.showValidPlayer1
      } else if (payload === '2') {
        this.showValidPlayer2 = !this.showValidPlayer2
      }
      // list of players in store for future games with more than 2 possible players
      // payload === '1' ? this.showValidPlayer2 = false : this.showValidPlayer1 = false
    },
    onValidPlaysChange(payload) {
      console.log('on valid plays change from play area')
      console.log(payload)
      // console.log(this.highlighted)
      this.higlightedTable = payload.tablePlays
      if (payload.player === '1') {
        console.log('player 1 from valid plays change')
        this.higlightedPlayer1 = payload.playerPlays
      } else if (payload.player === '2') {
        this.higlightedPlayer2 = payload.playerPlays
      }
    },
    onNewSelected (payload) {
      console.log('on new selected from play area')
      console.log(payload)
      var owner = payload.card.owner.charAt(0).toUpperCase() + payload.card.owner.slice(1).replace(' ', '')
      if(payload.isSelected === true) {
        this[`selected${owner}`].push(payload.card)
        console.log('on true new selected from play area')
      } else if (payload.isSelected === false ) {
        console.log('on false new selected from play area')
        this[`selected${owner}`] = this[`selected${owner}`].filter(x => x !== payload.card)
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
