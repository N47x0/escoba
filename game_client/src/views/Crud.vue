<template>
  <div class="crud">
    <div>
      <b-container>
        <CrudHeader />
        <hr />
        <CrudForm 
          @create-rule="onCreateRule"
        />
        <hr />
        <CrudTable
          :items="items"    
        />
      </b-container>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
// import DeckComp from '@/components/DeckComp.vue'
import CrudHeader from '@/components/Crud/CrudHeader.vue'
import CrudForm from '@/components/Crud/CrudForm.vue'
import CrudTable from '@/components/Crud/CrudTable.vue'
import axios from 'axios'
import { mapGetters } from 'vuex'

export default {
  name: 'Board',
  components: {
    CrudHeader,
    CrudForm,
    CrudTable
  },
  data () {
    return {
      items: [
        { isActive: true, game: 40, ruleTitle: 'Dickerson', ruleText: 'Macdonald' },
        { isActive: false, game: 21, ruleTitle: 'Larsen', ruleText: 'Shaw' },
        { isActive: false, game: 89, ruleTitle: 'Geneva', ruleText: 'Wilson' },
        { isActive: true, game: 38, ruleTitle: 'Jami', ruleText: 'Carney' }
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
    onCreateRule: function (payload) {
      console.log(payload)
      var url = this.getBaseUrl + `/rules/CreateRule`
      this.post(url, payload)
    },
    post: async function (url, payload) {
      console.log(url)
      console.log(payload)
      console.log(JSON.stringify(payload))

      var config = {
        headers: {
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*'
        }
      }
      await axios
        .post(url, payload, config)
        .then(function (response) {
          console.log(response)
        })
        .catch(function (error) {
          console.log(error)
        })
    }
  },
  mounted: function () {
    console.log(this)
  }
}
</script>

<style scoped>
  .crud {
    padding: 1rem 0rem 5rem 0rem;
    background-color: #2c3e50;  
  }
</style>