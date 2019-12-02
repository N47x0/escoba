<template>
  <div class="crud">
    <div>
      <b-container>
        <CrudForm />
      </b-container>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
// import DeckComp from '@/components/DeckComp.vue'
import CrudForm from '@/components/Crud/CrudForm.vue'
import { mapGetters } from 'vuex'

export default {
  name: 'Board',
  components: {
    CrudForm
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