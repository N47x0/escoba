<template>
  <div class="crud">
    <div>
      <b-container>
        <CrudHeader />
        <hr />
        <CrudForm />
        <hr />
        <CrudTable />
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
import { mapGetters } from 'vuex'

export default {
  name: 'Board',
  components: {
    CrudHeader,
    CrudForm,
    CrudTable
  },
  computed: {
    ...mapGetters([
      'getGameDataLoaded'
    ])
  },
  methods: {
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