<template>
    <div
      class="crud-form"
      id="crud-form"
    >
      <b-form>
        <b-button 
          type="submit" 
          class="create-rule-button"
          id="create-rule-button"
          @click="onCreateRule"
        >
          Create Rule
        </b-button>        
        <b-form-group
          id="game-input-group"
          label="Select Game:"
          label-for="game-input"
          description="Select the game for which you will CRUD"
        >          
          <b-form-select 
            v-model="selectedGame" 
            :options="options"
          ></b-form-select>
        </b-form-group>
        <b-form-group
          id="rule-name-input-group"
          label="Rule Name:"
          label-for="rule-name-input"
          description="Come up with a clever name for this step in the rules"
        >          
          <b-form-input 
            v-model="ruleName" 
            placeholder="Enter Rule Name"
          ></b-form-input>
        </b-form-group>
        <b-form-group
          id="rule-text-input-group"
          label="Rule Text:"
          label-for="rule-text-input"
          description="Come up with a clear and concise step in the rules for this game"
        >          
          <b-form-textarea 
            v-model="ruleText" 
            placeholder="Enter Rule Text"
            rows=3
            max-rows=6
          ></b-form-textarea>
        </b-form-group>
      </b-form>
      <b-row>
        <b-col md=4>
        </b-col>
        <b-col md=4>
        </b-col>
        <b-col md=4>
        </b-col>
      </b-row>
    </div>
</template>

<script>
// import TableCardsSingle from '@/components/Table/TableCardsSingle'
// import HandCompSingle from '@/components/Hand/HandCompSingle'
// import HandComp from '@/components/Hand/HandComp'
import { mapGetters } from 'vuex'

export default {
  name: 'CrudForm',
  props: {
  },
  components: {
    // TableCardsSingle,
    // HandComp,
    // HandCompSingle
  },
  data () {
    return {
      selectedGame: null,
      ruleName: null,
      ruleText: null,
      options: [
        { value: null, text: 'Please select an option' },
        { value: 'escoba', text: 'Escoba' },
        { value: 'pusoyDos', text: 'Pusoy Dos' },
        { value: { C: '3PO' }, text: 'This is an option with object value' },
        { value: 'd', text: 'This one is disabled', disabled: true }
        ]
      }
  },
  computed: {
    ...mapGetters([
      'getBaseUrl'
    ]),
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
    onCreateRule: function () {
      console.log('onCreateRule from crud form')
      var payload = {
        gameName: this.selectedGame,
        ruleName: this.ruleName,
        ruleText: this.ruleText
      }
      this.$emit('create-rule', payload)
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

.crud-icon-group {
  padding: 0rem 0rem 1.5rem 0rem;
}

.crud-icon-spacer {
  padding: 0rem .5rem 0rem .5rem;
}

  [class*=hand-player-] {
    background-color: #1d2833;
    border: solid 1px #42b983;
  }


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
